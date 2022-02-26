using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    public Transform tr;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        RotationP();
    }

    void RotationP()
    {
        //pega a posi��o do mouse
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        //transforma a posi��o do mouse em posi��o do mundo
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        //calcula a dist�ncia do mouse em rela��o ao player
        lookPos = lookPos - transform.position;
        //calcula o �ngulo que o jogador deve rotacionar em rela��o ao mouse
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        //Rotaciona o player
        tr.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
