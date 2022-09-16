using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;

    public LayerMask playerLayer;
    public float radious;

    private DialogueControl dc;
    bool onRadious;
    public GameObject Light;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }
    void FixedUpdate(){
        Interact();
    }
    void Update(){
        bool PI = DialogueControl.PInt;
        if(Input.GetButtonDown("Interact") && onRadious && PI == true) 
        {
            dc.Speech(profile, speechText, actorName);
        }
    }
    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if(hit != null){
            onRadious = true; 
            Light.SetActive(true);
        } 
        else
        {
            onRadious = false;
            Light.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}
