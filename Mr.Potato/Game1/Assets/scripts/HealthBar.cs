using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform playerTransform;
    public Vector3 offset = new Vector3(0, 1.3f, 0);//设置偏移量
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//将主角的位置传给playerTransform
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.transform.position + offset;
    }
}
