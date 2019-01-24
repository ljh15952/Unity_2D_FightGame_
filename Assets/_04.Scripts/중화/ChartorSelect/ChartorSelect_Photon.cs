using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

enum TARGET
{
    MASTER = 0,
    CLIENT = 1,
}



public class ChartorSelect_Photon : Photon.MonoBehaviour {

    public GameObject[] communicators = new GameObject[2];
    // 생성된 플레이어들의 객체를 넣어줌
    public bool bNetWarStart = false;     // 네트워크 전투가 시작됐는지 

    public GameObject PhotonPrefab;

    public GameObject[] IMAEGS;
    public CharatorSelectScript CSS;

    public void PhotonGameSetting()
    {   // 게임 시작 시 셋팅

        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Master Login2:");
            communicators[0] = PhotonNetwork.Instantiate(PhotonPrefab.name, new Vector3(-5.506f, 3.039f, 0f), Quaternion.identity, 0);
            communicators[0].transform.tag = "MASTER";
            IMAEGS[0].SetActive(true);
        }
        else
        {
            Debug.Log("Client Login2:");
            communicators[1] = PhotonNetwork.Instantiate(PhotonPrefab.name, new Vector3(5.1f, 3.039f, 0f), Quaternion.identity, 0);
            communicators[1].transform.tag = "CLIENT";
            IMAEGS[1].SetActive(true);

        }

        StartCoroutine(WaitPlayer());   // 대전 상대 기다리기 
    }

    IEnumerator WaitPlayer()
    {    // 대전 상대 기다리기 

        while (!bNetWarStart)
        {

            GameObject tmpComm = GameObject.FindGameObjectWithTag("COMM");

            if (PhotonNetwork.isMasterClient)
            {

                if (tmpComm && tmpComm.GetComponent<PhotonView>().viewID == 2001)
                {    // 내가 방장일 때 유저가 들어옴
                    bNetWarStart = true;
                    IMAEGS[1].SetActive(true);

                    communicators[1] = tmpComm;
                    Debug.Log("client come in");
                }

            }
            else
            {
                if (tmpComm && tmpComm.GetComponent<PhotonView>().viewID == 1001)
                {   // 내가 방원일 때 방장 정보가 들어옴 
                    bNetWarStart = true;
                    communicators[0] = tmpComm;
                    Debug.Log("master come in");
                    IMAEGS[0].SetActive(true);
                }
            }
            yield return null;
        }
    }

    public void OnPhotonPlayerConnected(PhotonPlayer other)         // 다른 유저가 접속했을 때 
    {
        Debug.Log("other player connect");
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer other)      // 상대의 접속이 끊겼을 때 
    {
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("other player disconnected");

        }
        else
        {
            Debug.Log("master disconnected");

        }

        StartCoroutine(LeaveEnd(4f));
    }

    public IEnumerator LeaveEnd(float ftime)
    {
        yield return new WaitForSeconds(ftime);
        LeaveRoom();
    }

    public void LeaveRoom()
    {
        communicators[0] = null;
        communicators[1] = null;

        IMAEGS[0].SetActive(false);
        IMAEGS[1].SetActive(false);

        bNetWarStart = false;




        PhotonNetwork.LeaveRoom();
    }

    public void UpdatePhoton()
    {      // GameManager 의 Update() 함수에서 돌아감 
        if (!bNetWarStart)
            return;
      
        if(PhotonNetwork.isMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("1");
                CSS.P1ClickLeft();
                SendMessege2target(0);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("2");
                CSS.P1ClickRight();
                SendMessege2target(1);

            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                CSS.P1ClickReady();
                SendMessege2target(4);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("3");
                CSS.P2ClickLeft();
                SendMessege2target(2);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("4");
                CSS.P2ClickRight();
                SendMessege2target(3);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                CSS.P2ClickReady();
                SendMessege2target(5);
            }
        }
    }

    //내가 방장일때 왼쪽 클라일때 오른쪽 **
    //방장일때 AD 클라일때 화살표  **
    //방장일때 화살표누르면 아무것도되면안됨 **
    //클라일때 AD누르면 아무것도되면안됨 **
    //방장일때 AD누르면 사진이랑바뀌고(내화면) 클라입장의화면에도 방장쪽사진이바뀌어야됨
    //클라일때 화살표누르면 사진바뀌고 방장입장의화면의 내사진바뀌어야함 

    public void SendMessege2target(int SendTarget)
    {
        communicators[0].GetComponent<PhotonView>().RPC("SendInfo", PhotonTargets.Others, SendTarget); //상대한테찍히는거같은데?
    }

    public void NetAttackDamage()
    {
        int SendTarget;

        if (PhotonNetwork.isMasterClient)
            SendTarget = (int)TARGET.CLIENT; //자기가 마스터면 클라한테보냄
        else 
            SendTarget = (int)TARGET.MASTER; //자기가 클라면 마스터한테보냄

        communicators[0].GetComponent<PhotonView>().RPC("SendInfo", PhotonTargets.Others, SendTarget); //상대한테찍히는거같은데?

    }

    public void SendProcess(int SendTarget) //상대의 함수임
    {
        switch(SendTarget)
        {
            case 0:
                CSS.P1ClickLeft();
                break;
            case 1:
                CSS.P1ClickRight();
                break;
            case 2:
                CSS.P2ClickLeft();
                break;
            case 3:
                CSS.P2ClickRight();
                break;
            case 4:
                CSS.P1ClickReady();
                break;
            case 5:
                CSS.P2ClickReady();
                break;
        }
    }

}

