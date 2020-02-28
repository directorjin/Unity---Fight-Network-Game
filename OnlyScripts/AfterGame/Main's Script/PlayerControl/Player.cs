using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun //Photon ViewにアクセスするためにPunを相続する。
{
    private Rigidbody2D playerRigidbody; //RigidBody2DをscriptでControlするために
    private SpriteRenderer spriteRenderer; //spriteRenderをscriptでControlするために
    private HpController hpController;

    public Vector3 movement;

    public int userNameNumber;
    public float health = 0f; 

    public float speed = 3f; //Userの速度

    private bool isGameOver = false; //flag to see if game is over

    private void Start()
    {
        userNameNumber = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpController = GetComponent<HpController>();


        if(photonView.IsMine) // もし、制御するObjectがLocalのObjectであれば
        {
            spriteRenderer.color = Color.blue; //そしたらLocalは blue
            health = 1f;
        }
        else
        {
            spriteRenderer.color = Color.red; //それなら Remoteは red
            health = 1f;
        }
    }


    //----------------------------------------------------
    #region Player Move Method( contain *FixedUpdate()*)
    private void FixedUpdate() //プレイヤーの入力を処理
    {
        if (!photonView.IsMine) //もし制御するObjectがLocalでないなら、
        {
            return; //下記のCodeを実行できないようにreturn
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MoveToPlayer(h, v);
    }

    void MoveToPlayer(float h, float v) //入力値を利用してプレイヤーを移動させる。
    { 
        movement.Set(h, v, 0);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }
    #endregion
    //----------------------------------------------------


    //----------------------------------------------------
    #region Damage Method
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.name == "Damage(debug)" && health > 0)
        {
            hpController.DamageHP();
            
        }
        else
        {
            
        }
    }
    #endregion
    //----------------------------------------------------

    public void GameOver()
    {
        Debug.Log("GameOver");
    }

    
}
