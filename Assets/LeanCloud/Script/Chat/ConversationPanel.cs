using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using Scrmizu;
using LeanCloud.Realtime;

public class ConversationPanel : MonoBehaviour {
    public InfiniteScrollRect scrollRect;

    public async void Reload() {
        scrollRect.ClearItemData();

        LCIMClient client = LCManager.Instance.IMClient;
        LCIMConversationQuery query = client.GetQuery()
            .WhereEqualTo("m", client.Id)
            .WhereEqualTo("tr", false)
            .OrderByDescending("updatedAt");
        ReadOnlyCollection<LCIMConversation> conversations = await query.Find();
        foreach (LCIMConversation conversation in conversations) {
            scrollRect.AddItemData(conversation);
        }
    }
}
