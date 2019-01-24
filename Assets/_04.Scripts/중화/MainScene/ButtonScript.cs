using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonScript : MonoBehaviour {

    public AudioSource on;
    public AudioSource click;

    public enum efx
    {
        mouseon = 1,
        click = 2
    }



    public void Button_Mouse_Enter(GameObject Obj)
    {
        Obj.GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1);
        Obj.GetComponent<Image>().color = new Color32(255, 255, 255, 240);

        EffectManager(efx.mouseon);
    }
    public void Button_Mouse_Out(GameObject Obj)
    {
        Obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Obj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

    }

    public void EffectManager(efx e)
    {
        if (e == efx.click)
        {
            click.Play();
        }
        else if (e == efx.mouseon)
        {
            on.Play();
        }
    }


    public void Select_Arcade_Mode()
    {
        Debug.Log("GameMode : Arcede");
        Debug.Log("LoadScene : Select Chartor");
        EffectManager(efx.click);
      
    }

    public void Select_2Player_Mode()
    {
        Debug.Log("GameMode : 2Player");
        Debug.Log("LoadScene : Select Chartor");
        EffectManager(efx.click);
        CharatorSelectScript.isOnline = false;
        SceneManager.LoadScene("ChartorSelectScene");

    }

    public void Select_Practice_Mode()
    {
        Debug.Log("GameMode : Practice");
        Debug.Log("LoadScene : Select Chartor");
        EffectManager(efx.click);

    }

    public void Select_Option_bt()
    {
        Debug.Log("Show_Panal");
        EffectManager(efx.click);

    }

    public void Exit()
    {
        Debug.Log("QUIT");
        Application.Quit();
        EffectManager(efx.click);

    }

    public void Online()
    {
        Debug.Log("ONLINE");
        EffectManager(efx.click);
        CharatorSelectScript.isOnline = true;
        SceneManager.LoadScene("ChartorSelectScene");

    }


}
