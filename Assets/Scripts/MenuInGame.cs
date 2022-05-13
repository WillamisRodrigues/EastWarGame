using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInGame : MonoBehaviour
{
    public GameObject MenuVitoria;
    public GameObject MenuDerrota;

    [SerializeField]
    Text pontos;
    
    [SerializeField]
    LifeMenager playerPontos ;

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
            if (playerPontos.GetPontos() >= 100)
            {
                MenuVitoria.SetActive(true);
            }
               pontos.text = playerPontos.GetPontos().ToString();
        }
            
    }

    public void SetLife(LifeMenager lifeMen)
    {
        playerPontos = lifeMen;
    }
}
