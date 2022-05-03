using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Player Config", menuName ="Nacoes/BodyData")]

public class BodyData : ScriptableObject
{
    public Sprite characterIcon;
    public string nationName;
    public GameObject prefabName;
}
