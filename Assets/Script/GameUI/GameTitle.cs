﻿
using System;
using UnityEngine;
using UnityEngine.UI;
using ZWGames;

using System.Diagnostics.SymbolStore;

public class GameTitle : MonoBehaviour {

    public Text scoreText;
    public Text levelText;
    public Button stopBtn;

    private Recipient _recipient = new Recipient();

    // Use this for initialization
    void Start()
    {
        _recipient.addNotify(NotifyId.NOTIFY_SOCRE_UPDATE, onScroeUpdate);
        _recipient.addNotify(NotifyId.NOTIFY_LEVEL_UPDATE, onLevelUpdate);

        _recipient.addNotify(NotifyId.NOTIFY_SHOW_STOP_MENU, (INotifyData obj) => { stopBtn.gameObject.SetActive(false); });
        _recipient.addNotify(NotifyId.ON_STONE_BALL_FALLING_END, (INotifyData obj) => { stopBtn.gameObject.SetActive(false); });
        _recipient.addNotify(NotifyId.START_A_ROUND, (INotifyData obj) => { stopBtn.gameObject.SetActive(true); });

        stopBtn.onClick.AddListener(onStopBtnClick);
	}

    private void onGameStop(INotifyData obj)
    {
        throw new NotImplementedException();
    }

    private void onStopBtnClick()
    {
        GameNotifier.getInstance().notifyStateChange((int)NotifyId.STOP_GAME);
    }

    private void OnDestroy()
    {
        _recipient.removeAllNotify();
    }

    private void onScroeUpdate(INotifyData obj)
    {
        NotifyData<int> data = obj as NotifyData<int>;
        scoreText.text = data.data.ToString();
    }

    private void onLevelUpdate(INotifyData obj)
    {
        NotifyData<int> data = obj as NotifyData<int>;
        levelText.text = "LVL:" + data.data.ToString();
    }
}
