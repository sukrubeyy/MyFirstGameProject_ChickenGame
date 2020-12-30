using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class asansorController : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;

     int objeninHareketHizi = 2;
    int gidilecekNoktalarSayac = 0;

    bool ileriKontrol = true;
    bool aradakiMesafeKontrol = true;

    Vector3 aradakiMesafe;

    
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        for(int i=0; i<gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        noktalaraGitGel();
    }
    void noktalaraGitGel()
    {
        if(aradakiMesafeKontrol)
        {
            aradakiMesafe = (gidilecekNoktalar[gidilecekNoktalarSayac].transform.position - transform.position).normalized;
            aradakiMesafeKontrol = false;
        }
        float mesafe = Vector3.Distance(gidilecekNoktalar[gidilecekNoktalarSayac].transform.position, transform.position);
        transform.position += aradakiMesafe * objeninHareketHizi * Time.deltaTime;
        if(mesafe<0.5f)
        {
            aradakiMesafeKontrol = true;
            if(ileriKontrol)
            {
                gidilecekNoktalarSayac++;
            }
            else
            {
                gidilecekNoktalarSayac--;
            }
            if(gidilecekNoktalarSayac==gidilecekNoktalar.Length-1)
            {
                ileriKontrol = false;
            }

            else if(gidilecekNoktalarSayac==0)
            {
                ileriKontrol = true;
            }
        }
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for(int i=0; i<transform.childCount-1; i++)
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
        for(int i=0; i<transform.childCount; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
    }
#endif
}


#if UNITY_EDITOR
[CustomEditor(typeof(asansorController))]
[System.Serializable]
class asansorControllerEditor:Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        asansorController script = (asansorController)target;
        if(GUILayout.Button("Üret"))
        {
            GameObject yeni = new GameObject();
            yeni.transform.parent = script.transform;
            yeni.transform.position = script.transform.position;
            yeni.transform.name = script.transform.childCount.ToString();

        }
    }
}
#endif