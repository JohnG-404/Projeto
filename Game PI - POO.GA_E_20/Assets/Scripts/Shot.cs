using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rig;
    public float destroyTime;

    void Start(){   
        //rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
        
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        //rig.velocity = transform.forward * velocidade;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject){
            Destroy(gameObject);
        }
        Destroy(gameObject); 
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject){
            Destroy(gameObject);
        }
        Destroy(gameObject); 
    }
}
