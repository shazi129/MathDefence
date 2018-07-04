using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ZWGames;
using System;

public class GameInstance : MonoBehaviour {

    public GameObject gameLayer;
    public GameObject menuLayer;

    [HideInInspector]
    public enum SceneLayerType
    {
        E_GAME_LAYER_TYPE,
        E_MENU_LAYER_TYPE,
    }

    public int _entrySceneIndex = 0;

    //界面合集
    private GameObjectList _sceneList;
    public GameObjectList sceneList
    {
        get
        {
            if (_sceneList == null) _sceneList = this.GetComponent<GameObjectList>();
            return _sceneList;
        }
    }

    //通知
    private Recipient _recipient = new Recipient();

    private void Start()
    {
        _recipient.addNotify(NotifyId.NOTIFY_SHOW_STOP_MENU, showStopMenu);
        _recipient.addNotify(NotifyId.NOTIFY_SHOW_GAME_END, showStopMenu);

        GameController.getInstance();
        showScene(_entrySceneIndex);
    }

    private void showStopMenu(INotifyData obj)
    {
        GameObject menu = showScene(1, SceneLayerType.E_MENU_LAYER_TYPE);
        StopMenu logic = menu.GetComponent<StopMenu>();
        if (logic != null)
        {
            logic.setGameEndDisplay(obj.id == (int)NotifyId.NOTIFY_SHOW_GAME_END);
        }
    }

    public GameObject showScene(int sceneIndex, SceneLayerType layerType= SceneLayerType.E_GAME_LAYER_TYPE)
    {
        if (sceneList == null || sceneList.Count == 0)
        {
            Debug.Log("GameInstance::showScene, empty scene list");
            return null;
        }

        GameObject scene = sceneList.getIndex(sceneIndex);
        if (scene == null)
        {
            Debug.Log("GameInstance::showScene, cannot find gameobj of index:" + sceneIndex);
            return null;
        }

        GameObject parentLayer = layerType == SceneLayerType.E_GAME_LAYER_TYPE ? gameLayer : menuLayer;

        //不管怎么样，都要清除menu
        for (int i = 0; i < menuLayer.transform.childCount; i++)
        {
            GameObject child = menuLayer.transform.GetChild(i).gameObject;
            Destroy(child);
        }

        //如果是展示game，清除GameLayer下所有子物体
        if (layerType == SceneLayerType.E_GAME_LAYER_TYPE)
        {
            for (int i = 0; i < gameLayer.transform.childCount; i++)
            {
                GameObject child = gameLayer.transform.GetChild(i).gameObject;
                Destroy(child);
            }
        }

        //将go放到GameLayer下
        GameObject newGameScene = GameObject.Instantiate(scene);

        newGameScene.SetActive(true);
        newGameScene.transform.SetParent(parentLayer.transform, false);
        newGameScene.transform.localPosition = new Vector3(0, 0, 0);

        //加个黑背景突出层次
        if (layerType == SceneLayerType.E_MENU_LAYER_TYPE)
        {
            Image menuBg = newGameScene.AddComponent<Image>();
            menuBg.color = new Color(0, 0, 0, 0.72f);
        }

        return newGameScene;
    }

}
