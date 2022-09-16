using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblin : MonoBehaviour
{
    private float Stop;
    private float speed;
    public float RangeDec;
    private int life = 100;
    private bool _seguindoJogador;
    //private float diferencaParaJogador; 
    public Collider2D c;
    private Animator anim;
    private Rigidbody2D rig;
    private Transform Player;
    private float damageTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
    }

    // Update is called once per frame
    void Update()
    { 
        VEAttackEnemy();
    }


    void VEAttackEnemy(){
        var diferencaParaJogador =  Player.gameObject.transform.position.x - transform.position.x;
       
        _seguindoJogador = Mathf.Abs(diferencaParaJogador) < RangeDec;
        // Debug.Log(_seguindoJogador);
       // Debug.Log(Player.rotation.y);
        if(_seguindoJogador){   
            if(diferencaParaJogador < -1 && Player.rotation.y != 0f)
            {
                speed = 3;
                anim.SetBool("Walk", true);
                Stop = -1.3f;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                if(diferencaParaJogador < Stop){
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.position.x,transform.position.y), speed * Time.deltaTime);
                    //c.enabled = false;
                    anim.SetBool("Attack", false);
                } else if(diferencaParaJogador >= Stop){
                    //c.enabled = true;
                    anim.SetBool("Attack", true);
                }
            }
            else if(diferencaParaJogador > 1 && Player.rotation.y == 0f)
            {    
                speed = 3;
                anim.SetBool("Walk", true);
                Stop = 1.3f;
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                if(diferencaParaJogador > Stop){
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.position.x,transform.position.y), speed * Time.deltaTime);
                    //c.enabled = false;
                    anim.SetBool("Attack", false);
                } else if(diferencaParaJogador <= Stop){
                    anim.SetBool("Attack", true);
                    //c.enabled = true;
                }
            }else{
                speed = 0;
                anim.SetBool("Walk", false);
            }
        }else{
            anim.SetBool("Walk", false);
        }    
    }
    public IEnumerator DamageA(){
            for (float i = 0; i < damageTime; i+= 0.2f)
			{
				GetComponent<SpriteRenderer>().enabled = false;
				yield return new WaitForSeconds(0.1f);
				GetComponent<SpriteRenderer>().enabled = true;
				yield return new WaitForSeconds(0.1f);
			}
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "BulletPlayer"){
            life -= 10;
            StartCoroutine(DamageA());
            Debug.Log(life);
            if(life <= 0){
                Die();
            }
        }
        if(coll.gameObject.tag == "ShootEnergy"){
            life -= 30;
            StartCoroutine(DamageA());
            if(life <= 0)
            {
                Die();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "BulletPlayer"){
            life -= 10;
            StartCoroutine(DamageA());
            Debug.Log(life);
            if(life <= 0){
                Die();
            }
        }
        if(coll.gameObject.tag == "ShootEnergy"){
            life -= 30;
            StartCoroutine(DamageA());
            if(life <= 0)
            {
                Die();
            }
        }
    }
    public void TookDamage(int damage){
        life -= damage;
        StartCoroutine(DamageA());
        if(life <= 0) {
            Die();
        }
    }
    void Die(){
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RangeDec);
    }
}
