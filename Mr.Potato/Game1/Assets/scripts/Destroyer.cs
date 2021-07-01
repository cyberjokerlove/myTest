using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    /*void DestroyGameObject()
    {
        Destroy(gameObject);
    }*/

    public bool destroyOnAwake;
    public float awakeDestroyDelay;
    public bool findChild = false;
    public string namedChild;
    // Start is called before the first frame update
    void Awake()
    {
        if (destroyOnAwake)
        {
            if (findChild)
            {
                Destroy(transform.Find(namedChild).gameObject);
            }
            else
            {
                Destroy(gameObject, awakeDestroyDelay);
            }
        }
    }
   
    void DisableChildGameObject()
    {
        // Destroy this child gameobject, this can be called from an Animation Event.
        if (transform.Find(namedChild).gameObject.activeSelf == true)
            transform.Find(namedChild).gameObject.SetActive(false);
    }
    // Update is called once per frame
    void DestroyGameObject()
    {
        // Destroy this gameobject, this can be called from an Animation Event.
        Destroy(gameObject);
    }
}
