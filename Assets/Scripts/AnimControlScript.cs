using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimControlScript : MonoBehaviour
{

    public static AnimControlScript instance;
    public GameObject tintPanel,olusturulduPanel,savassonuPanel;
    public Animator loginAnim, createAnim, garajAnim,arenaAnim,tintAnim,olusturulduAnim,savasSonuAnim;
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

            TintAnimasyon();
            garajAnim.SetBool("GarajAnim", true);
        }else if (!durum)
        {

            TintAnimasyon();
            garajAnim.SetBool("GarajAnim", false);
        }
    }

    public void createAnimasyon(bool durum)
    {
        if (durum)
        {
            // burada login panelindeki create tuşuna basınca yapılanlar

            TintAnimasyon();
            createAnim.SetBool("CreateAnim", true);
            loginAnim.SetBool("LoginAnim", false);
        }
        else if (!durum)
        {
            // burada create panelindeki geri tuşuna basılınca yapılanlar

            TintAnimasyon();
            createAnim.SetBool("CreateAnim", false);
            loginAnim.SetBool("LoginAnim", true);
        }
    }

    public void loginAnimasyon(bool durum)
    {
        if (durum)
        {

            TintAnimasyon();
            loginAnim.SetBool("LoginAnim", true);
        }
        else if (!durum)
        {

            TintAnimasyon();
            loginAnim.SetBool("LoginAnim", false);
        }
    }

    public void arenaAnimasyon(bool durum)
    {
        if (durum)
        {
            TintAnimasyon();
            arenaAnim.SetBool("Arena", true);
            garajAnim.SetBool("GarajAnim", false);
        }
        else if (!durum)
        {
            
            TintAnimasyon();
            arenaAnim.SetBool("Arena", false);
            garajAnim.SetBool("GarajAnim", true);
        }
    }

    public void TintAnimasyon()
    {
        tintPanel.SetActive(true);
        tintAnim.SetTrigger("Tint");
    }


    public void OyuncuOlusturulduAnim()
    {
        olusturulduPanel.SetActive(true);
        olusturulduAnim.SetTrigger("Tint");
    }

    public void SavasSonuAnim()
    {
        savassonuPanel.SetActive(true);
        savasSonuAnim.SetTrigger("Tint");
    }
}
