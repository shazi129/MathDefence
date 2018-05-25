using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZWGames;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //炮弹发生碰撞，表明击中了
        GameObject.Destroy(this.gameObject);

        GameNotifier.getInstance().notifyStateChange((int)NotifyId.NOTIFY_BULLET_HIT);
    }
}
