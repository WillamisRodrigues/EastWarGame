using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerCameraSettings : MonoBehaviour
{
    PhotonView photon;
    // Start is called before the first frame update
    void Start()
    {
        photon = GetComponent<PhotonView>();
        if (photon.IsMine)
        {
            CameraSettings();
        }
        
    }

    void CameraSettings()
    {
        if (GameObject.FindObjectOfType<CameraSettings>())
        {
            GameObject.FindObjectOfType<CameraSettings>().CameraSet(transform);
        }
    }
}
