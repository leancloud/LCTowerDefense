using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud;
using LeanCloud.Storage;

public class UserPopup : MonoBehaviour {
    public Text titleText;

    private LCUser target;

    public void Show(LCUser target) {
        this.target = target;
        titleText.text = target.GetNickname();
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }

    public async void AddFriend() {
        try {
            await LCFriendship.Request(target.ObjectId);
            Close();
        } catch (LCException e) {
            Debug.LogError($"{e.Code} : {e.Message}");
        }
    }

    public void Chat() {
        SendMessageUpwards("ShowPrivateChat", target, SendMessageOptions.RequireReceiver);
        Close();
    }
}
