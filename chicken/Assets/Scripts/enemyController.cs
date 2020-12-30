using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 
public class enemyController : MonoBehaviour
{
    AudioSource sRaudio;
    public Animator animator;
    public LayerMask layermask;

    GameObject[] gidilecekNoktalar;
    GameObject karakter;

    public GameObject kursuneffects;
    public GameObject kursun;
    public GameObject effects;
    public Transform kursunCikicakYer;

    int aradakiMesafeSayaci = 0;
    int hiz = 5;
    int can = 100;
    

    float mermiSaniyesi = 0;

    bool aradakiMesafeKontrol = true;
    bool ileriGeriKontrol = true;
    bool dusmanHareket = true;

    Vector3 aradakiMesafe;
    
    Vector3 karkterLocal;


    RaycastHit2D ray;
    
    
    void Start()
    {
        sRaudio = GetComponent<AudioSource>();
        karakter = GameObject.FindGameObjectWithTag("Player");
        
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
                gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            if (gidilecekNoktalar[i].transform.name!="KursunCikicakYer")
            {
                gidilecekNoktalar[i].transform.SetParent(transform.parent);
            }
               
        }
        
    }

    
    void Update()
    {
        if(can<=0)
        {
            karakter.GetComponent<chicken>().oldurulenDusmanSayisi++;
            Instantiate(effects, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
        if (ray.collider != null && ray.collider.tag == "Player")
        {
            
            
            animator.SetBool("enemyShot", true);
            animator.SetBool("enemyRun", false);
          if(ray.collider.tag=="Player")
            {
                dusmanHareket = false;
                mermiSaniyesi += Time.deltaTime;
                if (mermiSaniyesi > 1f)
                {
                    atesET();
                    mermiSaniyesi = 0;
                }
            }
           
            
            
        }
     
        else 
        {
           
            dusmanHareket = true;
            animator.SetBool("enemyShot", false);
          
        }   
    }
     void FixedUpdate()
    {
        beniGordugunde();
       
        if (dusmanHareket)
        {
            noktalaraGitGel();
        }
        
        
    }
    void atesET()
    {
            Vector2 gidilecekYer = karakter.transform.position-transform.position;
            GameObject bullets = Instantiate(kursun, kursunCikicakYer.position, kursunCikicakYer.rotation);
            bullets.GetComponent<enemyEggController>().dusmanaDogruHareket = gidilecekYer;
    }
  
    void beniGordugunde()
    {
       
            Vector3 rayYonum = karakter.transform.position - transform.position;
            ray = Physics2D.Raycast(transform.position, rayYonum, 10, layermask);
            Debug.DrawLine(transform.position, ray.point, Color.white);
      

    }
    void noktalaraGitGel()
    {
        if (aradakiMesafeKontrol)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeKontrol = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        animator.SetBool("enemyRun",true);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            
            aradakiMesafeKontrol = true;
            if (ileriGeriKontrol)
            {
                aradakiMesafeSayaci++;
            }
            else
            {
                aradakiMesafeSayaci--;
            }
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ileriGeriKontrol = false;
                
            }
            else if (aradakiMesafeSayaci == 0)
            {
                ileriGeriKontrol = true;
               
            }
            if(gidilecekNoktalar[aradakiMesafeSayaci].transform.position.x>transform.position.x)
            {
                
                transform.localScale = new Vector3(-(0.2f), 0.2f, 0);
            }
            else if (gidilecekNoktalar[aradakiMesafeSayaci].transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(0.2f, 0.2f, 0);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="yumurta")
        {
            sRaudio.Play();
            Instantiate(kursuneffects, transform.position, transform.rotation);
            can =can - 35;
        }
    }



#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount-1; i++)
        {

              
            
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
            
             
            
           
        }
        for (int i = 0; i < transform.childCount - 2; i++)
        {
            
          
            
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
            
           
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(enemyController))]
[System.Serializable]
class dusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        enemyController script = (enemyController)target;
        if (GUILayout.Button("Üret", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeni = new GameObject();
            yeni.transform.parent = script.transform;
            yeni.transform.position = script.transform.position;
            yeni.name = script.transform.childCount.ToString();
        }
        // dışarıya bir değişken açmak için editörün içine bunları yazmalıyız.
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("animator"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("kursunCikicakYer"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("effects"));
                     EditorGUILayout.PropertyField(serializedObject.FindProperty("kursuneffects"));
                     serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
        // bu aralığa kadar. 
    }
}
#endif
