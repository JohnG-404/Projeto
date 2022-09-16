using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageAction : MonoBehaviour
{
    public GameObject gaiola;
    private Animator Acat;
    private Collider2D gB;

    public void Start(){
        Acat = GetComponent<Animator>();
        gB = GetComponent<Collider2D>();
    }
    public void ActionC(){
        gaiola.SetActive(false);
        gB.enabled = false;
        StartCoroutine(catA());
    }
    public IEnumerator catA(){
        Acat.SetBool("Vanish", true);
        yield return new WaitForSeconds(1.5f); 
        gameObject.SetActive(false);
    }
}
