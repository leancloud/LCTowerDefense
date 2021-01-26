using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.UI;
using LeanCloud.Storage;

public class RegisterPanel : SimpleMainMenuPage
{
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

        LCUser user = new LCUser {
            Username = username,
            Password = password
        };
        await user.SignUp();
    }
}
