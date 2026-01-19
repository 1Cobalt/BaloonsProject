using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement4 : MonoBehaviour
{
    public GameObject completeImage;

    void OnEnable()
    {
        if (StaticDataStorage.GetBossStatus())
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = "Completed!";
            completeImage.SetActive(true);
        }
        else
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = "";
            completeImage.SetActive(false);
        }
    }
}
