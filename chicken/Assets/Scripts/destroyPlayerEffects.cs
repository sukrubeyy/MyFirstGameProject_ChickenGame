using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPlayerEffects : MonoBehaviour
{
    float zaman = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        zaman += Time.deltaTime;
        if(zaman>1.5f)
        {
            Destroy(gameObject);
        }
    }
}
