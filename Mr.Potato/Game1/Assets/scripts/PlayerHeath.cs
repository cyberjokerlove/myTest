using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath : MonoBehaviour
{
    public float health = 100f;
    public float hurtBloodPoint = 20f;
    public float damageRepeat = 0.5f;//受伤时间间隔
    public AudioClip[] ouchClips;
    public float hurtForce = 200f;
    SpriteRenderer healthBar;
    Vector3 healthBarScale;
    private float lastHurt;//最后一次受伤时间
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();//找到血条
        healthBarScale = healthBar.transform.localScale;//血条长度
        lastHurt = Time.time;//初始化最后一次受伤时间

        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Time.time > damageRepeat)
            {
                if (health > 0)
                {
                    //减血,并更新血条状态
                    TakeDamge(collision.gameObject.transform);
                    lastHurt = Time.time;
                    if (health <= 0)
                    {
                        //播放死亡动画
                        anim.SetTrigger("die");
                        //掉到河中
                        Collider2D[] colliders = GetComponents<Collider2D>();  //获取碰撞器
                        foreach (Collider2D c in colliders)  //遍历colliders
                            c.isTrigger = true;

                        /*    SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>();//死亡时将英雄设置为UI层，场景不会挡住falling动画
                            foreach (SpriteRenderer s in sp)
                                s.sortingLayerName = "UI";
                                s.sortingLayerID=SortingLayer.NameToID("UI");
                                s.sortingOrder=5;
                         */
                        GetComponent<PlayerControl>().enabled = false;
                        GetComponentInChildren<Gun>().enabled = false;
                    }
                }
            }

        }
    }
    void TakeDamge(Transform enemy)
    {
        health -= hurtBloodPoint;
        health = Mathf.Clamp(health, 0f, 100f);
        //更新血条状态
        UpdateHealthBar();

        Vector3 hurtVector = transform.position - enemy.position + Vector3.up;//撞到敌人时弹开
        GetComponent<Rigidbody2D>().AddForce(hurtForce * hurtVector);

        int i = Random.Range(0, ouchClips.Length);
        AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);//受伤时播放声音
    }
    public void UpdateHealthBar()
    {
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);//更新血条颜色
        healthBar.transform.localScale = new Vector3(health * 0.01f, 1, 1);//更新血条长度
    }
}
