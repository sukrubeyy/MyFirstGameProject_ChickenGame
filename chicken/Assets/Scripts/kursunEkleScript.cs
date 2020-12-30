using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunEkleScript : MonoBehaviour
{
    public float donmeHizi = 45;
    void Start()
    {
       
    }

    
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, donmeHizi * Time.deltaTime));
    }
}
