using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunDestroyEffect : MonoBehaviour
{
    float zamansayici = 0;
    void Start()
    {

    }


    void Update()
    {
        zamansayici += Time.deltaTime;
        if (zamansayici > 2f)
        {
            Destroy(gameObject);
        }
    }
}
