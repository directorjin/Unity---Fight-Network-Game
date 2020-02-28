using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks //punの機能を使用するためにMonoBehaviourPunCallbacksを相続した状態
{
    //
        //*** このscriptはゲームの全般的なものを管理します。 詳しくはregionを参考にしてください。***
    // 

    #region SingleTone Definition
    private static GameManager instance;

    public static GameManager Instance //SingleToneで作る"GameManager's Instance"宣言
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }
    #endregion


    public Text scoreText; //点数
    public Transform[] spawnPositions; //Playerの生成位置
    public GameObject playerPrefab; //生成するPlayerの'ProtoType Prefab'
    
    
    private int[] playerScores; //点数を保存するint配列

    private void Start()
    {
        playerScores = new[] { 0, 0 }; //初期の点数は0、0
        SpawnPlayer(); //各Playerが1回ずつSpawn Player Methodを実行する。


        if (PhotonNetwork.IsMasterClient)//もしHostなら
        {  
        }
    }

    //----------------------------------------------------
    #region Room Management
    public override void OnLeftRoom() //ルームを出る場合
    {

        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }
    #endregion
    //----------------------------------------------------


    //----------------------------------------------------
    #region Player Management
    private void SpawnPlayer()//Playerを生成する場合
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1; //1や2にActorNumberが入ってくるが、内部的には0と1で処理するために
        var spawnPosition = spawnPositions[localPlayerIndex];//Userのspawnの位置を保存  %[localPlayerIndex % spawnPositions.Length]
        
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation); //Localでの生成、RemoteでのUser生成のためのMethod
            //注意すべき部分はResourceにplayerPrefabのnameのようなものが必要。 そうすると、instantiate Methodが同じ名前をResourceで探して生成する。

    }
    #endregion
    //----------------------------------------------------



    #region Not yet implemented
    /*
    public void AddScore(int playerNumber, int score)//点数を取る場合
    {

        if(PhotonNetwork.IsMasterClient) //HostだけがMethodを有意義に使用できるようにするために
        {
            return;
        }

        playerScores[playerNumber - 1] += score;

        photonView.RPC("RPCUpdateScoreText",RpcTarget.All,playerScores[0].ToString(),playerScores[1].ToString());//Roomに参加した全てのPlayerがRPCudate Score Text(変数種類: RPC)を呼び出す。
                                                                                                                 //Remote Procedure Call
  
    }
    
    
    [PunRPC]
    private void RPCUpdateScoreText(string player1ScoreText, string player2ScoreText)
    {
        
        scoreText.text = $"{player1ScoreText} : {player2ScoreText}";
    }
    */
    #endregion
}