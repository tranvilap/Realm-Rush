using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverWinCanvas : GameOverCanvas
{
    [Header("Win Canvas")]
    [SerializeField] GameObject nextLevelButton = null;
    [SerializeField] GameObject bronzeStar = null;
    [SerializeField] GameObject silverStars = null;
    [SerializeField] GameObject goldStars = null;
    
    protected override void OnEnable()
    {
        var stageRank = FindObjectOfType<GameController>().CurrentStageRank;
        switch (stageRank)
        {
            case GameController.StageRank.Bronze:
                bronzeStar.SetActive(true);
                break;
            case GameController.StageRank.Silver:
                silverStars.SetActive(true);
                break;
            case GameController.StageRank.Gold:
                goldStars.SetActive(true);
                break;
            default:
                break;
        }
        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings -1)
        {
            nextLevelButton.SetActive(false);
        }
        base.OnEnable();
    }
}
