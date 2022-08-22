using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    void Awake() 
    {
       cameraShake = Camera.main.GetComponent<CameraShake>();    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageController damageController = other.GetComponent<DamageController>();

        if (damageController != null)
        {
            TakeDamage(damageController.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageController.Hit();
        }    
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
