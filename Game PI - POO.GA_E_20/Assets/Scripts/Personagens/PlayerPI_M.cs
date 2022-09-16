using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPI_M : MonoBehaviour
{
    //public GameObject PotionLife;
    public LayerMask EnemyLayers;
    private bool imune = false;      
    /*---------------------------------V Movimentação---------------------------------*/
    public static float Speed = 5;
    /*---------------------------------V Pulo---------------------------------*/
    public float JumpF; 
    private bool isJumping;
    /*---------------------------------V Checkpoint---------------------------------*/
    private GameController gc; 
    /*---------------------------------V StatusSword---------------------------------*/
    public bool SwordEquip = false;
    public float rangeAtt;
    public Transform PosAtt;
    private bool EAtt = true;
    private float nextAtt; 
    public float AttRate;
    private float damageTime = 0.5f;
    /*---------------------------------V Ataque---------------------------------*/
    //public bool attackG = false;
    public GameObject bullet;
    public Transform arma;
    public GameObject EnergyCut;
    public Transform SpawnEnergyCut;
    private Animator anim;
    private Rigidbody2D rig;
    private float nextFire; 
    public float fireRate;
    /*---------------------------------V Barra de Vida---------------------------------*/
    public HealthBar healthBar;
    public int maxHealth = 100;
	public static int currentHealth;
    private SpriteRenderer sprite;
    

    //public GameObject EnemyS;
   // public GameObject Stone;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>(); 
        /*---------------------------------Start Checkpoint---------------------------------*/
        gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        //PotionLife = GameObject.FindGameObjectWithTag("Health").GetComponent<GameObject>();
        //Psprite = GetComponent<SpriteRenderer>().color;
        transform.position = gc.lastCheckPointPos;
        /*---------------------------------Start Barra de Vida---------------------------------*/
        //danoSl = EnemyS.GetComponent<EnemySlime>().danoSlime;
        //danoD = Stone.GetComponent<DownObjects>().danoDown;
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
        SwordEquip = false;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    { 
        Speed = 5;
        bool PSp = DialogueControl.PSpace;
/*---------------------------------Movimentação---------------------------------*/
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        StartCoroutine(Actions());
        
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
        if(Input.GetAxis("Horizontal") > 0f){
            anim.SetBool("SwordRun",true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if(Input.GetAxis("Horizontal") < 0f){
            anim.SetBool("SwordRun",true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);           
        }
        if(Input.GetAxis("Horizontal") == 0f){
            anim.SetBool("SwordRun",false);            
        }
        LimiteDeVida();
        StartCoroutine(DamageP());
/*---------------------------------Pulo---------------------------------*/
        if(Input.GetButtonDown("Jump") && !isJumping && PSp == false) 
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpF),ForceMode2D.Impulse);
                anim.SetBool("jump",true);
            }
        }
    }
/*---------------------------------Ataque com Arma---------------------------------*/
    public IEnumerator Actions(){
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire && SwordEquip == false){
            nextFire = Time.time + fireRate;
            if(!isJumping){
                Speed = 0;
            }
            anim.SetTrigger("Shoot");
            if(Input.GetAxis("Horizontal") < 0f){
                bullet.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            } 
            yield return new WaitForSeconds(0.5f);
            GameObject cloneB = Instantiate(bullet, arma.position,arma.rotation);
            yield return new WaitForSeconds(0.3f);
            Speed = 5;
        }
        if(SwordEquip == true && Input.GetButtonDown("Fire1") && Time.time > nextAtt){
            nextAtt = Time.time + AttRate;
            Collider2D[] hitenemies = Physics2D.OverlapCircleAll(PosAtt.position, rangeAtt, EnemyLayers);
            if(EAtt == true){
                anim.SetTrigger("AttackP1");
            }else if(EAtt == false){
                anim.SetTrigger("AttackP2");
                Instantiate(EnergyCut, SpawnEnergyCut.position, SpawnEnergyCut.rotation);
            }
            foreach(Collider2D enemy in hitenemies){
                if(enemy.GetComponent<EnemyRange>()){
                    enemy.GetComponent<EnemyRange>().TookDamage(20);
                }
                else if(enemy.GetComponent<EnemyCentauro>()){
                    enemy.GetComponent<EnemyCentauro>().TookDamage(20); 
                }
                else if(enemy.GetComponent<EnemyGoblin>()){
                    enemy.GetComponent<EnemyGoblin>().TookDamage(20);
                }
                else if(enemy.GetComponent<EnemyRollerBeatle>()){
                    enemy.GetComponent<EnemyRollerBeatle>().TookDamage(20);
                }
                else if(enemy.GetComponent<EnemySkeleton>()){
                    enemy.GetComponent<EnemySkeleton>().TookDamage(20);
                }
                else if(enemy.GetComponent<CageAction>()){
                    enemy.GetComponent<CageAction>().ActionC();
                }    
            }
            EAtt = !EAtt;
        }
