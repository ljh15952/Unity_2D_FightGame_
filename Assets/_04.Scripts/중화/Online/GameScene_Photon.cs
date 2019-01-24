using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene_Photon : Photon.MonoBehaviour
{

    public GameObject[] communicators = new GameObject[2];

    public Player P1;
    public Player P2;

    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            communicators[0] = GameObject.FindGameObjectWithTag("MASTER");
            communicators[1] = GameObject.FindGameObjectWithTag("COMM");
        }
        else
        {
            communicators[0] = GameObject.FindGameObjectWithTag("CLIENT");
            communicators[1] = GameObject.FindGameObjectWithTag("COMM");
        }
        for(int i=0;i<2;i++)
        {
            communicators[i].GetComponent<communicate_script>().SetGM();
        }
    }

   
}
