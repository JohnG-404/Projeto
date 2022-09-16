using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemySlime : MonoBehaviour
{
    [SerializeField]
    private float _rangeDetectar;
    [SerializeField]
    private GameObject _jogador;

    public float danoSlime = 20.0f;
    //public float MinShootSpawn, MaxShootSpawn;
    public GameObject Shoot;
    public Transform ShootSpawn;
    private bool _ataqueJogador;
    private float nextFire; 
    public float fireRate;
/*---------------------------------Movimentação---------------------------------*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VEAttackEnemy();
    }
    void Patrulha(){
            
    }
    void VEAttackEnemy(){
        var diferencaParaJogador = _jogador.gameObject.transform.position.x - transform.position.x;
        if(Mathf.Abs(diferencaParaJogador) < _rangeDetectar && Time.time > nextFire){
            ShootE();
            nextFire = Time.time + fireRate;
        }
            if(diferencaParaJogador < 0)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
    }
    void ShootE(){
        Instantiate(Shoot, ShootSpawn.position, ShootSpawn.rotation);
    }
/*---------------------------------Movimentação---------------------------------*/
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeDetectar);
    }
}
