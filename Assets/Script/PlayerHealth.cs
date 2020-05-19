using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public static int curHealth;
    private bool gameOver;
    
   // public float healtBarLength;
    // Start is called before the first frame update
    void Start()
    {
       // healthBarLength = Screen.width / 2;
       curHealth = maxHealth;
       gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver){
            AddjustCurrentHealth(0);
        }
        
    }
   // void OnGUI()
    //{
     //GUI.Box(new Rect(10, 10, healthBarLength, 20), curHealth + "/" + maxHealth);
     //}
     
    
     public void AddjustCurrentHealth(int adj) {
       curHealth += adj;
       //Debug.Log(curHealth);  
         if(curHealth < 0){
            curHealth = 0;
            Debug.Log("player lose");
            // pause the game
            ShowHealth.death = true;
            Time.timeScale = 0;
            //SceneManager.LoadScene (sceneName:"Put the name of the scene here");
         }
             
         
         if(curHealth > maxHealth)
             curHealth = maxHealth;
         
         if(maxHealth < 1)
             maxHealth = 1;
        
        //Debug.Log(curHealth);
         
        // healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
     }
    
    
    
    
}
