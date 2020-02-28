using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks //punの機能を使用するためにMonoBehaviourPunCallbacksを相続した状態
//
//*** このscriptはLobbyをControlしてMatchMakingをする。***
// 
{
    private readonly string gameVersion = "1"; //NetWorkの上でUserが互いにGameのVersionが合っているかを確認するための変数

    
    public Text connectionInfoText;
    public Button joinButton;

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion; //現在GameのVersionをNetworkに登録する。
        PhotonNetwork.ConnectUsingSettings(); //Settingを実行するためのMethod

        joinButton.interactable = false;//Master Serverに接続した後、trueに変えるため
        connectionInfoText.text = "マスターサーバーに接続中です。 少々お待ちください。";
    }

    //----------------------------------------------------
    #region Master Sever management
    public override void OnConnectedToMaster() //もしMaster Serverが接続された場合、CallbackされるMethod
    {
        joinButton.interactable = true; //接続成功なのでtrue
        connectionInfoText.text = "接続に成功しました。";
    }

    public override void OnDisconnected(DisconnectCause cause)//もしMaster Serverが接続が切れた場合、CallbackされるMethod
    {
        joinButton.interactable = false; //接続失敗なのでFalse
        connectionInfoText.text = $"接続が切断されました。 理由 : {cause.ToString()}";//接続が切れた理由を表示

        PhotonNetwork.ConnectUsingSettings(); //再接続をするMethod
    }
    #endregion
    //----------------------------------------------------

    //----------------------------------------------------
    #region Room Management
    public override void OnJoinRandomFailed(short returnCode, string message) //もし上記のConnect MethodでRandom Roomの接続に失敗した場合、実行されるCall BackされるMethod
    {
        connectionInfoText.text = "部屋がありません。 新しいルームを作っています。";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });//nullは部屋の名前を意味。Random Matching Systemなのでnullに設定。 1:1代前なので、MaxPlayersが2人

    }

    public override void OnJoinedRoom()//Roomに接続した場合にcallbackされるMethod
    {
        connectionInfoText.text = "ルームに接続に成功しました。"; //Userが確認できないメッセージ
        PhotonNetwork.LoadLevel("Main");//HostとGuestがSyncronized Sceneを一緒にLoadするためのMethod
    }
    #endregion

    //------------------------------------------------------




    //----------------------------------------------------
    #region My Method(Connect(), ...)
    public void Connect() //JoinButtonがClickされた時のMethod。                *PhotonのMethodではない。*
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)//Codeの安全のための条件文。 もし押すと同時にネットワークの接続が切れた場合のために、
        {
            connectionInfoText.text = "ランダムなルームに接続中です。";
            PhotonNetwork.JoinRandomRoom(); //RandomなRoomに接続を試みるMethod

        }
        else //分からない理由でClickを押す時に接続が切れた場合は再接続を試みる。
        {
            connectionInfoText.text = "接続に失敗しました。 もう一度試します。";
            PhotonNetwork.ConnectUsingSettings(); //再接続をするMethod
        }
    }
    #endregion
    //------------------------------------------------------
}