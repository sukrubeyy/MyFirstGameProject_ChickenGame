using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class anaMenuYoneticisi : MonoBehaviour
{
    GameObject leveller;
    public GameObject[] levelButonlari;
    void Start()
    {
        leveller = GameObject.FindGameObjectWithTag("Leveller");
        for (int i = 0; i < leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("kacinciLevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }

    }
    void Update()
    {
       
    }
    
    public void basla()
    {
        SceneManager.LoadScene("1");
    }
   public void levellerGoster()
    {
        for(int i=0; i<leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(true);
            
        }
        for(int i=0; i<PlayerPrefs.GetInt("kacinciLevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
       
    }
   public void cikis()
    {
        Application.Quit();
    }
    public void GelenButton(int gelenbutton)
    {
        if(gelenbutton==1)
        {
            SceneManager.LoadScene("1");
        }

        else if(gelenbutton==2)
        {
            SceneManager.LoadScene("2");
        }
        else if(gelenbutton==3)
        {
            SceneManager.LoadScene("3");
        }
    }
    public void resetle()
    {
        PlayerPrefs.DeleteAll();
    }
    public void yoneticiler()
    {
        Application.OpenURL("https://www.instagram.com/hybridgamestudio/");
    }
}
