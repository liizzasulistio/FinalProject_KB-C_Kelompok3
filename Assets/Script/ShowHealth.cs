using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour
{
    Text health;
    public static bool death;
    public static bool win;
    void Start()
    {
        death = false;
        win = false;
        health = GetComponent<Text>();
    }
    void Update()
    {
        health.text = "Save the hostage and bring her back!\nPress WASD to move\nClick left mouse to shoot\nHealth: " + PlayerHealth.curHealth;   
        if(death){
            health.text = "GAME OVER\n press Q to Exit";  
        }
        if(win){
            health.text = "YOU WIN!\n press Q to Exit";  
        }
        if(Input.GetKey(KeyCode.Q)){
            Application.Quit();
            Debug.Log("quitting");
        }
    }
}
