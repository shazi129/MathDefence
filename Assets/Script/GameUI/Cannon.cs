﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZWGames;

public class Cannon : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject bulletStartArea;

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
        args.Add("oncomplete", "shoot");
        args.Add("oncompletetarget", gameObject);
        iTween.MoveTo(gameObject, args);
    }

    public void shoot()
    {
        //创建一个炮弹
        RectTransform startTransform = bulletStartArea.GetComponent<RectTransform>();
        RectTransform thisTransform = transform as RectTransform;

        GameObject bulletObj = GameObject.Instantiate(bulletPrefab);
        RectTransform bulletTransform = bulletObj.GetComponent<RectTransform>();

        Vector3 position = startTransform.localPosition + thisTransform.localPosition;
        bulletTransform.localPosition = position;
        bulletTransform.SetParent(this.transform.parent, false);

        //加一个向上的速度
        Rigidbody2D rigid2d = bulletObj.GetComponent<Rigidbody2D>();
        rigid2d.velocity = new Vector2(0, 1000);
    }
}
