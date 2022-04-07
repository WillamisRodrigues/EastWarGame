using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviour
{
    public float moveCurrentV;
    public float moveCurrentH;
    Vector2 moveFinal;
    public float addVelocity = 2f;
    public float rmVelocity = 2f;
    public GameObject myCanvas;

    public Image lifeBar;
    public float playerMaxLife = 100f;
    float playerLife;

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
        playerLife = playerMaxLife;

        playerLife = playerMaxLife;
        playerNickName = photonView.Owner.NickName;



    }

    // Update is called once per frame
    void Update()
    {
       //funcao com erro UpdateCanvasScore();
        
        if (photonView.IsMine)
        {
            MoveInput();
            IncreaseVelocity();
            DecreaseVelocity();

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

    void UpdateCanvasScore()
    {
        object scorePLayer;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Score", out scorePLayer) && myCanvas)
        {
            myCanvas.GetComponentInChildren<Text>().text = "Score " + (int)scorePLayer;
        }
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
    void LifeManager(float value)
    {
        playerLife += value;
        if (playerLife > playerMaxLife)
            playerLife = playerMaxLife;

        if (playerMaxLife < 0)
            playerLife = 0;

        lifeBar.fillAmount = playerLife / playerMaxLife;

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

    [PunRPC]
    public void sufferDamagerRPC(float value, string createrId)
    {
        LifeManager(-value);
        UpdateScorePlayer(createrId);
    }

    void UpdateScorePlayer(string playerId)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            object id;

            player.CustomProperties.TryGetValue("Id", out id);
            
            if ((string)id == playerId)
            {
                object scoreTemp;

                player.CustomProperties.TryGetValue("Score", out scoreTemp);

                int newScore = (int)scoreTemp + 50;

                Hashtable playerTempData = new Hashtable();
                playerTempData.Add("Score", newScore);
                player.SetCustomProperties(playerTempData);


            }
        }
    }


}
