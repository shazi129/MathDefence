using UnityEngine;
using System.Collections;
using System;

public class AssetBundleManager
{ 
    private AssetBundleManager(){}
    private static AssetBundleManager _instance;
    public static AssetBundleManager getInstance()
    {
        if (_instance == null) _instance = new AssetBundleManager();
        return _instance;
    }

    public static readonly string ABDir = "MyAssetBundles";

    //AssetBundle的本地路径
    public static readonly string localABPath =
#if UNITY_ANDROID
        "jar:file://" + Application.dataPath + "!/assets/" + ABDir + "/";
#elif UNITY_IPHONE
        Application.dataPath + "/Raw/" + ABDir + "/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        "file://" + Application.dataPath + "/" + ABDir + "/";
#else
        string.Empty;
#endif

    public IEnumerator loadAssetAsync(string assetBundleName, string assetName, Action<AssetBundleRequest> loadCB)
    {
        // 需要等待缓存准备好
        while (!Caching.ready) yield return null;

        string assetBundlePath = localABPath + assetBundleName;

        // 有相同版本号的AssetBundle就从缓存中获取，否则下载进缓存。
        using (WWW www = new WWW(assetBundlePath))
        {
            yield return www;
            if (www.error != null) throw new Exception("WWW download had an error:" + www.error);

            AssetBundle bundle = www.assetBundle;

            // 异步加载
            AssetBundleRequest request = bundle.LoadAssetAsync(assetName);

            // 等待加载完成
            yield return request;

            if (loadCB != null)
            {
                loadCB(request);
            }
            
            //bundle.Unload(false);

        }
    }

}
