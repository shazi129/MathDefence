using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameInstance : MonoBehaviour {

    public GameObject gameLayer;
    public GameObject menuLayer;

    [HideInInspector]
    public enum SceneLayerType
    {
        E_GAME_LAYER_TYPE,
        E_MENU_LAYER_TYPE,
    }

    public int entrySceneIndex = 0;

    private GameObjectList _sceneList;
    public GameObjectList sceneList
    {
        get
        {
            if (_sceneList == null) _sceneList = this.GetComponent<GameObjectList>();
            return _sceneList;
        }
    }

    private void Start()
    {
        GameController.getInstance();
        showScene(entrySceneIndex);
    }


    public void showScene(int sceneIndex, SceneLayerType layerType= SceneLayerType.E_GAME_LAYER_TYPE)
    {
        if (sceneList == null || sceneList.Count == 0)
        {
            Debug.Log("GameInstance::showScene, empty scene list");
            return;
        }

        GameObject scene = sceneList.getIndex(sceneIndex);
        if (scene == null)
        {
            Debug.Log("GameInstance::showScene, cannot find gameobj of index:" + sceneIndex);
            return;
        }

        GameObject parentLayer = layerType == SceneLayerType.E_GAME_LAYER_TYPE ? gameLayer : menuLayer;

        //先清除GameLayer下所有子物体
        for (int i = 0; i < parentLayer.transform.childCount; i++)
        {
            GameObject child = parentLayer.transform.GetChild(i).gameObject;
            Destroy(child);
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
    }

}
