using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controle.M2D;

public class EnemyRollerBeatle : MonoBehaviour
{
    [SerializeField]
    private float _velocidade = 20f;
    [SerializeField]
    private LayerMask _layersPermitidas;
    [SerializeField]
    private Vector2 _rayCastOffset;

    private Rigidbody2D rig;
    private Animator anim;
    public float RangeDec;
    public float RangeAttk;
    private float life = 100;
    private Transform Player;
    private bool _estaPulando;
    private Controle2D _controle;
    private bool _seguindoJogador;
    private int _andandoParaDireita;
    private float _movimentoHorizontal;
    private RaycastHit2D _raycastParedeDireitaInfo;
    private float damageTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _andandoParaDireita = 1;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        _controle = GetComponent<Controle2D>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        VEAttackEnemy();
        AplicaMovimento();
        DetectaParede();
        DetectaBeira();
    }

    private void DetectaParede()
    {
        var origemX = transform.position.x + _rayCastOffset.x;
        var origemY = transform.position.y + _rayCastOffset.y;
        _raycastParedeDireitaInfo = Physics2D.Raycast(new Vector2(origemX, origemY), Vector2.right, 0.5f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.right, Color.cyan);
        if (_raycastParedeDireitaInfo.collider != null)
        {
                _andandoParaDireita = -1;
        }

        var raycastParedeEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y + _rayCastOffset.y), Vector2.left, 0.5f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.left, Color.cyan);
        if (raycastParedeEsquerdaInfo.collider != null)
        {
                _andandoParaDireita = 1;
        }
    }



    private void DetectaBeira()
    {
        var raycastChaoDireitaInfo = Physics2D.Raycast(new Vector2(transform.position.x + _rayCastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x + _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoDireitaInfo.collider == null)
        {
                _andandoParaDireita = -1;
        }

        var raycastChaoEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoEsquerdaInfo.collider == null)
        {
                _andandoParaDireita = 1;

        }
    }

    private void AplicaMovimento()
    {
        _movimentoHorizontal = _andandoParaDireita * _velocidade;
        //anim.SetFloat("Velocidade", Math.Abs(_rb.velocity.x));
    }

    void FixedUpdate()
    {
        _controle.Movimento(_movimentoHorizontal * Time.fixedDeltaTime, _estaPulando);
    }

    void VEAttackEnemy(){
        var diferencaParaJogador = Player.gameObject.transform.position.x - transform.position.x;
        _seguindoJogador = Mathf.Abs(diferencaParaJogador) < RangeDec;
        if(_seguindoJogador){
            if(diferencaParaJogador < 0)
            {
                _andandoParaDireita = -1;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                
            }
            else
            {
                _andandoParaDireita = 1;
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
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
