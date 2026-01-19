using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] Sprite turnOnSprite;
    [SerializeField] Sprite turnOffSprite;
    private bool isOn = true;

    private void OnEnable()
    {
        isOn = StaticDataStorage.GetSoundsStatus();
        if (isOn)
        {
            gameObject.GetComponent<Image>().sprite = turnOnSprite;
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = turnOffSprite;
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }

    public void SoundsButtonPressed()
    {
        if (isOn)
        {
            gameObject.GetComponent<Image>().sprite = turnOffSprite;
            isOn = false;
            StaticDataStorage.SetSoundsStatus(isOn);
            AudioListener.volume = 0.0f;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = turnOnSprite;
            isOn = true;
            StaticDataStorage.SetSoundsStatus(isOn);
            AudioListener.volume = 1.0f;
        }
    }
}
