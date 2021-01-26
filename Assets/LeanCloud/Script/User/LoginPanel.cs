using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud;
using LeanCloud.Storage;

public class LoginPanel : SimpleMainMenuPage
{
    public InputField usernameInputField;

    public InputField passwordInputField;

    public Text tipsText;

    public async void OnLoginClicked() {
        string username = usernameInputField.text;
        if (!LCUtils.IsValidString(username)) {
            tipsText.text = "Please input valid username.";
            return;
        }
        string password = passwordInputField.text;
        if (!LCUtils.IsValidString(password)) {
            tipsText.text = "Please input valid password.";
            return;
        }

        try {
            LCUser user = await LCManager.Instance.Login(username, password);
            LCUtils.SaveUser(user);
            string nickname = user.GetNickname();
            if (string.IsNullOrEmpty(nickname)) {
                SendMessageUpwards("ShowNameMenu", SendMessageOptions.RequireReceiver);
            } else {
                SendMessageUpwards("ShowProfileMenu", SendMessageOptions.RequireReceiver);
            }
        } catch (LCException e) {
            tipsText.text = $"{e.Code} : {e.Message}";
        }
    }

    public async void OnLoginAnoymouslyClicked() {
        try {
            LCUser user = await LCUser.LoginAnonymously();
            LCUtils.SaveUser(user);
            SendMessageUpwards("ShowNameMenu", SendMessageOptions.RequireReceiver);
        } catch (LCException e) {
            tipsText.text = $"{e.Code} : {e.Message}";
        }
    }
}
