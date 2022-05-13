using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConfigInterface : MonoBehaviour
{
    [SerializeField]
    PhotonView photonView;
    [SerializeField]
    LifeMenager life;

    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine)
        {
            if (GameObject.FindObjectOfType<MenuInGame>() != null)
            {
                GameObject.FindObjectOfType<MenuInGame>().GetComponent<MenuInGame>().SetLife(life);
            }
        }
    }

}
