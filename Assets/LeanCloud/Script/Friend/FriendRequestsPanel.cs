using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using LeanCloud;
using LeanCloud.Storage;
using Scrmizu;

public class FriendRequestsPanel : MonoBehaviour {
    public InfiniteScrollRect scrollRect;

    void OnEnable() {
        Reload();
    }

    async void Reload() {
        scrollRect.ClearItemData();

        LCQuery<LCFriendshipRequest> query = new LCQuery<LCFriendshipRequest>("_FriendshipRequest")
            .WhereEqualTo("friend", LCManager.Instance.User)
            .WhereEqualTo("status", "pending")
            .Include("user");
        ReadOnlyCollection<LCFriendshipRequest> requests = await query.Find();
        foreach (LCFriendshipRequest request in requests) {
            scrollRect.AddItemData(request);
        }
    }
}
