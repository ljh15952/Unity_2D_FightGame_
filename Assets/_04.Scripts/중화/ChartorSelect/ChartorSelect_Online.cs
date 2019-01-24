using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ChartorSelect_Online : Photon.MonoBehaviour
{
    public ChartorSelect_Photon CSP;
    private void Awake()
    {
        AwakePhotonConnect();
    }

    void JoinLobby()
    {
        if (PhotonNetwork.JoinLobby())
        {    // 로비에 접속하면
            PhotonNetwork.JoinRandomRoom(); //랜덤방에 접속한다.
        }
        else
        {                                         // 로비에 접속이 실패하면
            JoinLobby();        // 다시 랜덤방에 접속한다.
        }
    }


    private void Update()
    {
        if(PhotonNetwork.connected)
        {
            Debug.Log("CONNECTED");
            CSP.UpdatePhoton();
        }
        else
        {
            Debug.Log("DISCONNECTEd");
        }

    }
    void AwakePhotonConnect()
    {
        // GameManager 스크립트의  Awke()에서 호출

        PhotonNetwork.ConnectUsingSettings("1.0");
        //포톤 네트워크에 접속한다.
    }

    public void BtnJoinRandomRoom()
    {  // 랜덤방에 접속한다.
        Debug.Log("ASDASD");
        JoinLobby();
    }

    public void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {   // 랜덤방이 없어서 접속에 실패하면
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);     // 스스로 방을 만든다. 
    }

    void OnJoinedRoom()
    {                     // 방에 접속했다.
        Debug.Log("OnJoinedRoom");

          CSP.PhotonGameSetting ();  // 게임 셋팅

    }

    void OnJoinedLobby()
    {
        Debug.Log("JoinedLobby:");    // 로비에 접속했다.
    }

}
