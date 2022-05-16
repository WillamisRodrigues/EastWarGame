using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LifeMenager : MonoBehaviour
{
    public Image lifeBar;
    public float playerMaxLife = 100f;

    [SerializeField]
    float playerLife;

    [SerializeField]
    PhotonView photonView;

    object scorePLayer;

    [SerializeField]
    Text pontos;

    Hashtable playerTempData = new Hashtable();


    // Start is called before the first frame update
    void Start()
    {

        playerLife = playerMaxLife;
       CreateProperties();

        if(photonView != null)
        {
            if (photonView.IsMine)
            {
                if (GameObject.FindGameObjectWithTag("GameScene"))
                {
                    pontos = GameObject.FindGameObjectWithTag("GameScene").GetComponent<Text>();
                }
            }
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCanvasScore();
        
    }

    void LifeManager(float value)
    {

        photonView.RPC("DanoRPC", RpcTarget.AllBuffered, value);

        if (playerLife <= 0)
        {
            photonView.RPC("DestroyParedeRPC", RpcTarget.AllBuffered);
            photonView.RPC("chamaMenu", RpcTarget.AllBuffered);

        }


       

    }

    [PunRPC]
    void DestroyParedeRPC()
    {
        Destroy(this.gameObject);
    }
    [PunRPC]
    void DanoRPC( float value)
    {
        playerLife += value;
        if (playerLife > playerMaxLife)
            playerLife = playerMaxLife;

        if (playerMaxLife <= 0)

            playerLife = 0;
        lifeBar.fillAmount = playerLife / playerMaxLife;
    }

    
    [PunRPC]
    void chamaMenu()
    {
        Menu();
    }



    void Menu()
    {
        if(photonView != null) {

            if (photonView.IsMine)
            {
                if (gameObject.CompareTag("Player"))
                {
                    if (playerLife <= 0)
                    {
                        if (GameObject.FindObjectOfType<MenuInGame>())
                        {
                            GameObject.FindObjectOfType<MenuInGame>().GetComponent<MenuInGame>().MenuDer(true);
                        }
                    }

                    if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Score", out scorePLayer))
                    {
                        if ((int)scorePLayer >= 300)
                        {
                            if (GameObject.FindObjectOfType<MenuInGame>())
                            {
                                GameObject.FindObjectOfType<MenuInGame>().GetComponent<MenuInGame>().MenuVit(true);
                            }
                        }
                    }
                }
                   

            }

        }
           

            
        
    }

    public float GetLife()
    {
        
        return playerLife;
    }

    public int GetPontos()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Score", out scorePLayer))
            return (int)scorePLayer;
        else
            return 0;
    }

    void UpdateCanvasScore()
    {
        if(photonView != null)
        {
            if (photonView.IsMine)
            {

                if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Score", out scorePLayer))
                {

                    pontos.text = "Score: " + scorePLayer;


                }
            }
        }
        
    } 


  

    [PunRPC]
    public void sufferDamagerRPC(float value, string createrId)
    {
        LifeManager(-value);

        if(createrId != "")
        {
            UpdateScorePlayer(createrId);
        }
      
    }



   void UpdateScorePlayer(string playerId)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            object id;

            player.CustomProperties.TryGetValue("Id", out id);

            if ((string)id == playerId)
            {
                object scoreTemp;

                player.CustomProperties.TryGetValue("Score", out scoreTemp);

                int newScore = (int)scoreTemp + 50;

                Hashtable playerTempData = new Hashtable();
                playerTempData.Add("Score", newScore);
                player.SetCustomProperties(playerTempData);


            }
        }
    }

    void CreateProperties()
    {
       

        playerTempData.Add("Name", PhotonNetwork.NickName);
        playerTempData.Add("Score", 0);
        playerTempData.Add("Id", PhotonNetwork.LocalPlayer.UserId);
        playerTempData.Add("Team", "Blue");
        PhotonNetwork.SetPlayerCustomProperties(playerTempData);
    }


}
