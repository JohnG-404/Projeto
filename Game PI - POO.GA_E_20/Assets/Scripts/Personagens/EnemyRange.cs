using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemyRange : MonoBehaviour
{
    [SerializeField]
    private float _rangeDetectar;

    private Transform Player;
    private Animator anim;
    //public float MinShootSpawn, MaxShootSpawn;
    public Rigidbody2D Shoot;
    public Transform ShootSpawn;
    private bool _ataqueJogador;
    private bool _seguindoJogador;
    //private float diferencaParaJogador;
    private float nextFire; 
    public float fireRate;
    public float ForceYMin;
    public float ForceYMax;
    public float life = 100;
    private float damageTime = 1f; 
/*---------------------------------Movimentação---------------------------------*/
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        VEAttackEnemy();
    }
    void Patrulha(){
            
    }
    void VEAttackEnemy(){
        var diferencaParaJogador = Player.gameObject.transform.position.x - transform.position.x;
        _seguindoJogador = Mathf.Abs(diferencaParaJogador) < _rangeDetectar;
        if(_seguindoJogador){
            if(diferencaParaJogador < 0 && Time.time > nextFire)
            {
                Invoke("ShootE", nextFire);
                nextFire = Time.time + fireRate;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                
            }
            else if(diferencaParaJogador > 0 && Time.time > nextFire)
            {
                Invoke("ShootE", nextFire);
                nextFire = Time.time + fireRate;
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }else{
                anim.SetBool("Attack", false);
            }
        }
    }
    void ShootE(){
        anim.SetBool("Attack", true);
        Rigidbody2D bullet = Instantiate(Shoot, ShootSpawn.position, ShootSpawn.rotation);
        bullet.AddForce(new Vector2(0,Random.Range(ForceYMin, ForceYMax)), ForceMode2D.Impulse);
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
            if(life <= 0)
            {
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
/*---------------------------------Movimentação---------------------------------*/
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeDetectar);
    }
}
