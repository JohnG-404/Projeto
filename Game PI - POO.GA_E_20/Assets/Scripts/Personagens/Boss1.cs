using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform target;
    public Vector2 alvo;
    public float speed = 100;
    public Transform SpawnA1;
    public GameObject Attack1; 
    public Transform SpawnAS;
    public Transform SpawnAS2;
    public GameObject AttackS;
    private float nextFire; 
    public float fireRate;
    public GameObject bullet;
    public Transform SpawnBullet;
    private bool ativ = false;
    //public float MaxTimeB, MinTimeB;
    //private float D = Vector2.Distance(transform.position, new Vector2(target.position.x,target.position.y));
    
    // Start is called before the first frame update
    void Start()
    {
        alvo = new Vector2(-7.64f ,-2.23f);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float D = Vector2.Distance(transform.position, new Vector2(target.position.x,target.position.y));
        float step = speed * Time.deltaTime;
       
        if(D<5){
            //transform.position = Vector2.MoveTowards(transform.position, alvo /*new Vector2(target.position.x,target.position.y)*/, step);
            Instantiate(Attack1, SpawnA1.position, SpawnA1.rotation);
        }
        if(D<10 && Time.time > nextFire){
            
            nextFire = Time.time + fireRate;
            //Instantiate(bullet, SpawnBullet.position, SpawnBullet.rotation);
            //Invoke("Attack", Random.Range(MaxTimeB, MinTimeB));
        }
        if(ativ == false){
            InvokeRepeating ("Moviments", 10.0f, 1.0f);
        }
    }
    void Moviments(){
        var attk = Random.Range(3.0f , 1.0f);
        ativ = true;
        if(attk == 1.0f){
            Instantiate(AttackS, SpawnAS.position, SpawnAS.rotation);
            Instantiate(AttackS, SpawnAS2.position, SpawnAS2.rotation);
            ativ = false;
        }else if(attk == 2.0f){
            //ataque 2
            ativ = false;
        }else if(attk == 3.0f){
            //ataque 3
            ativ = false;
        }
    }

    
    
}

