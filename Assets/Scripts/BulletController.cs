using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletController : MonoBehaviour
{
    public string createrName;
    public string createrId;
    public float speed = 50f;
    Rigidbody2D bulletBody;
    float bulletLifeTime = 7f;
    float bulletLifeTimeCount = 0f;


    // Start is called before the first frame update
    void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(transform.up * speed);


    }

    // Update is called once per frame
    void Update()
    {
        LifeTimeController();
    }

    void LifeTimeController()
    {
        if(bulletLifeTimeCount >= bulletLifeTime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            bulletLifeTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D quemColidiu)
    {
        if (quemColidiu.gameObject.GetComponent<PlayerController>())
        {
            PlayerController playerColidiu = quemColidiu.gameObject.GetComponent<PlayerController>();

            if (playerColidiu.playerNickName != createrName)
            {
                playerColidiu.sufferDamage(10f, createrId);
                //verificar GetComponent<PhotonView>().RPC("DestroyBullet", RpcTarget.All);
               Destroy(this.gameObject);
               
            }
        }
        else
        {

            if (quemColidiu.collider.CompareTag("Parede") && quemColidiu != null)
            {
                if(quemColidiu.gameObject.GetComponent<LifeMenager>()){
                    quemColidiu.gameObject.GetComponent<LifeMenager>().sufferDamagerRPC(10f, "");
                }
             
                // Debug.Log(quemColidiu.collider.name);
                Destroy(this.gameObject);
            }
        }
     
    }


    [PunRPC]
    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }


}
