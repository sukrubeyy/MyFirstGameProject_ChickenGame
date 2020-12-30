using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemyEffects : MonoBehaviour
{
    float zaman = 0;
    void Start()
    {
        
    }

    
    void Update()
    {
        zaman += Time.deltaTime;
        if(zaman>1)
        {
            Destroy(gameObject);
        }
    }
}
