using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class ChangeTheBackGround : MonoBehaviour
{
    public class MyFloatEnum //enumは整数だけを使うからクラスを作った。
    {
        public float max = (float)(200) / (float)(255); //赤色の最大
        public float min = (float)(100) / (float)(255); //赤色の最小
    }

    private MyFloatEnum colorState = new MyFloatEnum();//私が作ったクラスなのでオブジェクトを生成して使用する。
    private bool isColorChanging = false; //色が変わっているかどうかを判断

    public Image background;//かわるimage
    public float duration = 1f;// かかる時間設定。


    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>(); //現在のスクリプトの位置のObjectからImage Componentを取得。
        background.color = new Color((float)colorState.min, background.color.g, background.color.b);//バグを防ぐための初期化

    }
    

    IEnumerator ChangeColor(float state, float current)//このCoroutineは実行されると現在の状態と現在の値にimageの色を変更する。

    {
        float target = 0;//バグを防ぐための初期化


        //maxかminかによってtargetを設定する。-----------
        if (state == colorState.max)
        {
            target = (float)colorState.min;
        }
        else if(state == colorState.min)
        {
            target = (float)colorState.max;
        }
        //-----------------------------------------------

        float offset = (target - current) / duration; //間隔計算


        while (judgmentCurrentWay(current,target))//カラー値を変化させるWhile
        { 
            current += offset * Time.deltaTime;//値を計算
            background.color = new Color(current,background.color.g,background.color.b); // 値をRed Colorに伝達
            yield return new WaitForSeconds(0.01f);//このCoroutineが何秒ごとに実行されるのか
        }
        
        
        //値を正常化(整数の値を超える危険性があるため)----
        current = target;
        background.color = new Color(current, background.color.g, background.color.b);
        isColorChanging = false;
        //------------------------------------------------



        bool judgmentCurrentWay(float Acurrent, float Atarget)//色値が増えるか減少するかを判断するmethod
        {
            if(state == colorState.max)//maxの場合の判断→減少
            {
                if(Acurrent < Atarget){return false;}
                else { return true;}
            }
            else if(state == colorState.min)//minの時の判断 → 増加
            {
                if (Acurrent > Atarget) { return false; }
                else { return true;}
            }
            else{Debug.Log("Bug");return true; }//バグ
        }

    }


    void Update()
    {
        //Debug.Log(background);
        //Debug.Log(colorState);
        if (background.color.r== (float)colorState.max && isColorChanging == false)
        {//Maxなら色値が減少するように            
            isColorChanging = true;
            StartCoroutine(ChangeColor(colorState.max, background.color.r));
        }
        else if(background.color.r== (float)colorState.min && isColorChanging == false)
        {//Minなら色値が増えるように            
            isColorChanging = true;
            StartCoroutine(ChangeColor(colorState.min, background.color.r));
        }
        else{; }//バグ
    }

    
}
