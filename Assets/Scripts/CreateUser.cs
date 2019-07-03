using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CreateUser : MonoBehaviour
{
    public InputField userNameField;
    public InputField userPassField;
    
    string userName, userPass;
    int robotType;



    public void Olustur(int tip)
    {
        
        if ( userNameField.text.ToString() == "" || userPassField.text.ToString() == "")
        {
            Debug.Log("Lütfen alanları doğru doldurunuz..");
        }
        else
        {
            userName = userNameField.text.ToString();
            userPass = userPassField.text.ToString();
            robotType = tip;
            StartCoroutine(RegisterUser(userName, userPass, tip));
            
        }
        
    }

    public IEnumerator RegisterUser(string username, string password,int tip)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("robotImage", tip);

        using (UnityWebRequest www = UnityWebRequest.Post("http://saidhoca.com/registeruser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                AnimControlScript.instance.createAnim.SetBool("CreateAnim", false);
                AnimControlScript.instance.garajAnim.SetBool("GarajAnim", true);
               
            }
        }
    }

}
