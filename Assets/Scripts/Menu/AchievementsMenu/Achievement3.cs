using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement3 : MonoBehaviour
{
    public GameObject completeImage;

    void OnEnable()
    {
        if (StaticDataStorage.GetBaloonDestroyed() >= 100)
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = "100/100";
            completeImage.SetActive(true);
        }
        else
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = StaticDataStorage.GetBaloonDestroyed() + "/100";
            completeImage.SetActive(false);
        }
    }
}
