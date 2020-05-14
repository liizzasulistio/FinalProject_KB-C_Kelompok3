using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float  weaponSpeed = 7f;
    public static int health = 5;
    Vector2 movement;
    public Rigidbody2D player;
    public Camera cam;
    Vector2 mousePos;
    public GameObject weaponPrefab;
    public Transform shootPoint;
    public GameObject currentInterObj = null;

    void Start()
    {
        
    }

    void Update()
    {
        // movement
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // shoot
        if(Input.GetButtonDown("Fire1")){
            GameObject weapon = Instantiate(weaponPrefab, shootPoint.position, shootPoint.rotation);
            Debug.Log("weapon created");
            Rigidbody2D w = weapon.GetComponent<Rigidbody2D>();
            w.AddForce(shootPoint.up * weaponSpeed, ForceMode2D.Impulse); 
            Debug.Log("Pew!");
        }

        // if near hostage, press enter to save hostage
        if(Input.GetButtonDown("Submit") && currentInterObj && currentInterObj.CompareTag("Hostage")){
            Debug.Log("Hostage saved");
            Hostage.followPlayer = true;
        }

        // game over
        if(health <= 0){
            Destroy(gameObject);
        }
    }   

    void FixedUpdate(){
        player.MovePosition(player.position + movement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - player.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        player.rotation = angle;
    }

    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(other.name);
        currentInterObj = other.gameObject;
    }
}
