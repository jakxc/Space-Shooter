using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool isPlayer;
    
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() 
    {
       cameraShake = Camera.main.GetComponent<CameraShake>(); 
       audioPlayer = FindObjectOfType<AudioPlayer>();   
       scoreKeeper = FindObjectOfType<ScoreKeeper>();
       levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageController damageController = other.GetComponent<DamageController>();

        if (damageController != null && isPlayer)
        {
            TakeDamage(damageController.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayPlayerDamageClip();
            ShakeCamera();
            damageController.Hit();
        } 
        else if (damageController != null && ! isPlayer) {
            TakeDamage(damageController.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayEnemyDamageClip();
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
        else
        {
            levelManager.LoadGameOver();
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
        if (cameraShake != null)
        {
            cameraShake.Play();
        }
    }

    public float GetHealth()
    {
        return health;
    }
}
