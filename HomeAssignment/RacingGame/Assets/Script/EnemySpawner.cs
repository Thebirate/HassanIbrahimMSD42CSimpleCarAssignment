using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;

    int startingWave = 0;
    // We have updated the Start built-in method to become a coroutine so that during repetition
    // synchronous functionality is ensured.
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            GameObject enemyClone = Instantiate(waveConfig.GetEnemyPrefab(),
                                                waveConfig.GetWaypoints()[0].position,
                                                Quaternion.identity);

            /* A reference to the current generated clone is fetched so that we can set the current wave
             * inside its EnemyPathing component. Then, enemypathing can know from which wave the path
             * should be fetched.
             */
            enemyClone.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    IEnumerator SpawnAllWaves()
    {
        /* A foreach could also be used. The for loop is set to start from the first wave (wave at position
         * 0) and stop till the last wave in the list.
         */
        for (int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
        {
            WaveConfig currentWave = waveConfigs[waveIndex];

            /* when we yield return a Coroutine call, we are doing the process of synchronous calling or
             * ensuring chaining of coroutines. This is requried since seperate method calls are handled
             * by separate processes. Thus, methods can be executing at the same time. In this case, we 
             * need to ensure that waves are spawned one after the other thus, wave 2 cannot start until
             * wave 1 has finished spawning its enemies. Synchronous calls ensures that methods are called
             * one after the other thus, the following method call starts when the previous finishes 
             * execution. Basically, coroutines WAIT for each other to finish.
             */
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}