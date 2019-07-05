using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public  web web;
    public string id;
    public string playername;
    public string playerpass;
    public int para;
    public int robotcan;
    public int robotzirh;
    public int robotsaldiri;
    public int robothiz;
    public int robotimage;
    public Text canText, paraText, zirhText, saldiriText;
    public bool durum = false;


    void Awake()
    {
        MakeInstance();

    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetPlayer(string name, string pass, string id)
    {
        playername = name;
        playerpass = pass;
        this.id = id;
    }

    public void ParaGuncelle(string id , int para)
    {
        StartCoroutine(ParaGuncelleWeb(id, para));
    }

    public IEnumerator ParaGuncelleWeb(string id,int paramiz)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("para", paramiz);
        

        using (UnityWebRequest www = UnityWebRequest.Post("http://saidhoca.com/updatemoney.php", form))
        {
            yield return www.SendWebRequest();
            
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Para artırma hatası !!!");
            }
            else
            {
                Debug.Log("Para artırıldı");
                // para güncellemesi doğru bir şekilde çalştığında yapılacaklar.. kazandı yazısı gibi birşey olabilir..             
            }
        }
    }

    public IEnumerator SaldiriGuncelleWeb()
    {

        WWWForm form = new WWWForm();
        robotsaldiri += 5;
        form.AddField("saldiri", robotsaldiri);
        form.AddField("id", id);
        using (UnityWebRequest www = UnityWebRequest.Post("http://saidhoca.com/updateattack.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Saldırı güncellenemedi !!!!");
            }
            else
            {
                Debug.Log("Saldırı güncellendi");
                
                         
            }
        }
    }

    public IEnumerator CanGuncelleWeb()
    {

        WWWForm form = new WWWForm();
        robotcan += 10;
        form.AddField("can", robotcan);
        form.AddField("id", id);
        using (UnityWebRequest www = UnityWebRequest.Post("http://saidhoca.com/updatehealth.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Can güncellenemedi !!!!");
            }
            else
            {
                Debug.Log("Can güncellendi");
                

            }
        }
    }

    public IEnumerator ZirhGuncelleWeb()
    {

        WWWForm form = new WWWForm();
        robotcan += 6;
        form.AddField("zirh", robotzirh);
        form.AddField("id", id);
        using (UnityWebRequest www = UnityWebRequest.Post("http://saidhoca.com/updatehealth.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("ZIRH güncellenemedi !!!!");
            }
            else
            {
                Debug.Log("ZIRH güncellendi");
               
            }
        }
    }

    public void CanGuncelle()
    {
        
        if ( para >= 30)
        {
            StartCoroutine(CanGuncelleWeb());
            para = para - 30;
            StartCoroutine(ParaGuncelleWeb(id, para));
            paraText.text = "Bakiye : " +  para.ToString() + "Para";
            canText.text ="Robot Can :" + robotcan.ToString();
            durum = false;
        }
        
    }
}



