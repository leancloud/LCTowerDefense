using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.UI;
using UnityEngine;
using LeanCloud;
using LeanCloud.Storage;
using Scrmizu;

public class LeaderboardPanel : SimpleMainMenuPage {
    private const string LeaderboadName = "GameScore";

    public InfiniteScrollRect scrollRect;

    public override void Show() {
        base.Show();
        Reload();
    }

    private async void Reload() {
        LCLeaderboard leaderboard = LCLeaderboard.CreateWithoutData(LeaderboadName);
        ReadOnlyCollection<LCRanking> rankings = await leaderboard.GetResults(limit: 10, selectUserKeys: new string[] { "nickname" });
        foreach (LCRanking ranking in rankings) {
            LCUser user = ranking.User;
            Debug.Log($"{user.GetNickname()} : ${ranking.Value}");
        }

        scrollRect.SetItemData(rankings);
    }

    public async void FakePlay() {
        try {
            LCUser user = await LCUser.GetCurrent();
            int score = Random.Range(1, 10);
            Dictionary<string, double> statistics = new Dictionary<string, double> {
                { LeaderboadName, score }
            };
            await LCLeaderboard.UpdateStatistics(user, statistics, false);
            Reload();
        } catch (LCException e) {
            Debug.Log($"{e.Code} : {e.Message}");
        }
    }
}
