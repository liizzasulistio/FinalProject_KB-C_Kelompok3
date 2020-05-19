using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    void OnTriggerEnter2D(){
        if(Hostage.followPlayer){
            Debug.Log("you win");
            Time.timeScale = 0;
            ShowHealth.win = true;
        }
    }
}
