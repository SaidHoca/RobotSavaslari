using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using System;


public class ArenaScript : MonoBehaviour
{
    public static ArenaScript instance;

    public string playerName, oppenentName;
    public int playerCan, oppenentCan;
    public int playerZirh, oppenentZirh;
    public int playerSaldiri, oppenentSaldiri;
    public int playerImage, oppenentImage;
    public int oppenentId,playerId;
    public Text playerNameTxt, opponentNameTxt, playerCanTxt, opponentCanTxt;
    public Image playerImg, opponentImg;
    public Sprite robotresim1, robotresim2, robotresim3, robotresim4, robotresim5;
    int baslangicSirasi;
    public GameObject blueRocket, redRocket;
    bool atesEt = false;
    bool savasDevamEdiyor = true;


    void Awake()
    {
        MakeInstance();
        System.Random rnd = new System.Random();  // bu ve alttaki satır debug işleminde silinecek çünkü openent ayarla kodu bunu ayarlıyor..
        baslangicSirasi = rnd.Next(1, 3);
        
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        if (atesEt && savasDevamEdiyor)
        {
            StartCoroutine(Rocket());
        }
    }



    public void playerAyarla()
    {
        playerName = Player.instance.playername;
        playerCan = Player.instance.robotcan;
        playerZirh = Player.instance.robotzirh;
        playerSaldiri = Player.instance.robotsaldiri;
        playerImage = Player.instance.robotimage;
        playerId = Convert.ToInt32( Player.instance.id );
            
    }

    public void oppenentAyarla()
    {
        playerAyarla();
        StartCoroutine(CountUser());
        System.Random rnd = new System.Random();
        // oyuna kimin başlayacağı konusunda zar atıyoruz..
        baslangicSirasi =  rnd.Next(1, 3);
        
    }

    public IEnumerator CountUser()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Post("http://saidhoca.com/GetCount.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Contains("0"))
                {
                    Debug.Log("Try Again");
                }
                else
                {
                    // if we logged correctly
                    int sayi = Convert.ToInt32(www.downloadHandler.text.ToString());  // toplam oyuncu sayısını aldık...
                    System.Random rnd = new System.Random();
                    // oppenentid sini playerid sinde farklı olacak şekilde belirliyoruz.. 
                    do
                    {
                        oppenentId = rnd.Next(1, sayi + 1);
                    } while (oppenentId == playerId);


                    // şimdi belirlediğimiz id ye göre yukarda gerekli olan oppenent bilgilerini çekiyoruz..
                    StartCoroutine(GetUser(oppenentId.ToString()));
                    // ateş etme işleminin fitilini ateşliyoruz..
                    StartCoroutine(SavasBaslat());
                    
                }
            }
        }
    }

    public IEnumerator GetUser(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("oppenentID", id);
        using (UnityWebRequest www = UnityWebRequest.Post("http://saidhoca.com/GetOpponent.php", form))
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
                jsonstring = jsonstring.Replace("[", "");

                JSONObject playerJson = (JSONObject)JSON.Parse(jsonstring);
                oppenentName = playerJson["playername"];
                oppenentCan = playerJson["robotcan"];
                oppenentZirh = playerJson["robotzirh"];
                oppenentSaldiri = playerJson["robotsaldiri"];
                oppenentImage = playerJson["robotimage"];

                SetUiText();
                SetUiImage(playerImage,oppenentImage);
                

                Debug.Log(oppenentName + oppenentCan + oppenentZirh);

            }
        }
    }


    public void SetUiText()
    {
        // UI LARI AYARLIYORUZ..
        playerNameTxt.text = playerName;
        opponentNameTxt.text = oppenentName;
        playerCanTxt.text = playerCan.ToString();
        opponentCanTxt.text = oppenentCan.ToString();     

    }

    public void SetUiImage(int playerResim, int opponentResim)
    {

        // player image belirliyoruz...
        if ( playerResim== 1)
        {
            playerImg.sprite = robotresim1;
        }
        else if (playerResim == 2)
        {
            playerImg.sprite = robotresim2;
        }
        else if (playerResim == 3)
        {
            playerImg.sprite = robotresim3;
        }
        else if (playerResim == 4)
        {
            playerImg.sprite = robotresim4;
        }
        else if (playerResim == 5)
        {
            playerImg.sprite = robotresim5;
        }


        // opponent image belirliyoruz..
        if (opponentResim == 1)
        {
            opponentImg.sprite = robotresim1;
        }
        else if (opponentResim == 2)
        {
            opponentImg.sprite = robotresim2;
        }
        else if (opponentResim == 3)
        {
            opponentImg.sprite = robotresim3;
        }
        else if (opponentResim == 4)
        {
            opponentImg.sprite = robotresim4;
        }
        else if (opponentResim == 5)
        {
            opponentImg.sprite = robotresim5;
        }
    }


    public IEnumerator Rocket()
    {
        atesEt = false;
        yield return new WaitForSeconds(1.2f);
        if (baslangicSirasi == 1)
        {
            // birinci oyuncu saldırıyor..
            GameObject blue = Instantiate(blueRocket,new Vector3(-5f,0.3f,0f),Quaternion.identity);
            blue.GetComponent<Rigidbody2D>().velocity = new Vector2(10,0);


            baslangicSirasi = 2;
        }
        else if (baslangicSirasi == 2)
        {
            // ikinci oyuncu saldırıyor...

            GameObject red = Instantiate(redRocket, new Vector3(5f, 0.3f, 0f), Quaternion.identity);
            red.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);

            baslangicSirasi = 1;
        }

        if(playerCan > 0 && oppenentCan > 0)
        {
            atesEt = true;
   
        }
      
    }

    public void CanAzalt(string mermi)
    {
        if(mermi == "blue")
        {    if ( oppenentCan > 0 && playerCan > 0)
            {
                oppenentCan = oppenentCan - playerSaldiri + oppenentZirh / 2;
                opponentCanTxt.text = "Cânı : " + oppenentCan;
            }
            else
            {
                SavasBitir();
            }                      

        }else if (mermi == "red")
        {
            if ( playerCan > 0 && playerCan >0)
            {
                playerCan = playerCan - oppenentSaldiri*5 + playerZirh / 2;
                playerCanTxt.text = "Cânım : " + playerCan;
            }
            else
            {
                SavasBitir();
            }           
        }
    }

    public IEnumerator SavasBaslat()
    {      
        yield return new WaitForSeconds(2f);
        atesEt = true;      
    }

    public void SavasBitir()
    {
        savasDevamEdiyor = false;
        atesEt = false;
    }

}
