using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controle.M2D;

public class Cat : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layersPermitidas;
    [SerializeField]
    private Vector2 _rayCastOffset;

    public float Speed;
    public float Stop;
    public float JumpCat;
    private Transform Player;
    private Animator anim;
    private Rigidbody2D rig;
    private Controle2D _controle;
    private RaycastHit2D _raycastParedeDireitaInfo;
    private bool isJumping;
    private bool Fcat = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        _controle = GetComponent<Controle2D>();
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if(Fcat == false){
            if(Vector2.Distance(transform.position, Player.position) > Stop)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.position.x,transform.position.y), Speed * Time.deltaTime);    
                anim.SetBool("Walk", true);       
            }else
            {
                anim.SetBool("Walk", false);
            }
            if((transform.position.x - Player.position.x)<0)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if((transform.position.x - Player.position.x)>0){
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
        
        DetectaParede();
        // DetectaBeira();
    }

    private void DetectaParede()
    {
        var origemX = transform.position.x + _rayCastOffset.x;
        var origemY = transform.position.y + _rayCastOffset.y;
        _raycastParedeDireitaInfo = Physics2D.Raycast(new Vector2(origemX, origemY), Vector2.right, 1f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.right, Color.cyan);
        if (_raycastParedeDireitaInfo.collider != null)
        {
            Pula();
        }

        var raycastParedeEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y + _rayCastOffset.y), Vector2.left, 1f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.left, Color.cyan);
        if (raycastParedeEsquerdaInfo.collider != null)
        {
            Pula();
        }
    }
    // private void DetectaBeira()
    // {
    //     var raycastChaoDireitaInfo = Physics2D.Raycast(new Vector2(transform.position.x + _rayCastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
    //     Debug.DrawRay(new Vector2(transform.position.x + _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
    //     if (raycastChaoDireitaInfo.collider == null)
    //     {
    //             Pula();
    //     }

    //     var raycastChaoEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, 2f, _layersPermitidas);
    //     Debug.DrawRay(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
    //     if (raycastChaoEsquerdaInfo.collider == null)
    //     {
    //             Pula();
    //     }
    // }
    private void Pula()
    {
        if(!isJumping)
        {
            rig.AddForce(new Vector2(1f, JumpCat),ForceMode2D.Impulse);
        }
    }
    IEnumerator FC(){
        anim.SetBool("Vanish", true);
        yield return new WaitForSeconds(1.5f); 
        gameObject.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.layer == 8){
            isJumping = false;
        }
    }
    void OnCollisionExit2D(Collision2D coll){
        if(coll.gameObject.layer == 8){
            isJumping = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        
        if(col.gameObject.tag == "Cat"){
            Fcat = true;
            StartCoroutine(FC());
        }
    }
}
