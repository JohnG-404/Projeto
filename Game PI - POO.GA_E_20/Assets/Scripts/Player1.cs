using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
     public float Speed;
    private Animator anim;
    private Rigidbody2D rig;
    public float JumpF; 
    public bool isJumping;
    public bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
    }
    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        if(Input.GetAxis("Horizontal") > 0f){
            anim.SetBool("Run",true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if(Input.GetAxis("Horizontal") < 0f){
            anim.SetBool("Run",true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if(Input.GetAxis("Horizontal") == 0f){
            anim.SetBool("Run",false);
        } 
    }
    void Jump(){
        if(Input.GetButtonDown("Jump") && !isJumping) 
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpF),ForceMode2D.Impulse);
                anim.SetBool("jump",true);
            }
        }
    }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 8)
            {
                isJumping = false;
                anim.SetBool("jump",false);
            }
        }
        void OnCollisionExit2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 8)
            {
                isJumping = true;
            }
        }
    void Attack(){
        if(Input.GetButtonDown("Attack")) {
            attack = true; 
        }
        anim.SetBool("attack", attack);
    }

}

