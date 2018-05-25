
using System;
using UnityEngine;
using UnityEngine.UI;
using ZWGames;

public class StoneBall : MonoBehaviour {

    public Text numberText = null;

    [HideInInspector]
    public Action onFallAnimationEnd;

    // Use this for initialization
    void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //TODO: 需要重新设置速度么？
        GameNotifier.getInstance().notifyStateChange((int)NotifyId.ON_STONE_BALL_FALLING_END);
    }

    public void setNumber(int number)
    {
        numberText.text = number.ToString();
    }

    //球已经准备好了，三秒后往下掉
    public void ready()
    {
        Invoke("fall", 3.0f);
    }

    //往下掉
    public void fall()
    {
        Rigidbody2D rigid2D = this.GetComponent<Rigidbody2D>();
        if (rigid2D != null)
        {
            rigid2D.gravityScale = 40.0f;
        }
    }

}
