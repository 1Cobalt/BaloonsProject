using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] elementsToHide;
    public GameObject[] elementsToShow;
   

    public void HideAndShowElements()
    {
        for(int i = 0; i<elementsToHide.Length; i++)
        {
            elementsToHide[i].SetActive(false);
        }
        for (int i = 0; i < elementsToShow.Length; i++)
        {
            elementsToShow[i].SetActive(true);
        }

        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }


    public void LeaveGame()
    {
        Application.Quit();
    }
}
