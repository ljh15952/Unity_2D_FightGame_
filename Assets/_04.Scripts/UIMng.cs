using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class HpInfo
{
    public Image Hp;
    public Image HpBack;

    public float hpTimer;
}

public class UIMng : MonoBehaviour {

    public HpInfo p1HpInfo;
    public HpInfo p2HpInfo;

    public float hpTimerLimit;

    public Text startText;
    public Text Count;
    public Text FirstWin;
    public Text SecondWin;
    public Text Equal;

    bool isDoing = true;
    bool end;


    public int nCount=99;

    private void Update()
    {
        PlayHpTimer(p1HpInfo);
        PlayHpTimer(p2HpInfo);

      //  p1HpInfo.Hp.fillAmount; // player1 hp 퍼센트 0 ~ 1;
        //p2HpInfo.Hp.fillAmount; // player2 hp 퍼센트 0 ~ 1;
        if(nCount==0)
        {
            if(p1HpInfo.Hp.fillAmount > p2HpInfo.Hp.fillAmount)
                FirstWin.gameObject.SetActive(true);
            if(p1HpInfo.Hp.fillAmount < p2HpInfo.Hp.fillAmount)
                SecondWin.gameObject.SetActive(true);
            if (p1HpInfo.Hp.fillAmount == p2HpInfo.Hp.fillAmount)
                Equal.gameObject.SetActive(true);
            end = true;
        }
        if(p1HpInfo.Hp.fillAmount<=0)
        {
            end = true;
            SecondWin.gameObject.SetActive(true);
            SecondWin.rectTransform.DOScale(2, 5);
            SecondWin.DOFade(0, 5);
        }
        if(p2HpInfo.Hp.fillAmount<=0)
        {
            end = true;
            FirstWin.gameObject.SetActive(true);
        }

    }

    void PlayHpTimer(HpInfo _info)
    {
        if (_info.hpTimer >= hpTimerLimit)
        {
            _info.HpBack.fillAmount -= Time.deltaTime;
            if (_info.HpBack.fillAmount <= _info.Hp.fillAmount)
            {
                _info.HpBack.fillAmount = _info.Hp.fillAmount;
                _info.hpTimer = 0;
            }
        }
        if (_info.HpBack.fillAmount > _info.Hp.fillAmount)
            _info.hpTimer += Time.deltaTime;
    }
    public void SetHp(Player player, int amount)
    {
        player.Hp += amount;
        if(player.ctrlType == CtrlType.One)
        {
            p1HpInfo.Hp.fillAmount = player.Hp / player.MaxHp;
            p1HpInfo.hpTimer = 0;
        }
        if (player.ctrlType == CtrlType.Two)
        {
            p2HpInfo.Hp.fillAmount = player.Hp / player.MaxHp;
            p2HpInfo.hpTimer = 0;
        }
    }
    public void SetHp(Player_Photon player, int amount)
    {
        player.Hp += amount;
        if (player.ctrlType == CtrlType.One)
        {
            p1HpInfo.Hp.fillAmount = player.Hp / player.MaxHp;
            p1HpInfo.hpTimer = 0;
        }
        if (player.ctrlType == CtrlType.Two)
        {
            p2HpInfo.Hp.fillAmount = player.Hp / player.MaxHp;
            p2HpInfo.hpTimer = 0;
        }
    }
    public void PlayRoundStart()
    {
        StartCoroutine("RoundStart");
    }
    IEnumerator RoundStart()
    {
        startText.DOFade(0, 0);
        startText.text = "Round " + GameMng.Instance.GetRound();
        startText.DOFade(1, 1);

        yield return new WaitForSeconds(1.5f);
        startText.DOFade(0, 0);
        startText.text = "<i>Fight!!!!</i>";
        startText.GetComponent<RectTransform>().DOScale(10, 0);
        startText.GetComponent<RectTransform>().DOScale(1, 0.25f);
        startText.DOFade(1, 0.25f);

        yield return new WaitForSeconds(0.5f);
        //
        StartCoroutine("Counting");
        startText.DOFade(0, 0.25f);
        GameMng.Instance.SetStart(true);
        GameMng.Instance.SetPlayerActive(true);

    }

    void RenewText()
    {
        Count.text = nCount.ToString();
    }

    IEnumerator Counting()
    {
        while (isDoing)
        {
            yield return new WaitForSeconds(1.0f);
            nCount--;
            RenewText();
            if (end)
                isDoing = false;
        }

    }
}
