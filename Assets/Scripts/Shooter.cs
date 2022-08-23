using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileDuration = 5f;
    [SerializeField] float basefiringRate = 0.2f;
    
    [Header("AI")]
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] bool  useAI;

    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // If AI is enabled, set auto firing on
        if (useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {   
        if (isFiring && firingCoroutine == null) 
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
  
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, 
                                  transform.position,
                                  Quaternion.identity);
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed; 
            }

            Destroy(instance, projectileDuration);

            float projectileDelay = Random.Range(basefiringRate - firingRateVariance,
                                                 basefiringRate + firingRateVariance);

            projectileDelay = Mathf.Clamp(projectileDelay, minimumFiringRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
            
            yield return new WaitForSeconds(projectileDelay);
        }
    }
}
