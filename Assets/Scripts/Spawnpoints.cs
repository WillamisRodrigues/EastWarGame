using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawnpoints : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints = new Transform[2];
    public GameObject myPlayer;
    bool PlayerCheck = true;

    [SerializeField]
    public GameObject GameWindow;
    public PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

            CreateNewPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateNewPlayer()
    {
        Transform spawnPoint = this.transform;

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[i].UserId)
            {
                spawnPoint = spawnPoints[i];
            }
        }

        if (spawnPoint != null)
        {
            if(PlayerCheck == true) { 

            GameObject temPlayer = PhotonNetwork.Instantiate(myPlayer.name, spawnPoint.position, spawnPoint.rotation, 0);
                temPlayer.GetComponent<LifeMenager>().myCanvas = GameWindow;
                PlayerCheck = false;
            }

        }
       
    }

    
}
