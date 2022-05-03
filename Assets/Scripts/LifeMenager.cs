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
    public GameObject myCanvas;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = playerMaxLife;
        CreateProperties();
        playerLife = playerMaxLife;

        if (GameObject.FindGameObjectWithTag("GameScene"))
        {
            myCanvas = GameObject.FindGameObjectWithTag("GameScene");
        }
        else
        {
            Debug.Log(myCanvas);
            Debug.Log("Erro");
        }
        
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
        object scorePLayer;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Score", out scorePLayer) && myCanvas)
        {

            myCanvas.GetComponent<Text>().text = "Score: " + (int)scorePLayer;
        }
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
