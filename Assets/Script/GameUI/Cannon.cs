
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZWGames;

public class Cannon : MonoBehaviour {

    public Text numberText;

    private Recipient _recipient = new Recipient();

    // Use this for initialization
    void Start ()
    {
        _recipient.addNotify(NotifyId.CANNON_NUMBER_CHANGE, onOpNumberChanged);
	}

    private void OnDestroy()
    {
        _recipient.removeAllNotify();
    }

    private void onOpNumberChanged(INotifyData obj)
    {
        NotifyData<int> msg = obj as NotifyData<int>;
        if (msg.data <= 0)
        {
            numberText.text = "";
        }
        else
        {
            numberText.text = msg.data.ToString();
        }
    }

    public void clearNUmber()
    {
        numberText.text = "";
    }

    public void moveToX(float x)
    {
        Vector3 cannonPosition = this.transform.localPosition;
        cannonPosition.x = x;

        Hashtable args = iTween.Hash();
        args.Add("islocal", true);
        args.Add("position", cannonPosition);
        args.Add("time", 0.2f);
        iTween.MoveTo(gameObject, args);
    }
}
