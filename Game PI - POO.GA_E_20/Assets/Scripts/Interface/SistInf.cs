using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistInf : MonoBehaviour
{
    public GameObject diC;
    // Start is called before the first frame update
    void Start()
    {
        diC.SetActive(false);
        //GetComponent<Image>().enable = false;
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "Player"){
            diC.SetActive(true);
            //GetComponent<Image>().enable = true;
            //gameObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D coll){
        if(coll.gameObject.tag == "Player"){
            diC.SetActive(false);
            //GetComponent<Image>().enable = true;
            //gameObject.SetActive(true);
        }
    }
}
