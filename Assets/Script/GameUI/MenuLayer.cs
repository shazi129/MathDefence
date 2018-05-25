using UnityEngine;
using System.Collections;

public class MenuLayer : MonoBehaviour {

    public GameEndPanel gameEndPanel;

	// Use this for initialization
	void Start () {
	}

    public void showGameEnd()
    {
        gameEndPanel.gameObject.SetActive(true);
        gameEndPanel.showGameData();
    }
}
