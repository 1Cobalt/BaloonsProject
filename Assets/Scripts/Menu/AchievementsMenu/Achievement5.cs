using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement5 : MonoBehaviour
{
    public GameObject completeImage;

    void OnEnable()
    {
        if (StaticDataStorage.GetLevelStatus())
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
