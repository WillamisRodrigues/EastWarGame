using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuInGame : MonoBehaviour
{
    public GameObject MenuVitoria;
    public GameObject MenuDerrota;

    [SerializeField]
    Text pontos;
    
    [SerializeField]
    LifeMenager playerLife;

    // Start is called before the first frame update
    void Start()
    {
        MenuVitoria.SetActive(false);
        MenuDerrota.SetActive(false);
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

    public void MenuVit(bool menu)
    {
        if (menu)
        {
            MenuVitoria.SetActive(true);
        }
        else
        {
            MenuVitoria.SetActive(false);
        }
            
    }

    public void MenuDer(bool menu)
    {
        if (menu)
        {
            MenuDerrota.SetActive(true);
        }
        else
        {
            MenuDerrota.SetActive(false);
        }

    }
}
