using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D heroBody;
    public float moveForce = 100;
    public float jumpForce = 500;
    private float fInput = 0.0f;
    public float maxSpeed = 5;
    [HideInInspector]
    public bool bFaceRight = true;
    public AudioClip[] jumpClips;
    private bool bJump = false;
    //[SerializeField]
    private bool bGrounded = false;
    Transform mGroundcheck;
    private Animator anim;


    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();
        mGroundcheck = transform.Find("GroundCheck");//找到空物体的位置
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        fInput = Input.GetAxis("Horizontal");
        if (fInput < 0 && bFaceRight)
            flip();
        else if (fInput >0 && !bFaceRight)
            flip();

        heroBody.AddForce(Vector2.right * fInput * moveForce);

        bGrounded=Physics2D.Linecast(transform.position, mGroundcheck.position, 1 << LayerMask.NameToLayer("Ground"));//判断空物体Groundcheck是否在地面上，是则返回true
        if (bGrounded)//判断物体在地面上时是否按下空格
        {
            bJump = Input.GetKeyDown(KeyCode.Space);
            Vector2 upForce = new Vector2(0, 1);
            if (bJump)
            {
                heroBody.AddForce(upForce* jumpForce);
                anim.SetTrigger("Jump");//跳跃时播放跳跃动画

                int i = Random.Range(0, jumpClips.Length);
                AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);//跳跃时播放声音

                bJump = false;
            }
        }
            anim.SetFloat("speed", Mathf.Abs(heroBody.velocity.x));//速度>0.1播放动画
    }

    private void FixedUpdate() {

        if (Mathf.Abs(heroBody.velocity.x) < maxSpeed)
        //if (fInput * heroBody.velocity.x < MaxSpeed) ;
           heroBody.AddForce(fInput * moveForce * Vector2.right);

        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed, heroBody.velocity.y);
    }

    void flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale= theScale;
        bFaceRight = !bFaceRight;
    }
}
