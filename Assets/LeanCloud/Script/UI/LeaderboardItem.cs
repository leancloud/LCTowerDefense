using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scrmizu;
using LeanCloud.Storage;

public class LeaderboardItem : MonoBehaviour, IInfiniteScrollItem {
    public Text rankingText;
    public Text nameText;
    public Text scoreText;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void UpdateItemData(object data) {
        gameObject.SetActive(true);
        if (data is LCRanking ranking) {
            rankingText.text = $"No.{ranking.Rank + 1}";
            nameText.text = ranking.User["nickname"] as string;
            scoreText.text = $"{ranking.Value}";
        }
    }
}
