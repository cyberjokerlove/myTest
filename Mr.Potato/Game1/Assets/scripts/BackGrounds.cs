using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    public Transform[] backGrounds;
    public float fparallax = 0.4f;
    public float layerFraction = 5f;
    public float fSmooth = 5f;
    Transform cam;
    Vector3 previousCamPos;

    private void Awake()
    {
         cam = Camera.main.transform;//获取摄像机的位置
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;//存摄像机初始位置
    }

    // Update is called once per frame
    void Update()
    {
        float fParrllaX = (previousCamPos.x - cam.position.x) * fparallax;//所有相对摄像机偏移量
        for (int i = 0; i < backGrounds.Length; i++)
        {
            float fNewX = backGrounds[i].position.x + fParrllaX * (1 + i * layerFraction);
            Vector3 newPos = new Vector3(fNewX, backGrounds[i].position.y, backGrounds[i].position.z);
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, newPos, Time.deltaTime* fSmooth);//镜头平滑移动
        }
        previousCamPos = cam.position;//当前摄像机位置赋给上一帧
    }  
}
