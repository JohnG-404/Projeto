using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour
{
    
    //private GameObject SPlayer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D coll){
        
        if(coll.gameObject.tag == "Player"){
            int vida = PlayerPI_M.currentHealth;
            if(vida < 100){
                Destroy(gameObject, 0.1f);
            }
            
        }
    } 
}
