using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud;
using LeanCloud.Storage;

public class LoginPanel : SimpleMainMenuPage {
    public InputField usernameInputField;

    public InputField passwordInputField;

    public async void OnLoginClicked() {
        string username = usernameInputField.text;
        if (!LCUtils.IsValidString(username)) {
            LCUtils.ShowToast(this, "Please input valid username.");
            return;
        }
        string password = passwordInputField.text;
        if (!LCUtils.IsValidString(password)) {
            LCUtils.ShowToast(this, "Please input valid password.");
            return;
        }

        try {
            LCUser user = await LCManager.Instance.Login(username, password);
            LCUtils.SaveUser(user);
            string nickname = user.GetNickname();
            if (string.IsNullOrEmpty(nickname)) {
                SendMessageUpwards("ShowNameMenu", SendMessageOptions.RequireReceiver);
            } else {
                SendMessageUpwards("ShowLCMainMenu", SendMessageOptions.RequireReceiver);
            }
        } catch (LCException e) {
            LCUtils.ShowToast(this, e);
        }
    }

    public async void OnLoginAnoymouslyClicked() {
        try {
            LCUser user = await LCUser.LoginAnonymously();
            LCUtils.SaveUser(user);
            SendMessageUpwards("ShowNameMenu", SendMessageOptions.RequireReceiver);
        } catch (LCException e) {
            LCUtils.ShowToast(this, e);
        }
    }
}
