using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using ZWGames;
using GoogleMobileAds.Api;

public class StopMenu : MonoBehaviour {

    public Button settingBtn;
    public Button continueBtn;
    public Button restartBtn;
    public Button exitBtn;

    private BannerView bannerView;

	// Use this for initialization
	void Start ()
    {
        settingBtn.onClick.AddListener(onSettingBtnClick);
        continueBtn.onClick.AddListener(onContinueBtnClick);
        restartBtn.onClick.AddListener(onRestartBtnClick);
        exitBtn.onClick.AddListener(onExitBtnClick);

        requestBannerAd();
    }

    private void requestBannerAd()
    {
        string adUnitId = "ca-app-pub-3029558759541761/9614450584";  //横幅广告ID
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);  //横幅广告实例化（三个参数为：ID、大小、位置） 
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
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
