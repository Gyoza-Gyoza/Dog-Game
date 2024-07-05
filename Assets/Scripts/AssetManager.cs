using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public static class AssetManager 
{
    private static string imagePath = "Assets/Images/{0}";

    public static void LoadSprites(string spriteName, System.Action<Sprite> onLoaded)
    {
        Addressables.LoadAssetAsync<Sprite>(string.Format(imagePath, spriteName)).Completed += (loadedSprite) =>
        {
            onLoaded?.Invoke(loadedSprite.Result);
        };
    }
}
