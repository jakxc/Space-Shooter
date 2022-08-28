using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip playerDamageClip;
    [SerializeField] [Range(0f, 1f)] float playerDamageVolume = 1f;
    [SerializeField] AudioClip enemyDamageClip;
    [SerializeField] [Range(0f, 1f)] float enemyDamageVolume = 1f;
    [SerializeField] AudioClip playerDeathClip;
    [SerializeField] [Range(0f, 1f)] float playerDeathVolume = 1f;

    static AudioPlayer instance;

    void Awake() 
    {
        ManageSingleton();
    }

    // public AudioPlayer GetAudioPlayerInstance()
    // {
    //     return instance;
    // }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false); //Ensure AudioPlayer is not used by other classes
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayPlayerDamageClip()
    {
        PlayClip(playerDamageClip, playerDamageVolume);
    }

    public void PlayEnemyDamageClip()
    {
        PlayClip(enemyDamageClip, enemyDamageVolume);
    }

    public void PlayPlayerDeathClip()
    {
        PlayClip(playerDeathClip, playerDeathVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {   
            Vector3 cameraPos = Camera.main.transform.position;
           
            AudioSource.PlayClipAtPoint(clip, 
                                        cameraPos,
                                        volume);
        }
    }

}
