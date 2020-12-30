using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyEggController : MonoBehaviour
{
    Rigidbody2D rb;
    int yumurtaHiz = 100;
   public  Vector2 dusmanaDogruHareket;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(dusmanaDogruHareket.x * yumurtaHiz * Time.deltaTime, dusmanaDogruHareket.y * yumurtaHiz * Time.deltaTime);
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "enemy" && col.tag!="yumurta")
        {
            Destroy(gameObject);
        }
    }
}
