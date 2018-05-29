using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using ZWGames;

public class StopMenu : MonoBehaviour {

    public Button settingBtn;
    public Button continueBtn;
    public Button restartBtn;
    public Button exitBtn;

	// Use this for initialization
	void Start ()
    {
        settingBtn.onClick.AddListener(onSettingBtnClick);
        continueBtn.onClick.AddListener(onContinueBtnClick);
        restartBtn.onClick.AddListener(onRestartBtnClick);
        exitBtn.onClick.AddListener(onExitBtnClick);
    }

    public void setGameEndDisplay(bool isGameEnd)
    {
        continueBtn.gameObject.SetActive(!isGameEnd);
    }

    private void onRestartBtnClick()
    {
        //析构本界面
        Destroy(this.gameObject);
        //重新开始游戏
        GameNotifier.getInstance().notifyStateChange((int)NotifyId.RESTART_GAME);
    }

    private void onContinueBtnClick()
    {
        //析构本界面
        Destroy(this.gameObject);
        //重新开始游戏
        GameNotifier.getInstance().notifyStateChange((int)NotifyId.CONTINUE_GAME);
    }

    private void onSettingBtnClick()
    {
    }

    private void onExitBtnClick()
    {
        Application.Quit();
    }
	
}
