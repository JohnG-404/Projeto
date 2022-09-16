using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            gc.lastCheckPointPos = transform.position;
        }
    }
}
