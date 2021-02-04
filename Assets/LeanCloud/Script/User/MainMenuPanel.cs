using System;
using UnityEngine;
using UnityEngine.UI;
using Core.UI;
using LeanCloud;
using LeanCloud.Storage;

public class MainMenuPanel : SimpleMainMenuPage {
    public Text nameText;

    public override async void Show() {
        base.Show();
        try {
            LCUser user = await LCUser.GetCurrent();
            nameText.text = user.GetNickname();
        } catch (LCException e) {
            LCUtils.LogException(e);
        }
    }

    public async void Logout() {
        try {
            await LCManager.Instance.Logout();
            SendMessageUpwards("ShowTitleScreen", SendMessageOptions.RequireReceiver);
        } catch (LCException e) {
            // TODO Toast
            LCUtils.ShowToast(this, e);
        }
    }
}
