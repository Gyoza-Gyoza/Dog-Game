using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
public static class AssetManager 
{
    public static void LoadSprites(string spriteURL, Action<Sprite> onLoaded)
    {
        Addressables.LoadAssetAsync<Sprite>(spriteURL).Completed += (loadedSprite) =>
        {
            onLoaded?.Invoke(loadedSprite.Result);
        };
    }

    public static void SetSprite(string spriteURL, GameObject go)
    {
        LoadSprites(spriteURL, (Sprite sp) =>
        {
            go.GetComponent<SpriteRenderer>().sprite = sp;
        });
    }
    public static void SetImage(string spriteURL, GameObject go)
    {
        LoadSprites(spriteURL, (Sprite sp) =>
        {
            go.GetComponent<Image>().sprite = sp;
        });
    }
}
