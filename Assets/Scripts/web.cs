using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class web : MonoBehaviour
{


    public IEnumerator GetUser(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", id);
        Debug.Log(id);
        using (UnityWebRequest www = UnityWebRequest.Post("http://saidhoca.com/getusersinfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //string jsonstring = www.downloadHandler.text;
                //JSONObject playerJson = (JSONObject)JSON.Parse(jsonstring);
                //Debug.Log(playerJson["robotcan"]);

            }
        }
    }
}
