using UnityEngine;
using UnityEngine.UI;
using ZWGames;

public class NumberBtn : MonoBehaviour {

    public Text numberText;

    public Button button;

	// Use this for initialization
	void Start ()
    {
        button.onClick.AddListener(onBtnClick);
    }

    private void onBtnClick()
    {
        GameNotifier.getInstance().notifydata((int)NotifyId.ON_NUMBER_BTN_CLICK, int.Parse(numberText.text));

        //处于按下状态，不能再点击
        setBtnEnable(false);
    }

    public void setNumber(int number)
    {
        numberText.text = number.ToString();
    }

    public void setBtnEnable(bool enable)
    {
        button.interactable = enable;
    }
}
