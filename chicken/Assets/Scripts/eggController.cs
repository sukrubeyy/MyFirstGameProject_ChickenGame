using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggController : MonoBehaviour
{
    public Vector2 vect2;
    Rigidbody2D rb;
    public float mermiHiz = 30f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rb.velocity = new Vector2(vect2.x * mermiHiz * Time.deltaTime, vect2.y * mermiHiz * Time.deltaTime);
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
       if(col.tag!="Player" && col.tag!="enemyEgg")
        {
            Destroy(gameObject);
        }
    }
}
