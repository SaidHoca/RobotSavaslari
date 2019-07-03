using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControlScript : MonoBehaviour
{

    public static AnimControlScript instance;
    public Animator loginAnim, createAnim, garajAnim,arenaAnim;
    // bu animatörlerin hepsinde bool tanımlı ve login anim true olarak, diğerleri false olarak başlıyor..

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

    public void garajAnimasyon(bool durum)
    {
        if (durum)
        {
            garajAnim.SetBool("GarajAnim", true);
        }else if (!durum)
        {
            garajAnim.SetBool("GarajAnim", false);
        }
    }

    public void createAnimasyon(bool durum)
    {
        if (durum)
        {
            // burada login panelindeki create tuşuna basınca yapılanlar
            createAnim.SetBool("CreateAnim", true);
            loginAnim.SetBool("LoginAnim", false);
        }
        else if (!durum)
        {
            // burada create panelindeki geri tuşuna basılınca yapılanlar
            createAnim.SetBool("CreateAnim", false);
            loginAnim.SetBool("LoginAnim", true);
        }
    }

    public void loginAnimasyon(bool durum)
    {
        if (durum)
        {
            loginAnim.SetBool("LoginAnim", true);
        }
        else if (!durum)
        {
            loginAnim.SetBool("LoginAnim", false);
        }
    }

    public void arenaAnimasyon(bool durum)
    {
        if (durum)
        {
            arenaAnim.SetBool("Arena", true);
            garajAnim.SetBool("GarajAnim", false);
        }
        else if (!durum)
        {
            arenaAnim.SetBool("Arena", false);
            garajAnim.SetBool("GarajAnim", true);
        }
    }





}
