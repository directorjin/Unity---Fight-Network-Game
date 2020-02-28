using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
//
//*** このscriptはUserのauthorizationをする。***
//
{

    public bool IsFirebaseReady { get; private set; } //Firebaseが用意されているかを確認するための変数
    public bool IsSignInOnProgress { get; private set; } //Signinが実行中かどうかを確認するための変数

    public InputField emailField; //emailのUI
    public InputField passwordField; // passwordのUI
    public Button signInButton; // ButtonのUI

    public static FirebaseApp firebaseApp; // firebase APPをcontrolするための変数
    public static FirebaseAuth firebaseAuth; //firebase authorizationをcontrolするための変数

    public static FirebaseUser User; //FirebassのUserを保存するための変数

    public Text networInfoText;

    public void Start()
    {
        signInButton.interactable = false; //最初はDisable。なぜならまだFirebase NetWorkの作動確認ができていない。
        networInfoText.text = "Wait for Firebase..";
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>//Firebase NetworkをCheckする。問題があれば自動で再度接続する。
        //*関数がAsync関数なので、ChainやCallbackが必要 それで後ろにContinueWithOnMainThreadを使用。*
        {

            var result = task.Result; //Firebaseの結果を変数resultに保存

            if (result != DependencyStatus.Available) //Dependencyに問題が生じた場合
            {
                Debug.LogError(message: result.ToString()); //Logを表示
                IsFirebaseReady = false;//Firebaseが準備できていない。

                networInfoText.text = "Firebaseが準備できていない。";
            }
            else //Dependencyに問題がない場合
            {
                IsFirebaseReady = true; //Firebaseが準備される。

                firebaseApp = FirebaseApp.DefaultInstance; //Firebase APP変数がFirebase APPのinstanceとなる。
                firebaseAuth = FirebaseAuth.DefaultInstance;//Firebase APP変数がFirebase APPのinstanceとなる。

                networInfoText.text = "Firebaseが準備される。";
            }

            //条件文をはみ出して
            signInButton.interactable = IsFirebaseReady; //Firebaseの状態をボタンの有効化の有無で伝達。
        });
    }

    //----------------------------------------------------
    #region My Method(SignIn(),....)
    public void SignIn() //Action, if sighButton is clicked
    {
        if (!IsFirebaseReady || IsSignInOnProgress || User != null)//Firebaseが未用意の場合(case#1) or SingInProgressが進行中の場合(case#2) or Userが空いている場合(case#3)
        {
            return; //SignInが進行しない。
        }

        //正常な進行の場合(上記の条件文を通過する場合)
        IsSignInOnProgress = true; //進行中の状態に変更
        signInButton.interactable = false; //再ボタンを押すことができないように


        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(//Emailとpasswordをcheck
            (task) =>
        {
            Debug.Log(message: $"Sign In status : {task.Status}"); //進行状況を示す

            //**IsSignInOnProgress = false;
            //**signInButton.interactable = true;

            if (task.IsFaulted) // 一致しない場合
            {
                Debug.LogError(task.Exception); //Errorを表示

            }
            else if (task.IsCanceled) // 不明なエラーでキャンセルになった場合
            {
                Debug.LogError(message: "Sign-in canceled"); //Errorを表示
            }
            else //正常に作動する場合
            {
                User = task.Result; //taskのResultをuserに伝達
                Debug.Log(User.Email); //Emailを表示
                SceneManager.LoadScene("Lobby"); //Localで移動しなければならないので平凡にLoadScene Methodを使用
            }

            IsSignInOnProgress = false;//進行中でない状態に変更
            signInButton.interactable = true;//ボタンのClickができるように変更
        });

        //**firebaseAuth.SignInWithCredentialAsync    //GooglePlayとAppleIDを利用したログインに必要なMethod(まだ未使用)

    }
    #endregion
    //----------------------------------------------------
}