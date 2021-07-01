using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float spawnTime = 5f;        // 每次生成的时间量
    public float spawnDelay = 3f;       // 生成开始前的时间量
    public GameObject[] enemies;		// Array of enemy prefabs.

    // Start is called before the first frame update
    void Start()
    {
        // 延迟后开始重复调用 Spawn 函数。
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    void Spawn()
    {
        // Instantiate a random enemy.
        int enemyIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyIndex], transform.position, transform.localRotation);

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }

}
