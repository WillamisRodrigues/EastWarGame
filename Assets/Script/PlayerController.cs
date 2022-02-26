using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveCurrentV;
    public float moveCurrentH;
    Vector2 moveFinal;
    public float moveSpeed = 2f;
    public Rigidbody2D rb;
    Vector2 control;
  
  

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        IncreaseVelocity();
        DecreaseVelocity();



    }

    private void FixedUpdate()
    {
        MovePosition();
    }

    void MoveInput()
    {
        control.x = Input.GetAxisRaw("Horizontal");
        control.y = Input.GetAxisRaw("Vertical");
    }

    void MovePosition()
    {
        moveFinal.x = moveCurrentH;
        moveFinal.y = moveCurrentV;

        rb.MovePosition(rb.position + control * moveFinal * Time.fixedDeltaTime);


        
    }

    void IncreaseVelocity()
    {
        //se pressionar para horizontal aumentar velocidade
        if(control.x > 0)
        {
            moveCurrentH += Time.deltaTime;
        }
       else if (control.x < 0)
        {
            moveCurrentH -= Time.deltaTime;
        }

        //se pressionar para vertical aumentar a velocidade
        if (control.y > 0)
        {
            moveCurrentV += Time.deltaTime;
        }
        else if (control.y < 0)
        {
            moveCurrentV -= Time.deltaTime;
        }
    }
    
    void DecreaseVelocity()
    {
        //se não pressionar para horizontal decrementar velocidade
        if (control.x == 0)
        {

            if(moveCurrentH > 0)
            {
                moveCurrentH -= Time.deltaTime;
            }
            else if(moveCurrentH < 0)
            {
                moveCurrentH += Time.deltaTime;
            }

           
        }



    }



}
