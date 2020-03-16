using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun  //Photon ViewにアクセスするためにPunを相続する。
{
    private Rigidbody2D playerRigidbody; //RigidBody2DをscriptでControlするために
    private SpriteRenderer spriteRenderer; //spriteRenderをscriptでControlするために
    private HpController hpController;
    private Vector3 movement;


    [Header("Player State")]
    [SerializeField]
    private int userNameNumber;
    public float health = 0f; 
    public float speed = 3f; //Userの速度


    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 1500;
    public float bulletPostion;
    public float bulletReroadTime = 1;
    private float bulletReroad;


    private bool isGameOver = false; //flag to see if game is over


    private Camera mainCamera;
    

    private void Start()
    {
        ComponentSetting();
        

        if (photonView.IsMine) // もし、制御するObjectがLocalのObjectであれば
        {//そしたらLocalは
            spriteRenderer.color = Color.blue; 
            gameObject.tag = "Player";
            health = 1f;
        }
        else
        {//それなら Remoteは
            spriteRenderer.color = Color.red; 
            gameObject.tag = "Enemy";
            health = 1f;
        }
    }


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
    

    #region Damage Method
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Damage(debug)" && health > 0)
        {
            photonView.RPC("DamageHP", RpcTarget.All, 0.001f);      
        }
        else
        {
            
        }
    }

    
    #endregion


    #region Fire Method
    private void Update()
    {
        if (photonView.IsMine) // もし、制御するObjectがLocalのObjectであれば
        {
            if(bulletReroad>0)
            {
                bulletReroad -= Time.deltaTime;
            }
            if(bulletReroad<0)
            {
            bulletReroad = 0;
            }

            if (Input.GetMouseButtonDown(0) && bulletReroad == 0)
            {
                
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
                Vector3 playerPosition = transform.position;
                Vector3 fireDirection = (mousePosition - playerPosition).normalized; //プレーヤーがマウスの方向を眺めるベクトル
                

                GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, playerPosition + (fireDirection * bulletPostion), transform.rotation); //
                bullet.GetComponent<Rigidbody2D>().AddForce(fireDirection * bulletSpeed, ForceMode2D.Force);

                bulletReroad = bulletReroadTime;
            }
            
        }
        
    }

    #endregion

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
    private void ComponentSetting()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>(); //camera find
        userNameNumber = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        Debug.Log(userNameNumber);//一番目のplayer = 0、 二番目のplayer = 1

        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpController = GetComponent<HpController>();
        bulletPostion = 2.0f;
        bulletReroad = 0f;
    }

    
}
