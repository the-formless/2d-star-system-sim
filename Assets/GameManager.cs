using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite playBtnImg;
    public Sprite pauseBtnImg;

    public Button playBtn;
    public Slider speedSlider;

    public Transform topRight;
    public Transform topLeft;
    public Transform bottomRight;
    public Transform bottomLeft;

    private int gameSpeed = 0;
    private int currentGameSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = gameSpeed;
        currentGameSpeed = gameSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void SliderValueChange()
    {
        gameSpeed = (int)speedSlider.value;
    }

    public void PlayBtnClick()
    {
        //currentGameSpeed = gameSpeed;
        if(gameSpeed > 0)
        {
            PauseGame();
        }
        else if(gameSpeed == 0 && currentGameSpeed == 0)
        {
            StartGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void ResumeGame()
    {
        Debug.Log("gameResumed");
        gameSpeed = currentGameSpeed;
        SwitchPlayBtnImage(pauseBtnImg);
    }

    private void StartGame()
    {
        gameSpeed = 1;
        currentGameSpeed = gameSpeed;
        SwitchPlayBtnImage(pauseBtnImg);
    }

    private void SwitchPlayBtnImage(Sprite passedImg)
    {
        playBtn.GetComponent<Image>().sprite = passedImg;
    }

    private void PauseGame()
    {
        Debug.Log("GamePaused");
        currentGameSpeed = gameSpeed;
        gameSpeed = 0;
        SwitchPlayBtnImage(playBtnImg);
    }
}
