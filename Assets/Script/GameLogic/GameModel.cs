using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel : ModelBase
{
    private static GameModel _instance = null;

    public static GameModel getInstance()
    {
        if (_instance == null)
        {
            _instance = new GameModel();
        }
        return _instance;
    }

    //分数
    private int _score = 0;
    [ObservableName("score")]
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
        }
    }

    //等级
    private int _level = 1;
    [ObservableName("level")]
    public int level
    {
        get { return _level; }
        set
        {
            _level = value;
        }
    }

    //点击的操作数
    [ObservableName("opNumber")]
    public int opNumber { get; set; }


    //结果
    [ObservableName("resultNumber")]
    public int resultNumber { get; set; }


    //随机数生成函数
    private System.Random _random = new System.Random();
    public int getRandomNumber(int start, int end)
    {
        return _random.Next(start, end);
    }
    
}
