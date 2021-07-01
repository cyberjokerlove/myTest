using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killtrigger : MonoBehaviour
{
    public GameObject splash;

    private void OnTriggerEnter2D(Collider2D collision)//水花碰撞检测
    {
        //如果hero碰到tigger
        if(collision.gameObject.tag=="Player")
        {
            //停止摄像机跟随
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;
            // 停止血条跟随
            if (GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
            {
                GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
            }

            //实例化水花当hero falling
            Instantiate(splash, collision.transform.position, transform.rotation);
            //销毁hero
            Destroy(collision.gameObject);
            // ... reload the level.
            StartCoroutine("ReloadGame");
        }
        else
        {
            //实例化水花当enemy faliing
            Instantiate(splash, collision.transform.position, transform.rotation);

            // Destroy the enemy.
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ReloadGame()
    {
        // ... pause briefly
        yield return new WaitForSeconds(2);
        // ... and then reload the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
