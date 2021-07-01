using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] pickups;
    public float leftX;
    public float rightX;
    public float intervalTime = 5;
    public float hightHealthThreshold = 75f;
    public float lowHealthThreshold = 25f;

    public PlayerHeath playerHealth;


    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeath>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnPickup());

        StopCoroutine(spawnPickup());
    }

    public IEnumerator spawnPickup()//协程产生弹药箱和血宝
    {
        yield return new WaitForSeconds(intervalTime);//程序启动一段时间后开始spawnPickup

        float randomx = Random.Range(leftX, rightX);
        Vector3 randomPos = new Vector3(randomx, 15, 0);

        if(playerHealth.health>=hightHealthThreshold)
            Instantiate(pickups[0], randomPos, Quaternion.identity);
        else if(playerHealth.health <= lowHealthThreshold)
            Instantiate(pickups[1], randomPos, Quaternion.identity);
        else
        {
            int index = Random.Range(0, pickups.Length);
            Instantiate(pickups[index], randomPos, Quaternion.identity);
        }
        
    }
}