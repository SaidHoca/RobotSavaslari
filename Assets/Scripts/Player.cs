using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
}



