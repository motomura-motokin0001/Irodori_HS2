using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Mathematics;

public class NewBehaviourScript : MonoBehaviour
{
    public Camera MainCamera;

    //===========各キャンバス===============
    public Canvas TitleCanvas;
    public Canvas SelectCanvas;
    public Canvas EASYCanvas;
    public Canvas NORMALCanvas;
    public Canvas HARDCanvas;
    public Canvas EXCanvas;
    //==========================================
    public RectTransform BM;

    public Vector3 S_Position;
    public Vector3 E_Position;
    private Vector3 Title_Position = new (0f,-100f,-10f);
    private Vector3 Select_Position = new (30f,-100f,-10f);
    private Vector3 Game_Position = new (60f,-100f,-10f);
    private int DifficultyLevel = 0;
        void Start()
    {
        //==========キャンバスの初期設定=============
        TitleCanvas.gameObject.SetActive(true);
        SelectCanvas.gameObject.SetActive(false);
        EASYCanvas.gameObject.SetActive(false);
        NORMALCanvas.gameObject.SetActive(false);
        HARDCanvas.gameObject.SetActive(false);
        EXCanvas.gameObject.SetActive(false);
        //=========================================
        
        MainCamera.transform.position = Title_Position; //カメラの初期位置設定
        BM.anchoredPosition = S_Position;
    }
    void FeedImage()
    {
        BM.DOAnchorPos(E_Position, 0.5f);
    }

    public void GAME_START()
    {
        // メニューを隠す
        FeedImage();
        // 3秒待機してからメニューを表示
        StartCoroutine(Time_G());
        TitleCanvas.gameObject.SetActive(false);
        MainCamera.transform.position = Select_Position;
    }
    IEnumerator Time_G()
    {
        yield return new WaitForSeconds(1);
        BM.DOAnchorPos(S_Position, 0.5f);
        SelectCanvas.gameObject.SetActive(true);
    }

    public void Select_back_Title()
    {
        // メニューを隠す
        DifficultyLevel = 0;
        FeedImage();
        StartCoroutine(Time_S_B());// 3秒待機してからメニューを表示
        SelectCanvas.gameObject.SetActive(false);
        MainCamera.transform.position = Title_Position;
    }
    IEnumerator Time_S_B()
    {
        yield return new WaitForSeconds(1);
        BM.DOAnchorPos(S_Position, 0.5f);
        if(DifficultyLevel == 0)
        {
            TitleCanvas.gameObject.SetActive(true);
        }
        else if(DifficultyLevel == -1)
        {
            SelectCanvas.gameObject.SetActive(true);
        }
        
    }
    public void Game_back_Select()
    {
        DifficultyLevel = -1;
        FeedImage();
        StartCoroutine(Time_S_B());// 3秒待機してからメニューを表示
        EASYCanvas.gameObject.SetActive(false);
        NORMALCanvas.gameObject.SetActive(false);
        HARDCanvas.gameObject.SetActive(false);

    }
    public void EASY()
    {
        DifficultyLevel = 1;
        FeedImage();
        StartCoroutine(Time_Game());
        SelectCanvas.gameObject.SetActive(false);
        MainCamera.transform.position = Game_Position;
    }
    public void NORMAL()
    {
        DifficultyLevel = 2;
        FeedImage();
        StartCoroutine(Time_Game());
        SelectCanvas.gameObject.SetActive(false);
        MainCamera.transform.position = Game_Position;
    }
    public void HARD()
    {
        DifficultyLevel = 3;
        FeedImage();
        StartCoroutine(Time_Game());
        SelectCanvas.gameObject.SetActive(false);
        MainCamera.transform.position = Game_Position;
    }

    IEnumerator Time_Game()
    {
        yield return new WaitForSeconds(1);
        BM.DOAnchorPos(S_Position, 0.5f);
        yield return new WaitForSeconds((int)0.5);
        if(DifficultyLevel == 1)
        {
            EASYCanvas.gameObject.SetActive(true);
        }
        else if(DifficultyLevel ==2)
        {
            NORMALCanvas.gameObject.SetActive(true);
        }
        else if(DifficultyLevel == 3)
        {
            HARDCanvas.gameObject.SetActive(true);
        }
        else if(DifficultyLevel == 4)
        {
            //EXCanvas.gameObject.SetActive(true);
        }
    }

}

