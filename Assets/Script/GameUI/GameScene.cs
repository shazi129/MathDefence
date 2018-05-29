using ZWGames;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class GameScene : MonoBehaviour {

    public GameObject stoneBallPrefab;

    public GameObject stoneStartArea;
    public GameObject stoneHangArea;
    
    public NumberBtnArea numberBtnArea;
    public Cannon cannon;

    private Recipient _recipient = new Recipient();
    private GameObject _stoneBallObj = null;

    void Start()
    {
        _recipient.addNotify(NotifyId.START_A_ROUND, startRound);
        _recipient.addNotify(NotifyId.NOTIFY_OP_RESULT, showOpResult);
        _recipient.addNotify(NotifyId.NOTIFY_SHOW_STOP_MENU, stopGame);

        GameNotifier.getInstance().notifyStateChange((int)NotifyId.NOTIFY_GAME_PREPARE_OK);

    }


    private void OnDestroy()
    {
        _recipient.removeAllNotify();
    }

    private void showOpResult(INotifyData obj)
    {
        NotifyData<bool> notifyData = obj as NotifyData<bool>;

        //算对了
        if (notifyData.data == true)
        {
            //将炮移到数字的位置
            if (_stoneBallObj != null)
            {
                float x = _stoneBallObj.transform.localPosition.x;
                cannon.moveToX(x);
            }
            else
            {
                Debug.LogError("GameScene.showOpResult: _stoneBallObj is null");
            }
        }
        else
        {
            cannon.shake();
        }
    }

    private void startRound(INotifyData obj)
    {
        NotifyData<NotifyRoundStartMsg> msg = obj as NotifyData<NotifyRoundStartMsg>;

        //创建一个石球
        resetStoneBall(msg.data.result);

        cannon.clearNumber();

        //刷新按键数字
        numberBtnArea.refreshNumbers(msg.data.numbers);

    }

    private void resetStoneBall(int ballNumber)
    {
        if (_stoneBallObj != null)
        {
            GameObject.Destroy(_stoneBallObj);
        }

        if (stoneBallPrefab != null && stoneHangArea != null)
        {
            //创建
            _stoneBallObj = GameObject.Instantiate(stoneBallPrefab);

            //
            RectTransform startArea = stoneStartArea.GetComponent<RectTransform>();
            float startY = startArea.localPosition.y;

            //随机一个位置
            RectTransform hangArea = stoneHangArea.GetComponent<RectTransform>();
            float width = hangArea.sizeDelta.x;

            float hangY = hangArea.localPosition.y;
            float startX = hangArea.localPosition.x - width / 2;
            float endX = hangArea.localPosition.x + width / 2;
            float x = GameModel.getInstance().getRandomNumber((int)startX, (int)endX);
            
            //石球放到start的位置
            _stoneBallObj.transform.localPosition = new Vector3(x, startY, 0);
            _stoneBallObj.transform.SetParent(this.transform, false);

            //石球的层次
            _stoneBallObj.transform.SetSiblingIndex(0);

            //石球move到hang的位置
            Hashtable args = iTween.Hash();
            args.Add("islocal", true);
            args.Add("position", new Vector3(x, hangY, 0));
            args.Add("time", 1.0f);
            args.Add("easetype", "easeOutBack");
            args.Add("oncomplete", "ready");
            args.Add("oncompletetarget", _stoneBallObj);
            iTween.MoveTo(_stoneBallObj, args);

            //随机一个数值
            StoneBall logic = _stoneBallObj.GetComponent<StoneBall>();
            logic.setNumber(ballNumber);
        }
    }

    //暂停游戏
    private void stopGame(INotifyData obj)
    {
        if (_stoneBallObj != null)
        {
            Destroy(_stoneBallObj);
            _stoneBallObj = null;
        }
    }

}
