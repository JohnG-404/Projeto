using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownObjects : MonoBehaviour
{
    private Animator anim;
    public float danoDown = 40.0f;
    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.layer == 8 /*&& col.gameObject.tag == "Player"*/){
            anim.SetTrigger("Destroy");
            Destroy(gameObject, destroyTime);
        }
    }
}
