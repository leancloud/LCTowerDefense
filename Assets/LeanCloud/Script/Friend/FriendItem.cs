using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scrmizu;
using LeanCloud.Storage;

public class FriendItem : MonoBehaviour, IInfiniteScrollItem {
    public Text nameText;

    private LCObject friendship;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void UpdateItemData(object data) {
        if (data is LCObject obj) {
            friendship = obj;
            LCUser friend = friendship["followee"] as LCUser;
            nameText.text = friend["nickname"] as string;
            gameObject.SetActive(true);
        }
    }

    public void Chat() {
        LCUser friend = friendship["followee"] as LCUser;
        SendMessageUpwards("ShowPrivateChat", friend, SendMessageOptions.RequireReceiver);
    }
}
