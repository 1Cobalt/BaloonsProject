using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    private int completedAchivements = 0;
    public Slider fillSlider;

    public GameObject completeImage;

    void OnEnable()
    {
        uint temp = 0;
        for (uint i = 0; i < 30; i++)
        {
            if (StaticDataStorage.GetLevelCompleteStatus(i))
            {
                temp++;
            }
        }
        if (temp == 30)
        {
            completedAchivements++;
        }

        if (StaticDataStorage.GetBulletsShot() >= 200)
        {
            completedAchivements++;
        }

        if (StaticDataStorage.GetBaloonDestroyed() >=100)
        {
            completedAchivements++;
        }

        if (StaticDataStorage.GetBossStatus())
        {
            completedAchivements++;
        }

        if (StaticDataStorage.GetLevelStatus())
        {
            completedAchivements++;
        }

        fillSlider.value = completedAchivements;

        this.GetComponent<TMPro.TextMeshProUGUI>().text = completedAchivements+"/5";


    }


}


