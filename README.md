# Unity---Fight-Network-Game
[Unity] Fight Network Game 
  
  
*プロジェクトファイルでSignInSecneがあるが、FireBase連動に問題があったため始めるとLobbyから始めるようにScriptを作っておいた。
もし、SingInSecneをテストしたいなら、Script->AfterGame->Main'sScript->EnvironmentControlにあるForceStartの変数をSignInに変えてください。*
  
*OnlyScriptsフォルダは、この文章を見ている会社の関係者の方々が私のポートフォリオを評価するのに時間を節約するために作りました。 このフォルダにはUnityのScriptだけがあります。*
<hr/>  
  
Play Video
-----------
Video : https://www.youtube.com/watch?v=NzC9wAe0EHc
  
<hr/> 


<hr/>  

このプロジェクトはまだ未完成です。 GithubのLFSのbandwidthに対する問題のせいでGithubにアップロードされない状況です。 それでプロジェクトファイルのリンクを書いておきます。
-----

Project File : https://drive.google.com/open?id=1JD_BS6t76-b7WMhugOTMB0bSpreeeeS7 (2020/02/28)
  
  
  
具現すべきリスト
---------
0. ファイアベース連動(現在バグ有り。 他のNetwork Connect Methodを探してみ中。)
1. ~体力の具現~
2. ~体力syncronized~
3. 弾丸の具現
4. 弾丸 syncronized
5. 陥穽の具現
6. 陥穽 syncronized
7. 瞑想の具現(HPを少しずつ回復)
8. 自害具現(HPを少しずつ減少)
9. ターン具現
10. 視野具現
11. ランダムマップ作成具現
12. How To Playを具現
13. いろいろなキャラクターを具現
14. ~ロビー具現~
15. 勝利敗北の具現

-追加の具現-
-------

16. モバイル具現
17. playStoreで接続具現

<hr/>



Description
===========
  
FirebaseとPhotonを利用したネットワークゲームです。
----------------------------------------------
プレーヤーがランダムにマッチして戦う2Dアクションゲームです。
---------------------------------------------
プレーヤーは30秒ごとにお互いのHPと位置が変わります。(これをターンといいます。)
------------------------------------------------------------
プレイヤーは限られた視野の中で相手と戦うのか、それとも自分のHPをわざと減少させて次のターンを待つのかを選択することになります。
-------------------------------------------------------------
単純なアクションゲームに様々な戦略的要素を入れました。
--------------------------------



_Q : このゲームを作った理由?_  

_A :   就職のためにポートフォリオでネットワークゲームを作ろうと思いました。 そして、ゲームの楽しさのために30秒ごとに体力と位置が変わる戦略的要素を考えました。_

  
  
  
<hr/>
unity 3.1f, photon PUN2, firebase 6.11.0
