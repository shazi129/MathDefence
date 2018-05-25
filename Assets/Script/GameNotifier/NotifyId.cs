using System.Collections.Generic;

public enum NotifyId
{

    START_A_ROUND, //开始一回合
    ON_NUMBER_BTN_CLICK,  //按钮点击
    CANNON_NUMBER_CHANGE, //炮上的数字改变

    ON_STONE_BALL_FALLING_END, //

    NOTIFY_OP_RESULT,
    NOTIFY_SOCRE_UPDATE,
    NOTIFY_LEVEL_UPDATE,
    NOTIFY_GAME_END,

    NOTIFY_GAME_PREPARE_OK, //gameScene已经准备好了

    NOTIFY_BULLET_HIT, //击中
}

//一回合开始的消息
public class NotifyRoundStartMsg
{
    public int result = -1;
    public List<int> numbers;
}