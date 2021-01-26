using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.UI;
using LeanCloud;
using LeanCloud.Storage;

public class ProfilePanel : SimpleMainMenuPage
{
    public Text nameText;

    public override async void Show() {
        base.Show();
        try {
            LCUser user = await LCUser.GetCurrent();
            nameText.text = user.GetNickname();
        } catch (LCException e) {
            Debug.LogError($"{e.Code} - {e.Message}");
        }
    }

    public async void Logout() {
        try {
            await LCUser.Logout();
            LCUtils.RemoveLocalUser();
            SendMessageUpwards("ShowTitleScreen", SendMessageOptions.RequireReceiver);
        } catch (LCException e) {
            Debug.LogError($"{e.Code} - {e.Message}");
        }
    }
}
