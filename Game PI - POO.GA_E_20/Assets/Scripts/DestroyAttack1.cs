using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAttack1 : MonoBehaviour
{
    public float DestroyAttack;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyAttack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
