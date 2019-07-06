using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;


public class login : MonoBehaviour
{
    public InputField UserNameInput;
    public InputField PasswordInput;
    public Text isim, can, zirh, hiz, saldiri, para;
    public Image robotresim;
    public Sprite robotresim1, robotresim2, robotresim3, robotresim4, robotresim5;
    


    public void loginBtn()
    {
        string name = UserNameInput.text.ToString();
        string pass = PasswordInput.text.ToString();
        StartCoroutine(LoginUser(name,pass));
        
    }

    public IEnumerator LoginUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        using (UnityWebRequest www = UnityWebRequest.Post("http://www.saidhoca.com/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {                         
                if (www.downloadHandler.text.Contains("wrong credentials") || www.downloadHandler.text.Contains("username does not exist"))
                {
                    Debug.Log("Try Again");
                }
                else
                {
                    // if we logged correctly
                    string id = www.downloadHandler.text.ToString();
                    Player.instance.SetPlayer(username, password, id);
                    StartCoroutine(GetUser(id));
                    // animasyonlar falan filan burada yapılacak..   
                    AnimControlScript.instance.garajAnimasyon(true);
                    AnimControlScript.instance.loginAnimasyon(false);
                }
            }
        }
    }


    public IEnumerator GetUser(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", id);
        using (UnityWebRequest www = UnityWebRequest.Post("http://www.saidhoca.com/getusersinfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                string jsonstring = www.downloadHandler.text;
                // j son cümlesinin başında ve sonunda gelen köşeli parantezler php den çekilen veriden kaynaklı
                // ve burada objeye çevrilmesine engel oluyor.. bu sebeple bunu düzenliyoruz önce....
                jsonstring = jsonstring.Replace("]", "");
                jsonstring = jsonstring.Replace("[" , ""); 
                
                JSONObject playerJson = (JSONObject)JSON.Parse(jsonstring);
                Player.instance.para = playerJson["para"];
                Player.instance.robotcan = playerJson["robotcan"];
                Player.instance.robotzirh = playerJson["robotzirh"];
                Player.instance.robotsaldiri = playerJson["robotsaldiri"];
                Player.instance.robothiz = playerJson["robothiz"];
                Player.instance.robotimage = playerJson["robotimage"];

                isim.text = Player.instance.playername;
                can.text = "Can : " + Player.instance.robotcan.ToString();
                zirh.text = "Zırh : " + Player.instance.robotzirh.ToString();
                hiz.text = "Speed : " + Player.instance.robothiz.ToString();
                saldiri.text = "Saldırı : " + Player.instance.robotsaldiri.ToString();
                para.text = "Bakiye : " + Player.instance.para.ToString() + " paracık";
                RobotResimBelirle(Player.instance.robotimage);

            }
        }
    }

    public void RobotResimBelirle(int resimno)
    {
        if (resimno == 1)
        {
            robotresim.sprite = robotresim1;
        }else if (resimno == 2)
        {
            robotresim.sprite = robotresim2;
        }
        else if (resimno == 3)
        {
            robotresim.sprite = robotresim3;
        }
        else if (resimno == 4)
        {
            robotresim.sprite = robotresim4;
        }
        else if (resimno == 5)
        {
            robotresim.sprite = robotresim5;
        }
    }

}
