using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float  bulletSpeed = 7f;
    public static int health = 5;
    Vector2 movement;
    public Rigidbody2D player;
    public Camera cam;
    Vector2 mousePos;
    public GameObject bulletPrefab;
    public Transform shootPoint;

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
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D b = bullet.GetComponent<Rigidbody2D>();
            b.AddForce(shootPoint.up * bulletSpeed, ForceMode2D.Impulse); 
            Debug.Log("Pew!");
        }

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
}
