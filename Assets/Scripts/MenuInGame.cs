using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    public GameObject MenuVitoria;
    public GameObject MenuDerrota;
    bool endgame;

    [SerializeField]
    Text pontos;
    [SerializeField]
    LifeMenager playerLife;

    // Start is called before the first frame update
    void Start()
    {
        MenuVitoria.SetActive(false);
        MenuDerrota.SetActive(false);
        endgame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pontos != null)
        {
            pontos.text = playerLife.GetPontos().ToString();
        }


    }

    public void SetLife(LifeMenager lifeMen)
    {
        playerLife = lifeMen;
    }


    public void OpenMenuEndGame(bool loose)
    {
        MenuDerrota.SetActive(loose);
        MenuVitoria.SetActive(!loose);
    }

    public void ClickReturn()
    {
        Application.Quit();
    }

}