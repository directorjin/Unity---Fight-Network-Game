using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NormalBulletScript : MonoBehaviourPun
{
    public float bulletSpeed = 1500f;// temp
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌");
        if (collision.gameObject.CompareTag("Wall"))
        {
            PhotonNetwork.Destroy(transform.gameObject);
            Debug.Log("제거완료");
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetPhotonView().RPC("DamageHP", RpcTarget.AllBuffered, 0.1f);
            Debug.Log("포톤뷰의 값");
            Debug.Log(photonView);

            //collision.gameObject.GetComponent<HpController>().DamageHP(0.1f);
            PhotonNetwork.Destroy(transform.gameObject);
        }
    }

    
}
