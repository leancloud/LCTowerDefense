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

    private LCUser user;

    public void Hide() {
        gameObject.SetActive(false);
        user = null;
    }

    public void UpdateItemData(object data) {
        gameObject.SetActive(true);
        if (data is LCRanking ranking) {
            user = ranking.User;
            rankingText.text = $"No.{ranking.Rank + 1}";
            nameText.text = user.GetNickname();
            scoreText.text = $"{ranking.Value}";
        }
    }

    public void OnClick() {
        SendMessageUpwards("ClickUser", user, SendMessageOptions.DontRequireReceiver);
    }
}
