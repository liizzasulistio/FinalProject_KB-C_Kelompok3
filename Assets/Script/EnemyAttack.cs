using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject target;
    public float attackTime;
    public float coolDown;
    
    // Start is called before the first frame update
    void Start()
    {
        attackTime = 3;
        coolDown = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTime > 0)
                attackTime -= Time.deltaTime;
            
            if(attackTime < 0)
                attackTime = 0;
            
            
        if(attackTime == 0)
        {
            Attack();
            attackTime = coolDown;
        }
    }
    private void Attack()
    {
        target = GameObject.Find("Player");
        float distance = Vector3.Distance(target.transform.position, transform.position);
    
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);
    
        Debug.Log(distance + " " + direction);
            
        if(distance < 3.5f)
        {
            if(direction > 0)
            {
                
                PlayerHealth eh = (PlayerHealth)target.GetComponent("PlayerHealth");
                //Debug.Log(eh.transform.position);
                eh.AddjustCurrentHealth(-10);
                    //Debug.Log ("success" + gameObject.name);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Weapon")
        {
           Destroy(gameObject);
        }
    }
   
}
