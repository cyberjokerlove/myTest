using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject UI_100points;

    public float moveSpeed = 15.0f;
    public Sprite damageEnemy;
    public Sprite deadEnemy;
    public int HP = 2;
    public float maxSpinForce = 100f;
    public float minSpinForce = -100f;

    private Rigidbody2D enemyBody;
    private Transform frontCheck;
    private bool isDead = false;
    private SpriteRenderer curBody;
    private UItest uitest;

    private void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        frontCheck = transform.Find("frontCheck");
        curBody = transform.Find("body").GetComponent<SpriteRenderer>();//当前的body
        uitest = GameObject.Find("Canvas").GetComponent<UItest>();
    }
    private void FixedUpdate()
    {
        enemyBody.velocity = new Vector2(transform.localScale.x * moveSpeed, enemyBody.velocity.y);
        Collider2D[] collders = Physics2D.OverlapPointAll(frontCheck.position);
        foreach(Collider2D c in collders)
        {
            if(c.tag=="wall")
            {
                flip();
                break;
            }
        }
        if (HP == 1 & damageEnemy != null)
        {
            curBody.sprite = damageEnemy;
        }
        if (HP <= 0&& !isDead)
        {
            death();
            
        }
    }
    void death()
    {
        isDead = true;
        SpriteRenderer[] Sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in Sprites)
        {
           // s.Sort
            s.enabled = false;
        }
            
        curBody.enabled = true;
        curBody.sprite = deadEnemy;

        Collider2D[] cols = GetComponents<Collider2D>();
        foreach(Collider2D c in cols)
        {
            c.isTrigger = true;
        }
        //给一个随机选择扭矩
        enemyBody.freezeRotation = false;
        enemyBody.AddTorque(Random.Range(minSpinForce,maxSpinForce));

        Vector3 UI100Pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Instantiate(UI_100points, UI100Pos, Quaternion.identity);
        Debug.Log(Quaternion.identity);

        uitest.AddScore();

    }
    public void Hurt()
    {
        HP--;
    }

    void flip()
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}
