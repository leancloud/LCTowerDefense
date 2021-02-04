using UnityEngine;
using UnityEngine.UI;
using Core.UI;
using LeanCloud;

public class RegisterPanel : SimpleMainMenuPage {
    public InputField usernameInputField;

    public InputField passwordInputField;

    public InputField confirmPasswordInputField;

    public async void OnRegisterClicked() {
        string username = usernameInputField.text;
        if (!LCUtils.IsValidString(username)) {
            LCUtils.ShowToast(this, "Please input username.");
            return;
        }
        string password = passwordInputField.text;
        if (!LCUtils.IsValidString(password)) {
            LCUtils.ShowToast(this, "Please input password.");
            return;
        }
        string confirmPassword = confirmPasswordInputField.text;
        if (!LCUtils.IsValidString(confirmPassword)) {
            LCUtils.ShowToast(this, "Please retype password.");
            return;
        }
        if (password != confirmPassword) {
            LCUtils.ShowToast(this, "Password not match.");
            return;
        }

        try {
            await LCManager.Instance.Register(username, password);
            LCUtils.ShowToast(this, "注册成功");
            SendMessageUpwards("ShowNameMenu", SendMessageOptions.RequireReceiver);
        } catch (LCException e) {
            LCUtils.ShowToast(this, e);
        } 
    }
}
