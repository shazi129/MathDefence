using System.Collections.Generic;
using ZWGames;

public class GameController : Recipient
{
    private static GameController _instance = null;

    public static GameController getInstance()
    {
        if (_instance == null)
        {
            _instance = new GameController();
        }
        return _instance;
    }

    private GameController()
    {
        this.addNotify(NotifyId.ON_NUMBER_BTN_CLICK, handleBtnClick);
        this.addNotify(NotifyId.ON_STONE_BALL_FALLING_END, handleStoneFallingEnd);
        this.addNotify(NotifyId.NOTIFY_GAME_PREPARE_OK, handleGamePrepareOk);
    }

    ~GameController()
    {
        GameNotifier.getInstance().removeRecipient(this);
    }

    private void handleGamePrepareOk(INotifyData obj)
    {
        //初始化数据
        GameModel gameModel = GameModel.getInstance();
        gameModel.level = 1;
        gameModel.score = 0;
        startRound();
    }

    public void handleBtnClick(INotifyData obj)
    {
        NotifyData<int> msg = obj as NotifyData<int>;
        if (msg != null)
        {
            GameModel gameModel = GameModel.getInstance();
            GameNotifier gameNotifier = GameNotifier.getInstance();

            bool needResult = false;

            //如果现在已经有数字, 加上去
            if (gameModel.opNumber > 0)
            {
                gameModel.opNumber += msg.data;
                needResult = true;
            }
            else
            {
                gameModel.opNumber = msg.data;
            }

            //通知炮上的数字发生改变
            gameNotifier.notifydata((int)NotifyId.CANNON_NUMBER_CHANGE, gameModel.opNumber);

            //如果需要结算
            if (needResult)
            {
                handleOpResult();
            }
        }
    }

    public void handleOpResult()
    {
        GameNotifier gameNotifier = GameNotifier.getInstance();
        GameModel gameModel = GameModel.getInstance();

        //通知结果
        bool isCorrect = (gameModel.resultNumber == gameModel.opNumber);
        gameNotifier.notifydata((int)NotifyId.NOTIFY_OP_RESULT, isCorrect);

        //重置记录的数字
        gameModel.opNumber = -1;

        //如果正确，计分，开始新的一回合
        if (isCorrect)
        {
            //更新分数
            gameModel.score += 1;
            gameNotifier.notifydata((int)NotifyId.NOTIFY_SOCRE_UPDATE, gameModel.score);

            //每5分升一级
            if (gameModel.score > 0 && gameModel.score % 5 == 0)
            {
                gameModel.level += 1;
                gameNotifier.notifydata((int)NotifyId.NOTIFY_LEVEL_UPDATE, gameModel.level);
            }

            startRound();
        }
    }

    //球落到地上了，游戏结束
    public void handleStoneFallingEnd(INotifyData obj)
    {
        //GameNotifier.getInstance().notifyStateChange((int)NotifyId.NOTIFY_GAME_END);
        startRound();
    }

    public void startRound()
    {
        int numberCount = 8;
        GameModel gameModel = GameModel.getInstance();

        //最大数字
        int maxNumber = gameModel.level * 10;

        //生成8个随机数
        List<int> randomNumbers = new List<int>();
        while (randomNumbers.Count < numberCount)
        {
            int number = gameModel.getRandomNumber(1, maxNumber);
            if (randomNumbers.IndexOf(number) != -1)
            {
                continue;
            }
            randomNumbers.Add(number);
        }

        //挑选2个座位结果
        int index1 = gameModel.getRandomNumber(0, numberCount - 1);
        int index2 = gameModel.getRandomNumber(0, numberCount - 1);
        while (index1 == index2)
        {
            index2 = gameModel.getRandomNumber(0, numberCount - 1);
        }

        //消息体
        NotifyData<NotifyRoundStartMsg> msg = new NotifyData<NotifyRoundStartMsg>();
        msg.data = new NotifyRoundStartMsg();

        msg.id = (int)NotifyId.START_A_ROUND;
        msg.data.result = randomNumbers[index1] + randomNumbers[index2];
        msg.data.numbers = randomNumbers;
        GameNotifier.getInstance().notify(msg);

        gameModel.resultNumber = msg.data.result;
    }

}
