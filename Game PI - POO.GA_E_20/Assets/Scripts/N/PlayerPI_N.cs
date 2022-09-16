using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPI_N  : MonoBehaviour
{
    /*public float Speed;
    public bool ladoDireito = true;
    public bool noChao = false;  
    public bool attack = false;
    private Animator anim;
    public AudioClip run;
    public AudioClip hit;
    AudioSource AudioSrc; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        run = Resources.Load<AudioClip>("Sounds/PassosPlayer");
        hit = Resources.Load<AudioClip>("Sounds/PlayerAttackSword");
        AudioSrc = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal")!=0)
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal")*Speed*Time.deltaTime,0,0));
        }
        anim.SetFloat("run", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (Input.GetAxis("Horizontal")>0 && !ladoDireito)
            Vire();
        if (Input.GetAxis("Horizontal")<0 && ladoDireito)
            Vire();
        if (noChao && Input.GetButtonDown("Jump")){
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,350));
            noChao = false;
        }
        anim.SetBool("jump",noChao);
        if(Input.GetButtonDown("Attack")){
            attack=true;
        }
        anim.SetBool("Attack", attack);
        void OnCollisionEnter2D(Collision2D col)
        {
            if(col.gameObject.tag == "chao") {
                noChao=true;
            }
        }
        void Vire()
        {
            ladoDireito=!ladoDireito;
            Vector2 novoScale = new Vector2(transform.localScale.x*-1,transform.localScale.y);
            transform.localScale = novoScale;        
            }
    }*/
}

