using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class misirController : MonoBehaviour
{
    public float donmeHizi = 10;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(new Vector3(0, 0,donmeHizi*Time.deltaTime));
    }
}
