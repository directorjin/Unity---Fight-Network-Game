using UnityEngine;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviour
//
//*** このscriptはUserのEmailをUIに表示する。***
//
{


    private Text nameText; //名前を表示する変数

    private void Start()
    {
        nameText = GetComponent<Text>();//今のText ComponentをnameTextに割り当て 
        //*今、ScriptのComponentのアドレスを持ってきた状態*

        if (AuthManager.User != null) //User変数はSignInSceneで宣言された変数。 UserはUserの情報が割り当てられている。
        {
            nameText.text = $"{AuthManager.User.Email}";//Interpolated strings($)を使用してUserのEmailをnameText.text(このTextComponent)に表示。
        }
        else
        {
            nameText.text = "Test ID";
            //nameText.text = $"Network ERROR. {AuthManager.User}";
        }
    }
}
