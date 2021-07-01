using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;


    void Start()
    {
        Destroy(gameObject, 2);//2秒内未碰到物体销毁
        //explosion = Resources.Load("explosion") as GameObject;//从文件获取预设体对象
    }


    private void OnTriggerEnter2D(Collider2D collision)//炮弹碰撞检测
    {
        float rotationZ = Random.Range(0, 360);//产生一个随机数
        if (collision.tag != "body"|| collision.tag!="Rocket") //检测是否为炮弹或角色
        {
        Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0, 0, rotationZ)));//实例化
        Destroy(gameObject);//销毁
        }

        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hurt();
        }
    }

}
