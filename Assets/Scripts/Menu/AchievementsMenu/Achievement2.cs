using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement2 : MonoBehaviour
{
    public GameObject completeImage;

    void OnEnable()
    {
        if (StaticDataStorage.GetBulletsShot() >= 200)
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = "200/200";
            completeImage.SetActive(true);
        }
        else
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = StaticDataStorage.GetBulletsShot() + "/200";
            completeImage.SetActive(false);
        }
    }
}
