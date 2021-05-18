using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTran;//主角的Transform
    public float xMargin = 2.0f;
    public float yMargin = 2.0f;
    public float xSpeed = 1.0f;
    public float ySpeed = 1.0f;
    public Vector2 maxXandY = new Vector2(4, 5);
    public Vector2 minXandY = new Vector2(-4, -3);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
    }

    private void Awake() {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        /*playerTran = GameObject.Find("Hero").transform;*/
    }
    private bool NeedMovex()  //x方向是否需要移动摄像机
    {
        bool bMove = false;
        if(Mathf.Abs(transform.position.x - playerTran.position.x) > xMargin)
            bMove = true;
        else
            bMove = false;
        return bMove;
        /*return Mathf.Abs(transform.position.x - playerTran.position.x) > xMargin;*/
    }
    bool NeedMoveY()  //y方向是否需要移动摄像机
    {
        return Mathf.Abs(transform.position. y- playerTran.position.y) > yMargin;
    }

    private void FixedUpdate()
    {
        //TrackPlayer();
    } 

    private void TrackPlayer() 
    {
    float CamNewX = transform.position.x;
    float CamNewY = transform.position.y;
    if (NeedMovex())//计算新摄像机的位置
        CamNewX = Mathf.Lerp(transform.position.x, playerTran.position.x, xSpeed * Time.deltaTime);
    if (NeedMoveY())
        CamNewY = Mathf.Lerp(transform.position.y, playerTran.position.y, ySpeed * Time.deltaTime);
    //将摄像机位置固定在合法范围内
    CamNewX = Mathf.Clamp(CamNewX, minXandY.x, maxXandY.x);
    CamNewY = Mathf.Clamp(CamNewY, minXandY.y, maxXandY.y);
        transform.position = new Vector3(CamNewX, CamNewY, transform.position.z);
    }
}