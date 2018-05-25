using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour {

    public Text scoreTxt;
    public Button playAgainBtn;
    public Button exitBtn;

    public MenuLayer menuLayerLogic { get; set; }

	// Use this for initialization
	void Start ()
    {
        playAgainBtn.onClick.AddListener(() => { onPlayAgainBtnClick(); });
        exitBtn.onClick.AddListener(() => { onExitBtnClick(); });

        showGameData();
    }

    public void showGameData()
    {
        scoreTxt.text = "Score:" + GameModel.getInstance().score;
    }

    private void onPlayAgainBtnClick()
    {
    }

    private void onExitBtnClick()
    {
        Application.Quit();
    }
	
}
