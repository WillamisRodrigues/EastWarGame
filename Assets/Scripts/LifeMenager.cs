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
    float playerLife;
    
    int scorePLayer;

    [SerializeField]
    Text pontos;


    // Start is called before the first frame update
    void Start()
    {

        playerLife = playerMaxLife;
       CreateProperties();
        playerLife = playerMaxLife;


        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCanvasScore();
    }

    void LifeManager(float value)
    {
        playerLife += value;
        if (playerLife > playerMaxLife)
            playerLife = playerMaxLife;

        if (playerMaxLife < 0)
            playerLife = 0;

        lifeBar.fillAmount = playerLife / playerMaxLife;

    }

    void UpdateCanvasScore()
    {
        

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Score", out scorePLayer) && myCanvas)
        {

            myCanvas.GetComponent<Text>().text = "Score: " +  scorePLayer;

           
        }
    } 

  /* void UpdateCanvasScore()
    {
        if(pontos != null)
        pontos.text = "Score: " + scorePLayer;
    } */

    public int GetPontos()
    {
        return scorePLayer ;
        //mostrar pontos verificar
    }

    [PunRPC]
    public void sufferDamagerRPC(float value, string createrId)
    {
        LifeManager(-value);
       UpdateScorePlayer(createrId);
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
        Hashtable playerTempData = new Hashtable();

        playerTempData.Add("Name", PhotonNetwork.NickName);
        playerTempData.Add("Score", 0);
        playerTempData.Add("Id", PhotonNetwork.LocalPlayer.UserId);
        playerTempData.Add("Team", "Blue");
        PhotonNetwork.SetPlayerCustomProperties(playerTempData);
    }


}
