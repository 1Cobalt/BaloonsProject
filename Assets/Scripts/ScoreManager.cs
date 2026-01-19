using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreText;
  
    public int scoreForWin;

    public delegate void OnLevelFinished();
    public static event OnLevelFinished onLevelFinished;

    private float score = 0.0f;
    void OnEnable()
    {
        GameController.onScoreAdded += AddScore;
    }
    void OnDisable()
    {
        GameController.onScoreAdded -= AddScore;
    }

    void AddScore(float scoreToAdd)
    {
        if (scoreToAdd == -1.0f)
        {

        }
        else
        {
            score += scoreToAdd;
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + score;

            if (score >= scoreForWin)
            {
                onLevelFinished?.Invoke();
            }
        }
    }


}
