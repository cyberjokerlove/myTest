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
    private bool bJump = false;
    //[SerializeField]
    private bool bGrounded = false;
    Transform mGroundcheck;

    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();
        mGroundcheck = transform.Find("GroundCheck");//找到空物体的位置

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
                bJump = false;
            }
        }
        
    }

    private void FixedUpdate() {

        if (Mathf.Abs(heroBody.velocity.x) < maxSpeed)
        //if (fInput * heroBody.velocity.x < MaxSpeed) ;
           heroBody.AddForce(fInput * moveForce * Vector2.right);

        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed, heroBody.velocity.y);

        /*if (bJump) {
            heroBody.AddForce(new Vector2(0f, jumpForce));
            bJump = false;
        }*/
    }

    void flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale= theScale;
        bFaceRight = !bFaceRight;
    }
}
