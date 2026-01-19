using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement1 : MonoBehaviour
{
    public GameObject completeImage;

 
    void OnEnable()
    {
        uint temp = 0;
        for (uint i = 0; i < 30; i++) {
            if (StaticDataStorage.GetLevelCompleteStatus(i))
            {
                temp++;
            }
        }

        this.GetComponent<TMPro.TextMeshProUGUI>().text = temp + "/30";

        if (temp == 30)
        {
            completeImage.SetActive(true);
        }

    }
}
