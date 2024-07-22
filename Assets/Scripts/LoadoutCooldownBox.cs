using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutCooldownBox : MonoBehaviour
{
    private Slider
        slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void StartCooldown(float cooldown)
    {
        StartCoroutine(StartCooldownCoroutine(cooldown));
    }
    private IEnumerator StartCooldownCoroutine(float cooldown)
    {
        float timer = cooldown; 

        while (timer >= 0f)
        {
            timer -= Time.deltaTime;
            slider.value = timer / cooldown;
            yield return null;
        }
    }

    public void SetImage(string spriteURL)
    {
        AssetManager.SetImage(spriteURL, gameObject);
    }
}
