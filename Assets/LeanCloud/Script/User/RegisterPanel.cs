using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.UI;
using LeanCloud;
using LeanCloud.Storage;

public class RegisterPanel : SimpleMainMenuPage {
    public InputField usernameInputField;

    public InputField passwordInputField;

    public InputField confirmPasswordInputField;

    public async void OnRegisterClicked() {
        string username = usernameInputField.text;
        if (!LCUtils.IsValidString(username)) {
            return;
        }
        string password = passwordInputField.text;
        if (!LCUtils.IsValidString(password)) {
            return;
        }
        string confirmPassword = confirmPasswordInputField.text;
        if (!LCUtils.IsValidString(confirmPassword)) {
            return;
        }

        try {
            await LCManager.Instance.Register(username, password);
            SendMessageUpwards("ShowNameMenu", SendMessageOptions.RequireReceiver);
        } catch (LCException e) {
            // TODO Toast

        } 
    }
}
