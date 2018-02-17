using UnityEngine;
using System.Collections;

public class MasterSpawner : MonoBehaviour {

    [SerializeField]public SpawnerScript[] spawners = new SpawnerScript[4];

    public int noToSpawn;
    public float spawnDelay;
    
    public float minHealth, maxHealth;
    public float minSpeed, maxSpeed;
    public float minAttackDamage, maxAttackDamage;
    public float minAttackSpeed, maxAttackSpeed;

    public bool spawn;


    IEnumerator SpawnAtRandom()
    {
        if (spawn)
        {
            for (int i = 0; i < noToSpawn; i++)
            {
                spawners[Random.Range(0, 4)].SpawnEnemy(Random.Range(minHealth, maxHealth), Random.Range(minSpeed, maxSpeed), Random.Range(minAttackDamage, maxAttackDamage), Random.Range(minAttackSpeed, maxAttackSpeed));
                yield return new WaitForSeconds(spawnDelay);
            }            
        }
        spawn = false;

    }

    public void Reset()
    {
        StopCoroutine(SpawnAtRandom());
        StartCoroutine(SpawnAtRandom());
    }
}
