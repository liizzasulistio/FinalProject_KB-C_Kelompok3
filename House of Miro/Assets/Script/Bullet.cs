using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(){
        Debug.Log("boom");
        Destroy(gameObject);
    }

}
