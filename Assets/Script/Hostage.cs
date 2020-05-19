using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    public static bool followPlayer;
    private float speed;
    private Transform target;
    private float dist;
    // Use this for initialization
    void Start () {
        speed = 5f;
        dist = 1f;
        followPlayer = false;
        target = GameObject.Find("Player").transform;
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
 
    // Update is called once per frame
    void Update () {
        if(followPlayer && Vector3.Distance(transform.position,target.position) >= dist){
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
    }
}
