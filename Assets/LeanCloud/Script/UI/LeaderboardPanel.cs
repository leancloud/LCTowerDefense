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

    public override async void Show() {
        base.Show();
        LCLeaderboard leaderboard = LCLeaderboard.CreateWithoutData(LeaderboadName);
        ReadOnlyCollection<LCRanking> rankings = await leaderboard.GetResults(limit: 10, selectUserKeys: new string[] { "nickname" });
        foreach (LCRanking ranking in rankings) {
            LCUser user = ranking.User;
            Debug.Log($"{user["nickname"]} : ${ranking.Value}");
        }

        scrollRect.SetItemData(rankings);
    }

    public async void OnTestClicked() {
        try {
            LCUser user = await LCUser.GetCurrent();
            Random random = new Random();
            int score = Random.Range(1, 100);
            Dictionary<string, double> statistics = new Dictionary<string, double> {
                { LeaderboadName, score }
            };
            await LCLeaderboard.UpdateStatistics(user, statistics, false);
        } catch (LCException e) {
            Debug.Log($"{e.Code} : {e.Message}");
        }
    }
}
