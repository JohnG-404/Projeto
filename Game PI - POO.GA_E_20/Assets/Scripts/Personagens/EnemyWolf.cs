using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controle.M2D;

public class EnemyWolf : MonoBehaviour
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
    private Transform Player;
    private Controle2D _controle;
    private float _movimentoHorizontal;
    private RaycastHit2D _raycastParedeDireitaInfo;
    private int _andandoParaDireita;
    private bool _seguindoJogador;
    private bool _estaPulando;
    private float LifeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        _controle = GetComponent<Controle2D>();
        _andandoParaDireita = 1;
        LifeEnemy = 100;
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
        _raycastParedeDireitaInfo = Physics2D.Raycast(new Vector2(origemX, origemY), Vector2.right, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.right, Color.cyan);
        if (_raycastParedeDireitaInfo.collider != null)
        {
            if (!_seguindoJogador)
            {
                _andandoParaDireita = -1;
            }
            else
            {
                Pula();
            }
        }

        var raycastParedeEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y + _rayCastOffset.y), Vector2.left, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.left, Color.cyan);
        if (raycastParedeEsquerdaInfo.collider != null)
        {
            if (!_seguindoJogador)
            {
                _andandoParaDireita = 1;
            }
            else
            {
                Pula();
            }
        }
    }

    private void Pula()
    {
        if(_controle.GetEstaNoChao())
        {
            _estaPulando = true;
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
                _andandoParaDireita = -1;
            }
            else
            {
                Pula();
            }
        }

        var raycastChaoEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoEsquerdaInfo.collider == null)
        {
            if (!_seguindoJogador)
            {
                _andandoParaDireita = 1;
            }
            else
            {
                Pula();
            }
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
        if (_estaPulando)
            _estaPulando = false;
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
    void OnCollisionEnter2D(Collision2D coll){ 
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "BulletPlayer"){
            LifeEnemy -= 10;
            Debug.Log(LifeEnemy);
            if(LifeEnemy <= 0){
                Destroy(gameObject);
            }
        }  
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RangeDec);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeAttk);
    }
}
