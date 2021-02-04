using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scrmizu;
using LeanCloud;
using LeanCloud.Storage;

public class FriendRequestItem : MonoBehaviour, IInfiniteScrollItem {
    public Text nameText;

    private LCFriendshipRequest request;

    public void Hide() {
        gameObject.SetActive(false);
        request = null;
    }

    public void UpdateItemData(object data) {
        if (data is LCFriendshipRequest req) {
            request = req;
            LCUser user = request["user"] as LCUser;
            nameText.text = user.GetNickname();
            gameObject.SetActive(true);
        }
    }

    public async void Accept() {
        try {
            await LCFriendship.AcceptRequest(request);
            SendMessageUpwards("Reload", SendMessageOptions.RequireReceiver);
        } catch (LCException e) {
            LCUtils.LogException(e);
            throw e;
        }
    }

    public async void Decline() {
        try {
            await LCFriendship.DeclineRequest(request);
            SendMessageUpwards("Reload", SendMessageOptions.RequireReceiver);
        } catch (LCException e) {
            LCUtils.LogException(e);
            throw e;
        }
    }
}
