using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layersPermitidas;
    [SerializeField]
    private Vector2 _rayCastOffset;
    
    private float speed = 3f;
    private Rigidbody2D rig;
    private Animator anim;
    public float RangeDec;
    public float RangeAttk;
    private Transform Player;
    private RaycastHit2D _raycastParedeDireitaInfo;
    private bool _seguindoJogador;
    private float nextAtt; 
    public float AttRate;
    private float life = 100f;
    private float damageTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        life = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        VEAttackEnemy();
        Move();
        DetectaParede();
        DetectaBeira();
    }

    void Move(){
        rig.velocity = new Vector2 (speed, rig.velocity.y); 
    }

    private void DetectaParede()
    {
        var origemX = transform.position.x + _rayCastOffset.x;
        var origemY = transform.position.y + _rayCastOffset.y;
        _raycastParedeDireitaInfo = Physics2D.Raycast(new Vector2(origemX, origemY), Vector2.right, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.right, Color.cyan);
        if (_raycastParedeDireitaInfo.collider != null)
        {
            if (!_seguindoJogador)
            {
                speed = -3f;
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }

        var raycastParedeEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y + _rayCastOffset.y), Vector2.left, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.left, Color.cyan);
        if (raycastParedeEsquerdaInfo.collider != null)
        {
            if (!_seguindoJogador)
            {
                speed = 3f;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }

    private void DetectaBeira()
    {
        var raycastChaoDireitaInfo = Physics2D.Raycast(new Vector2(transform.position.x + _rayCastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x + _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoDireitaInfo.collider == null)
        {
            if (!_seguindoJogador)
            {
                speed = -3f;
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            
        }

        var raycastChaoEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoEsquerdaInfo.collider == null)
        {
            if (!_seguindoJogador)
            {
                speed = 3f;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }


    void VEAttackEnemy(){
        var diferencaParaJogador = Player.gameObject.transform.position.x - transform.position.x;
        _seguindoJogador = Mathf.Abs(diferencaParaJogador) < RangeDec;
        if(_seguindoJogador){
        if(diferencaParaJogador < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            if(diferencaParaJogador < -1.5f){
                speed = -3f;
                anim.SetBool("Attk", false);
            } else if(diferencaParaJogador > -1.5f && diferencaParaJogador < 0f){
                speed = 0f;
                if(Time.time > nextAtt){
                    nextAtt = Time.time + AttRate;
                    anim.SetBool("Attk", true);
                }
            }
            
        }
        else if(diferencaParaJogador > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            if(diferencaParaJogador > 1.5f){
                speed = 3f;
                anim.SetBool("Attk", false);
            } else if(diferencaParaJogador < 1.5f && diferencaParaJogador > 0f){
                speed = 0f;
                if(Time.time > nextAtt){
                    nextAtt = Time.time + AttRate;
                    anim.SetBool("Attk", true);
                }
            }
        }
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
        if(coll.gameObject.layer == 12){
            Die();
        }
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeAttk);
    }
}
