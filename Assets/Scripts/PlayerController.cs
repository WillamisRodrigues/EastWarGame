using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerController : MonoBehaviour
{
    public float moveCurrentV;
    [SerializeField] 
    float offsetToStopMovementCompletely = 0.5f;
    public float moveCurrentH;
    Vector2 moveFinal;
    public float addVelocity = 2f;
    public float rmVelocity = 2f;
   

   

    public Rigidbody2D rb;
    Vector2 control;
    public PhotonView photonView;

    public GameObject bulletInstance;
    public Transform bulletSpawnPoint;

    public string playerNickName;



    
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        
        playerNickName = photonView.Owner.NickName;


    }


    // Update is called once per frame
    void Update()
    {
       
        
        if (photonView.IsMine)
        {
            MoveInput();
            IncreaseVelocity();
            DecreaseVelocity();
            OffsetToStop();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }

        }
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

        rb.MovePosition(rb.position + moveFinal * Time.fixedDeltaTime);

    }

    public float MovimentoXGet()
    {
        return moveFinal.x;
    }

    public float MovimentoYGet()
    {
        return moveFinal.y;
    }

    void IncreaseVelocity()
    {
        //se pressionar para horizontal aumentar velocidade
        if(control.x > 0)
        {
            moveCurrentH += addVelocity * Time.deltaTime;
        }
       else if (control.x < 0)
        {
            moveCurrentH -= addVelocity *  Time.deltaTime;
        }

        //se pressionar para vertical aumentar a velocidade
        if (control.y > 0)
        {
            moveCurrentV += addVelocity * Time.deltaTime;
        }
        else if (control.y < 0)
        {
            moveCurrentV -= addVelocity *  Time.deltaTime;
        }
    }
    
    void DecreaseVelocity()
    {
        //se não pressionar para horizontal decrementar velocidade
        if (control.x == 0)
        {

            if(moveCurrentH > 0)
            {
                moveCurrentH -= rmVelocity * Time.deltaTime;
            }
            else if(moveCurrentH < 0)
            {
                moveCurrentH += rmVelocity *  Time.deltaTime;
            }

           
        }

        //se não pressionar para vertical decrementar velocidade
        if (control.y == 0)
        {

            if (moveCurrentV > 0)
            {
                moveCurrentV -= rmVelocity * Time.deltaTime;
            }
            else if (moveCurrentV < 0)
            {
                moveCurrentV += rmVelocity *  Time.deltaTime;
            }


        }



    }
    

    private void Shoot()
    {

        object myId;
        PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Id", out myId);
        photonView.RPC("ShootRPC", RpcTarget.All, (string)myId);
    }

    [PunRPC]
    public void ShootRPC(string createrId)
    {
        GameObject tempBullet = Instantiate(bulletInstance, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        tempBullet.GetComponent<BulletController>().createrName = photonView.Owner.NickName;

        tempBullet.GetComponent<BulletController>().createrId = createrId;
    }


    public void sufferDamage(float value, string createrId)
    {
        photonView.RPC("sufferDamagerRPC", RpcTarget.AllBuffered, value, createrId);
    }

    

    

   
    void OffsetToStop()
    {
        //se não pressionar para horizontal e está no offset para parar completamente
        if (control.x == 0)
        {
            if (moveCurrentH < offsetToStopMovementCompletely && moveCurrentH > -offsetToStopMovementCompletely)
            {
                moveCurrentH = 0;
            }

        }

        //se não pressionar para vertical e está no offset para parar completamente
        if (control.y == 0)
        {
            if (moveCurrentV < offsetToStopMovementCompletely && moveCurrentV > -offsetToStopMovementCompletely)
            {
                moveCurrentV = 0;
            }
        }
    }


}
