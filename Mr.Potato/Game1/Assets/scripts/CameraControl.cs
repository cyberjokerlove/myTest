using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform player;
    public float xMargin = 10.0f;
    public float yMargin = 10.0f;
    public float SmoothX = 1.0f;
    public float SmoothY = 1.0f;
    public float MaxCamX = 5.0f;
    public float MaxCamY = 4.0f;
    public float MinCamX = -5.0f;
    public float MinCamY = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    bool NeedMovex()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }
    bool NeedMoveY()
    {
        return Mathf.Abs(transform.position. y- player.position.y) > yMargin;
    }

    private void FixedUpdate()
    {
        TrackPlayer();
    } 

    void TrackPlayer() {
    float CamNewX = transform.position.x;
    float CamNewY = transform.position.y;
    if (NeedMovex())//计算新摄像机的位置
        CamNewX = Mathf.Lerp(transform.position.x, player.position.x, SmoothX * Time.deltaTime);
    if (NeedMoveY())
        CamNewY = Mathf.Lerp(transform.position.y, player.position.y, SmoothY * Time.deltaTime);
    //将摄像机位置固定在合法范围内
    CamNewX = Mathf.Clamp(CamNewX,MinCamX, MaxCamX);
    CamNewY = Mathf.Clamp(CamNewY,MinCamX, MaxCamX);
        transform.position = new Vector3(CamNewX, CamNewY, transform.position.z);
}
}