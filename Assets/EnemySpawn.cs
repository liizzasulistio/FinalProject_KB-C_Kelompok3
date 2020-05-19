using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float time = 5f;
    private float current;
    public GameObject enemy;
    void Start(){
        current = time;
    }

    void Update()
    {
        Debug.Log(current);
     current -= Time.deltaTime;
     if ( current < 0 ){
        Instantiate(enemy, transform.position, transform.rotation);
        current = time;
     }
    }
}
