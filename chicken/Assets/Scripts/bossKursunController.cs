using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossKursunController : MonoBehaviour
{
    Rigidbody2D rb;
    int hiz = 300;
    public Vector2 gidilecekYer;
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(gidilecekYer.x * hiz * Time.deltaTime, gidilecekYer.y * hiz * Time.deltaTime);

    }

   
    void Update()
    {
        
    }
  
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.tag!="boss")
        {
            Destroy(gameObject);
        }
    }
}
