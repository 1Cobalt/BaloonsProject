using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChooseLevel : MonoBehaviour
{
    void OnEnable()
    {
        if (this.gameObject.tag == "LevelChooseButton")
        {
            Debug.Log("Enabled");
            if (StaticDataStorage.GetLevelCompleteStatus((uint)int.Parse(this.gameObject.name)))
            {
                this.GetComponent<Image>().color = new Color32(75, 132, 255, 255);
                gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
            }
            else
            {
                this.GetComponent<Image>().color = Color.white;
                gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(75, 132, 255, 255);
            }
        }
    }


    public void ChooseNumberedLevel(int LevelNumber)
    {
        SceneManager.LoadScene("Level"+LevelNumber);
    }

    public void Restart()
    {
      
        SceneManager.LoadScene("Level"+ (int.Parse(SceneManager.GetActiveScene().name.Replace("Level", ""))+1));
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
