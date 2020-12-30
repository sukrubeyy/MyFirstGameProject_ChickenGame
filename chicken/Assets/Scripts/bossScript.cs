using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bossScript : MonoBehaviour
{
    GameObject player;
    public GameObject kursun, kursunCikicakYer;
    public int can = 100;
    public Button zipla;
    float mermiSaniye = 0;
    public Image theEndsImage;

    public GameObject effects;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        theEndsImage.gameObject.SetActive(false);
    }

   
    void Update()
    {
        menzileGirdiginde();
        if(can<=0)
        {
            Instantiate(effects, transform.position, transform.rotation);
            Destroy(this.gameObject);
            theEndsImage.gameObject.SetActive(true);
            player.gameObject.GetComponent<chicken>().enabled = false;
            player.gameObject.GetComponent<Animator>().enabled = false;
            zipla.gameObject.GetComponent<Button>().enabled = false;

        }
    }
    void menzileGirdiginde()
    {
        float aradakiMesafe = Vector3.Distance(transform.position, player.transform.position);
        if(aradakiMesafe<20f)
        {
            mermiSaniye += Time.deltaTime;
            if(mermiSaniye>0.3f)
            {
                Vector2 git = player.transform.position - transform.position;
                GameObject bullets = Instantiate(kursun, kursunCikicakYer.transform.position, kursunCikicakYer.transform.rotation);
                bullets.GetComponent<bossKursunController>().gidilecekYer = git;
                mermiSaniye = 0;
            }
              
               
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="yumurta")
        {
            can =can-15;
        }
    }
}
