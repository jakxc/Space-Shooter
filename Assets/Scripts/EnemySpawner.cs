using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float wavesDelay = 0f;
    [SerializeField] bool isLooping;
    WaveConfigSO currentWave;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do {
            foreach(WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;

                for (int i = 0; i < currentWave.GetEnemyCount(); i++) 
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.identity,
                                transform);

                    yield return new WaitForSeconds(currentWave.GetSpawnDelay());
                }

                yield return new WaitForSeconds(wavesDelay);
            }
        }
        while(isLooping == true);
    }
}
