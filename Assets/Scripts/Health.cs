using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool isPlayer;
    
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    void Awake() 
    {
       cameraShake = Camera.main.GetComponent<CameraShake>(); 
       audioPlayer = FindObjectOfType<AudioPlayer>();   
       scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageController damageController = other.GetComponent<DamageController>();

        if (damageController != null)
        {
            TakeDamage(damageController.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageController.Hit();
        }    
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
           Die();
        }
    }

    void Die()
    {
        // If the player is not destroyed, add the score to the ScoreKeeper
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }

        Destroy(gameObject);
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

    public float GetHealth()
    {
        return health;
    }
}
