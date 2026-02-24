using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Loader
{
    public static T Load<T>(string path) where T : Object
    {
        var asset = Resources.Load<T>(path);
        if (asset == null) throw new Exception("Resource not found: " + path);
        return asset;
    }
    
    public static void Unload() => Resources.UnloadUnusedAssets();
}