/*---------------------------------Ataque de Espada---------------------------------*/
        if(Input.GetButtonDown("AttackSword")) {
            if(SwordEquip == false){
                anim.SetTrigger("SwordOpen");
            }
            
            // StatusSword = !StatusSword; 
            SwordEquip = !SwordEquip;
            Debug.Log(SwordEquip);
            anim.SetBool("SwordEquip",SwordEquip);
            Speed = 0;
            yield return new WaitForSeconds(0.9f);
            Speed = 5;  
        }
    }
    public IEnumerator DamageP(){
        if(imune){
            for (float i = 0; i < damageTime; i+= 0.2f)
			{
				GetComponent<SpriteRenderer>().enabled = false;
				yield return new WaitForSeconds(0.1f);
				GetComponent<SpriteRenderer>().enabled = true;
				yield return new WaitForSeconds(0.1f);
			}
        }
    }
    void LimiteDeVida(){
        if(currentHealth > 100){
            currentHealth = 100;
        }
        if(currentHealth <= 0){
            gc.ShowGameOver();
        }
    }
/*---------------------------------Colliders---------------------------------*/
    void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 8)
            {
                isJumping = false;
                anim.SetBool("jump",false);
            }
            if(!imune){
                if(collision.gameObject.tag == "Enemy"){
                    currentHealth -= 20;
                    healthBar.SetHealth(currentHealth);
                    //rig.AddForce(new Vector2(-20, 10), ForceMode2D.Impulse);
                    imune = true;
                    Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
                    Invoke("PlayerImune",1.5f);
                } 
            }
            if(collision.gameObject.tag == "Enemy2"){ 
                anim.SetTrigger("DamageP");
                //rig.AddForce(new Vector2(-20, 10), ForceMode2D.Impulse);
            }
            if(collision.gameObject.layer == 12){
                currentHealth = 0;
                healthBar.SetHealth(currentHealth);
                gc.ShowGameOver();
            }
        }
    void OnCollisionExit2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 8)
            {
                isJumping = true;
            }


        }
    void OnTriggerEnter2D(Collider2D coll){
        if(!imune){
                if(coll.gameObject.tag == "Enemy"){
                    currentHealth -= 20;
                    healthBar.SetHealth(currentHealth);
                    //rig.AddForce(new Vector2(-20, 10), ForceMode2D.Impulse);
                    imune = true;
                    Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
                    Invoke("PlayerImune", 1.5f);
                } 
            }
        if(coll.gameObject.tag == "Health"){
            if(currentHealth < 100){
                currentHealth += 20;
                healthBar.SetHealth(currentHealth);
                Debug.Log(currentHealth);
                //Destroy(PotionLife, 0.1f);
            }
        }
        if(!imune){
            if(coll.gameObject.layer == 11){
                currentHealth -= 10;
                healthBar.SetHealth(currentHealth);
                Debug.Log(currentHealth);
                //rig.AddForce(new Vector2(-20, 10), ForceMode2D.Impulse);

                imune = true;
                Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
                Invoke("PlayerImune", 2f);
            }
        }
    }
    void PlayerImune(){
        imune = false;
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    } 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(PosAtt.position, rangeAtt);
    }

} 
