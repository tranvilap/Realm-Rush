using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverWinCanvas : GameOverCanvas
{
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
        base.OnEnable();
    }
}
