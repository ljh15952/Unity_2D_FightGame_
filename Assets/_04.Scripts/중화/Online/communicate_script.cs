using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicate_script : Photon.PunBehaviour, IPunObservable
{
    public new string name = "USER";                                     // 유저의 HP

    public GameObject GM;
    public GameObject[] GM2 = new GameObject[2];

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GM = GameObject.FindGameObjectWithTag("GM");
    }

    public void SetGM()
    {
        GM2[0] = GameObject.FindGameObjectWithTag("P1");
        GM2[1] = GameObject.FindGameObjectWithTag("P2");
    }

    [PunRPC]       // RPC 함수를 통하여 정보를 전달합니다. 
    public void SendInfo(int attackTarget)
    {
        // attackTarget 공격할 대상   damage 적에게 주는 데미지
        GM.GetComponent<ChartorSelect_Photon>().SendProcess(attackTarget); //상대의 함수호출이라능
        Debug.Log("ASDASD");

    }
    [PunRPC]       // RPC 함수를 통하여 정보를 전달합니다. 
    public void SendinGameInfo(int num,int who)
    {
        GM2[who].GetComponent<Player_Photon>().SendProcess(num);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // 우리 캐릭터의 정보를 다른 유저들에게 전송합니다. ^^
            stream.SendNext(this.name);
        }
        else
        {
            // 다른 유저가 정보를 받습니다. ^^
            this.name = (string)stream.ReceiveNext();
        }
    }
}
