using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBear : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Ativ(){
        float speed = PlayerPI_M.Speed;
        speed -= 1f;
        PlayerPI_M.Speed = speed;
        yield return new WaitForSeconds(5f);
        speed = 5f;
        PlayerPI_M.Speed = speed;
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "Player"){
            anim.SetTrigger("Activ");
            StartCoroutine(Ativ());
            Destroy(gameObject, 2f);
        }
    }
}
