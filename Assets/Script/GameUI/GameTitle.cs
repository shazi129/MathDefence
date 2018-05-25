
using UnityEngine;
using UnityEngine.UI;
using ZWGames;

public class GameTitle : MonoBehaviour {

    public Text scoreText;
    public Text levelText;
    public Button stopBtn;

    private Recipient _recipient = new Recipient();

	// Use this for initialization
	void Start ()
    {
        _recipient.addNotify(NotifyId.NOTIFY_SOCRE_UPDATE, onScroeUpdate);
        _recipient.addNotify(NotifyId.NOTIFY_LEVEL_UPDATE, onLevelUpdate);
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
