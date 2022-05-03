using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDirection : MonoBehaviour
{

    Transform tr;
    [SerializeField] PlayerController plController;
    [SerializeField] float rotationSpeed = 40f;

    private void Start()
    {
        tr = transform;
    }

    void Update()
    {
        Vector3 movementDirection;
        movementDirection.z = 0;
        movementDirection.y = -plController.MovimentoYGet();
        movementDirection.x = -plController.MovimentoXGet();


        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            tr.rotation = Quaternion.Slerp(tr.rotation, toRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
