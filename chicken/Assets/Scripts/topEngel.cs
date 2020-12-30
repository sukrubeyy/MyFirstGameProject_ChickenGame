using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class topEngel : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;

    int gidilecekNoktalarSayac=0;
    int donmeSekli = 300;

    bool ileriKontrol = true;
    bool aradakiMesafeKontrol=true;

    Vector3 aradakiMesafe;

    int engelinGitmeHizi = 3;
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
        transform.Rotate(new Vector3(0, 0, donmeSekli * Time.deltaTime));
    }
    void FixedUpdate()
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
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[gidilecekNoktalarSayac].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * engelinGitmeHizi;
        if(mesafe<0.5f)
        {
            aradakiMesafeKontrol = true;
            if(ileriKontrol)
            {
                gidilecekNoktalarSayac++;
                donmeSekli = -300;
            }
            else
            {
                gidilecekNoktalarSayac--;
                donmeSekli = 300;
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
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for(int i=0; i<transform.childCount-1; i++)
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(topEngel))]
[System.Serializable]
class topEngelEditor:Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        topEngel script = (topEngel)target;
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
