using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class chicken : MonoBehaviour
{
    AudioSource source;


    public Joystick joystick;

    public Rigidbody2D rb;

    public Animator animator;

    public Image durdurulma;
    public Image gameOverEkrani;
    public Image winMenu;
    public Image theEndEkrani;

    public Text kursunYumurtaTEXT;
    public Text CanTEXT;
    public Text misirTEXT;
    
    public Transform mermiAtesYeri;

    public float minX, maxX;
    public float minY, maxY;

    float horizontal = 0;
    float kosmaHiz = 10f;

   

    public GameObject mermiObjesi;
    public GameObject playerEffects;
    public GameObject mermiEfects;

    GameObject[] durdurulacakObjeler;
    GameObject EnemyScripti;
    GameObject boss;

    public int oldurulenDusmanSayisi = 0;
    int kursunSayisi = 10;
    int can = 100;
    int misirSayisi=0;

    Vector2 myTransform;
    Vector2 vect;
    Vector2 mousePosition;

    bool ziplamaKontrol=false;
    bool karakterAtes = true;
    bool ates = true;

    void Start()
    {
        source = GetComponent<AudioSource>();
        gameOverEkrani.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(false);
        theEndEkrani.gameObject.SetActive(false);
        durdurulacakObjeler = GameObject.FindGameObjectsWithTag("enemy");
        boss = GameObject.FindGameObjectWithTag("boss");
        myTransform = transform.position;
        Time.timeScale = 1;
        
    }

    void Update()
    {
        //source.Stop();




        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacinciLevel"))
        {
            PlayerPrefs.SetInt("kacinciLevel", SceneManager.GetActiveScene().buildIndex);
        }
        if(SceneManager.GetActiveScene().buildIndex==2)
        {
            minY = -40;
        }
      

       

        TextYazdir();
        if(can<=0)
        {
           
            gameOVER();
        }
       
        //Karakteri Sinirlama
        myTransform.x=Mathf.Clamp(transform.position.x,minX,maxX);
        myTransform.y = Mathf.Clamp(transform.position.y,minY,maxY);
        transform.position = myTransform;
        // Koşma Kısmında Gerçekleşecekler
        if (joystick.Horizontal>0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(2, 2, 1);
        }
       else if(joystick.Horizontal<0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(-2, 2, 1);
        }
       else if (joystick.Horizontal==0)
        {
            karakterAtes = false;
            animator.SetBool("isRunning", false);
        }
        // Ateş Ederken
        if (!karakterAtes && ziplamaKontrol )
        {
            karakterAtes = true;
            if (Input.GetButtonDown("Fire1"))
            {
               
                if (kursunSayisi > 0)
                {
                    animator.SetBool("isShoting", true);
                    atesEt();
                    kursunSayisi--;
                }
            }
        }
      
       
            if (Input.GetButtonUp("Fire1"))
            {
                animator.SetBool("isShoting", false);
            }

       

    }
    void FixedUpdate()
    {
      
        karakterHareket();
    }
    
    void karakterHareket()
    {
        karakterAtes = true;
        horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        if (karakterAtes)
        {
            if (horizontal > 0)
            {
                transform.Translate(horizontal * kosmaHiz * Time.deltaTime, 0, 0);
            }
            if (horizontal < 0)
            {
                transform.Translate(horizontal * kosmaHiz * Time.deltaTime, 0, 0);
            }
            transform.Translate(0, vertical * 10 * Time.deltaTime, 0);
        }
       
      
        
    }
    void TextYazdir()
    {
        kursunYumurtaTEXT.text = kursunSayisi.ToString();
        CanTEXT.text = can.ToString();
        misirTEXT.text = misirSayisi.ToString();
    }
    void gameOVER()
    {
        Time.timeScale = 0.4f;
        Instantiate(playerEffects, transform.position, transform.rotation);
        this.gameObject.GetComponent<chicken>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
        gameOverEkrani.gameObject.SetActive(true);
       
       
      
            for (int i = 0; i < durdurulacakObjeler.Length - oldurulenDusmanSayisi; i++)
            {
                durdurulacakObjeler[i].GetComponent<enemyController>().enabled = false;
                durdurulacakObjeler[i].GetComponent<Animator>().enabled = false;
            }
        

        if (SceneManager.GetActiveScene().buildIndex==3)
        {
            boss.GetComponent<bossScript>().enabled = false;
        }
        GameObject top = GameObject.FindGameObjectWithTag("engelTOP");
        top.GetComponent<topEngel>().enabled = false;
    }
    void WinMENU()
    {
        winMenu.gameObject.SetActive(true);
        this.gameObject.GetComponent<chicken>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
    }
    void theEndMenu()
    {
        theEndEkrani.gameObject.SetActive(true);
        this.gameObject.GetComponent<chicken>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
    }
   


    private void OnTriggerEnter2D(Collider2D col)
    {
      
        if (col.gameObject.tag == "misir")
        {
            Destroy(col.gameObject);
            misirSayisi++;
            if(can<100)
            {
                can = can + 5;
                if(can>=100)
                {
                    can = 100;
                }
              
            }
           

        }
        else if(col.gameObject.tag=="mermiEkle")
        {
            Destroy(col.gameObject);
            kursunSayisi += 7;
        }
        else if(col.gameObject.tag=="sonMermiEkle")
        {
            Destroy(col.gameObject);
            kursunSayisi += 30;
        }
        else if(col.gameObject.tag=="enemyEgg")
        {
            source.Play();
            Instantiate(mermiEfects, transform.position, transform.rotation);
            if(can>0)
            {
                
              
                can = can - 2;
            }
            if (can < 0)
            {
                can = 0;
                CanTEXT.text = "";
               
            }
        }
        else if(col.gameObject.tag=="bossEgg")
        {
            can = can - 5;
        }
        else if(col.gameObject.tag=="engelTOP")
        {
            gameOVER();
            
            
        }
       else if(col.gameObject.tag=="nextLVL" )
        {
            if(SceneManager.GetActiveScene().buildIndex==1 && misirSayisi==3)
            {
                WinMENU();
            }
            else if(SceneManager.GetActiveScene().buildIndex==2 && misirSayisi==4 && oldurulenDusmanSayisi==5)
            {
                WinMENU();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3 && misirSayisi==4 && oldurulenDusmanSayisi==10)
            {
                theEndMenu();
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        ziplamaKontrol = true;
        karakterAtes = false;
        animator.SetBool("isJumping", false);
    }
    void atesEt()
    {
        if(ates)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 gidilecekYer = mousePosition - rb.position;
            GameObject bullets = Instantiate(mermiObjesi, mermiAtesYeri.position, mermiAtesYeri.rotation);
            bullets.GetComponent<eggController>().vect2 = gidilecekYer;
        }
      
    }
   public void oyunİciButon()
    {
        durdurulma.gameObject.SetActive(true);
        GameObject oyunİci = GameObject.FindGameObjectWithTag("oyuniciMenuAcButonu");
        oyunİci.SetActive(false);
        this.gameObject.GetComponent<chicken>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
        for (int i = 0; i < durdurulacakObjeler.Length - oldurulenDusmanSayisi; i++)
        {
            durdurulacakObjeler[i].GetComponent<enemyController>().enabled = false;
            durdurulacakObjeler[i].GetComponent<Animator>().enabled = false;
        }

    }
  
    public  void anaMenu()
    {
        SceneManager.LoadScene("anaMENU");
    }
    public void retryLEVEL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void bilgiBaglantisi()
    {
        Application.OpenURL("https://www.instagram.com/hybridgamestudio/");
    }
    public void nextLEVEL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ziplama()
    {
        karakterAtes = true;
        if (ziplamaKontrol)
        {
            ziplamaKontrol = false;
            animator.SetBool("isJumping", true);
            rb.AddForce(new Vector2(0,275));
           karakterAtes = false;
        }
    }
   
}
