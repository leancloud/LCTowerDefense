using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using LeanCloud.Storage;
using Scrmizu;

public class FriendsPanel : MonoBehaviour {
    public InfiniteScrollRect scrollRect;

    async void OnEnable() {
        scrollRect.ClearItemData();

        LCQuery<LCObject> query = new LCQuery<LCObject>("_Followee")
            .WhereEqualTo("user", LCManager.Instance.User)
            .WhereEqualTo("friendStatus", true)
            .Include("followee");
        ReadOnlyCollection<LCObject> friends = await query.Find();
        foreach (LCObject friend in friends) {
            scrollRect.AddItemData(friend);
        }
    }
}
