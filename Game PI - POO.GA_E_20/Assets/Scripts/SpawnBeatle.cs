using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeatle : MonoBehaviour
{
    public GameObject RollerBeatle;
    private Transform T;
    private int nI = 1;
    public List<GameObject> ListB;

    void Start(){
        T = GetComponent<Transform>();
    }
    void Update(){
        SpBeatle();
        for(int i = 0; i < nI; i++){
                GameObject Beatle = Instantiate(RollerBeatle, T.position, T.rotation);
                ListB.Add(Beatle);
        }
    }

    public void SpBeatle(){
        
    }
}

