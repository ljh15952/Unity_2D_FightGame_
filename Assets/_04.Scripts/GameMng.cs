using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    public static GameMng Instance;
    public UIMng uiMng;
    public GameObject Hero1; // Right
    public GameObject Hero2; // Left

    int round;
    bool isStart;
    public bool isonline;
    float timeAttack = 99;

    private void Awake()
    {
        round = 1;
        SetPlayerActive(false);
        Instance = this;
        uiMng.PlayRoundStart();
    }

    private void Update()
    {
        if (!isonline)
        {
            if (isStart)
            {
                if (Hero1.transform.position.x < Hero2.transform.position.x)
                {
                    Hero1.GetComponent<Player>().TurnRight();
                    Hero2.GetComponent<Player>().TurnLeft();
                    Hero1.GetComponent<Player>().isturn = false; //중화가 추가
                    Hero1.GetComponent<Player>().isturn = false; //중화가 추가

                }
                else
                {
                    Hero1.GetComponent<Player>().TurnLeft();
                    Hero2.GetComponent<Player>().TurnRight();
                    Hero1.GetComponent<Player>().isturn = true; //중화가 추가
                    Hero2.GetComponent<Player>().isturn = true;//중화가 추가
                }
            }
        }
        else
        {
            if (isStart)
            {
                if (Hero1.transform.position.x < Hero2.transform.position.x)
                {
                    Hero1.GetComponent<Player_Photon>().TurnRight();
                    Hero2.GetComponent<Player_Photon>().TurnLeft();
                    Hero1.GetComponent<Player_Photon>().isturn = false; //중화가 추가
                    Hero1.GetComponent<Player_Photon>().isturn = false; //중화가 추가

                }
                else
                {
                    Hero1.GetComponent<Player_Photon>().TurnLeft();
                    Hero2.GetComponent<Player_Photon>().TurnRight();
                    Hero1.GetComponent<Player_Photon>().isturn = true; //중화가 추가
                    Hero2.GetComponent<Player_Photon>().isturn = true;//중화가 추가
                }
            }
        }
    }

    public void SetPlayerActive(bool _value)
    {
        if (!isonline)
        {
            Hero1.GetComponent<Player>().enabled = _value;
            Hero2.GetComponent<Player>().enabled = _value;
        }
        else
        {
            Hero1.GetComponent<Player_Photon>().enabled = _value;
            Hero2.GetComponent<Player_Photon>().enabled = _value;
        }
    }
    public int GetRound()
    {
        return round;
    }
    public void SetStart(bool _value)
    {
        isStart = _value;
    }
    public bool GetStart()
    {
        return isStart;
    }
}
