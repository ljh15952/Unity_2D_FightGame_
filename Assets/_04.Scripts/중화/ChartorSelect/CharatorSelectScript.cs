using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class CharatorSelectScript : MonoBehaviour
{
    public GameObject P1;

    public GameObject P2;

    public Image[] ChartorsP1 = new Image[4];
    public Image[] ChartorsP2 = new Image[4];

    public Image BigImage_P1;
    public Image BigImage_P2;

    public RectTransform endSelectText;

    public RectTransform Ready_P1;
    public RectTransform Ready_P2;

    int P1Ct;
    int P2Ct;

    public bool P1_isReady;
    public bool P2_isReady;

    static public bool isOnline;

    public GameObject JoinBt;


    public GameObject[] IMAGES;

 

    void Start()
    {
        if(!isOnline)
        {
            IMAGES[0].SetActive(true);
            IMAGES[1].SetActive(true);
        }
        else
        {
            JoinBt.SetActive(true);
        }
        P1_isReady = false;
        P2_isReady = false;
        P1Ct = 1;
        P2Ct = 1;
        ChartorsP1[P1Ct - 1].color = new Color32(255, 255, 255, 255);
        ChartorsP2[P2Ct - 1].color = new Color32(255, 255, 255, 255);

       
    }

    void Update()
    {
        Select_ON(P1Ct, 0);
        Select_ON(P2Ct, 1);

        if (isOnline)
            return;


        InputKey_P1();
        
        InputKey_P2();


        PickChartor();
    }


    public void P1ClickReady()
    {
        P1_isReady = true;
        Ready_P1.DOScale(1, 0.5f);
        Ready_P1.GetComponent<Text>().DOFade(1, 0.5f);
        if (P1_isReady && P2_isReady)
            StartCoroutine(Delay());
    }

    public void P2ClickReady()
    {
        P2_isReady = true;
        Ready_P2.DOScale(1, 0.5f);
        Ready_P2.GetComponent<Text>().DOFade(1, 0.5f);
        if (P1_isReady && P2_isReady)
            StartCoroutine(Delay());
    }
    void PickChartor()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            P1ClickReady();
            //    P1 Pick Chartor
            //  1플레이어의 케릭터 정보를 넘겨줌
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            P2ClickReady();
            //    P2 Pick Chartor
            //     2플레이어의 케릭터 정보르 넘겨줌
        }

        if (P1_isReady && P2_isReady)
            StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        endSelectText.DOScale(1, 0.5f);
        endSelectText.GetComponent<Text>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(1);

        if (!isOnline)
            SceneManager.LoadScene("GameScene_AI");
        else
            SceneManager.LoadScene("GameScene_Online");

    }

    void ChangeBigImage(int num,int what)
    {
        if (what == 0)
            BigImage_P1.sprite = ChartorsP1[num - 1].sprite;
        else
            BigImage_P2.sprite = ChartorsP2[num - 1].sprite;

    }
 


    void Select_ON(int num, int what)
    {
        if (what == 0)
            ChartorsP1[num - 1].color = new Color32(255, 255, 255, 255);
        else
            ChartorsP2[num - 1].color = new Color32(255, 255, 255, 255);

    }

    void Select_OFF(int num, int what)
    {
        if (what == 0)
            ChartorsP1[num - 1].color = new Color32(100, 100, 100, 255);
        else
            ChartorsP2[num - 1].color = new Color32(100, 100, 100, 255);
    }

    public void P1ClickLeft()
    {
        if (P1Ct == 1)
        {
            P1.transform.Translate(new Vector3(0, -250 * 3, 0));

            Select_OFF(P1Ct, 0);
            P1Ct = 4;
            Select_ON(P1Ct, 0);

            ChangeBigImage(P1Ct, 0);

            return;
        }
        P1.transform.Translate(new Vector3(0, 250, 0));

        Select_OFF(P1Ct, 0);
        P1Ct--;
        Select_ON(P1Ct, 0);

        ChangeBigImage(P1Ct, 0);
    }
    public void P1ClickRight()
    {
        if (P1Ct == 4)
        {
            P1.transform.Translate(new Vector3(0, 250 * 3, 0));

            Select_OFF(P1Ct, 0);
            P1Ct = 1;
            Select_ON(P1Ct, 0);

            ChangeBigImage(P1Ct, 0);

            return;
        }
        P1.transform.Translate(new Vector3(0, -250, 0));

        Select_OFF(P1Ct, 0);
        P1Ct++;
        Select_ON(P1Ct, 0);

        ChangeBigImage(P1Ct, 0);
    }
    void InputKey_P1()
    {
        if (P1_isReady)
            return;
        if (Input.GetKeyDown(KeyCode.A))
            P1ClickLeft();
        else if (Input.GetKeyDown(KeyCode.D))
            P1ClickRight();
    }

    public void P2ClickLeft()
    {
        if (P2Ct == 1)
        {
            P2.transform.Translate(new Vector3(0, -250 * 3, 0));

            Select_OFF(P2Ct, 1);
            P2Ct = 4;
            Select_ON(P2Ct, 1);

            ChangeBigImage(P2Ct, 1);


            return;
        }
        P2.transform.Translate(new Vector3(0, 250, 0));

        Select_OFF(P2Ct, 1);
        P2Ct--;
        Select_ON(P2Ct, 1);

        ChangeBigImage(P2Ct, 1);
    }
    public void P2ClickRight()
    {
        if (P2Ct == 4)
        {
            P2.transform.Translate(new Vector3(0, 250 * 3, 0));

            Select_OFF(P2Ct, 1);
            P2Ct = 1;
            Select_ON(P2Ct, 1);

            ChangeBigImage(P2Ct, 1);


            return;
        }
        P2.transform.Translate(new Vector3(0, -250, 0));

        Select_OFF(P2Ct, 1);
        P2Ct++;
        Select_ON(P2Ct, 1);

        ChangeBigImage(P2Ct, 1);
    }
    void InputKey_P2()
    {
        if (P2_isReady)
            return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            P2ClickLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            P2ClickRight();
    }
}
