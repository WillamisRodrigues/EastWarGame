using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera cameraPlayer;

    public void CameraSet(Transform player)
    {
        cameraPlayer.Follow = player;
        cameraPlayer.LookAt = player;
    }
}
