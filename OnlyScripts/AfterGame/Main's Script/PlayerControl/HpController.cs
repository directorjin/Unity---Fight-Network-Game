using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HpController : MonoBehaviourPunCallbacks, IPunObservable
{
    //
        //***  このscriptはプレイヤーの体力に関するものを管理します。***
    // 

    public Image healthFill;
    public float playerHP = 1f;
    public float damage = 0.011f;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //----------------------------------------------------
    #region HP Control
    public void DamageHP()
    {
        playerHP -= damage;
        healthFill.fillAmount = playerHP;
        if (playerHP <= 0)
        {
            gameObject.GetComponent<Player>().GameOver(); //HPが0以下になるとゲームオーバー
        }

    }
    #endregion
    //----------------------------------------------------


    //----------------------------------------------------
    #region Hp synchronization
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {         
            stream.SendNext(playerHP);//送る内容はHPのvalue
        }
        else
        {
            this.playerHP = (float)stream.ReceiveNext();//受け取る内容はHPの(float)value
        }
    }
    #endregion
    //----------------------------------------------------
}



