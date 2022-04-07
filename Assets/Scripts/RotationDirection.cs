using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDirection : MonoBehaviour
{
    
    [SerializeField]  PlayerController move;
    Transform tr;
    [SerializeField] float offset = 90f;

    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        if (move.MovimentoXGet() != 0 || move.MovimentoYGet() != 0) { 
        float angle = Mathf.Atan2(move.MovimentoXGet(), move.MovimentoYGet()) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.AngleAxis(angle + offset,tr.forward);
        }
    }
}
