using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeanCloud;
using LeanCloud.Storage;

public static class LCUtils {
    public const string LC_SESSION_TOKEN = "lc_session_token";

    public static bool IsValidString(string str) {
        return !string.IsNullOrEmpty(str) && str.Trim().Length > 0;
    }

    public static void SaveUser(LCUser user) {
        PlayerPrefs.SetString(LC_SESSION_TOKEN, user.SessionToken);
    }

    public static bool TryGetLocalSessionToken(out string sessionToken) {
        sessionToken = PlayerPrefs.GetString(LC_SESSION_TOKEN, null);
        return !string.IsNullOrEmpty(sessionToken);
    }

    public static void RemoveLocalUser() {
        PlayerPrefs.DeleteKey(LC_SESSION_TOKEN);
    }

    public static void LogException(LCException e) {
        Debug.LogError($"{e.Code} : {e.Message}");
    }

    public static void ShowToast(Component component, string message) {
        component.SendMessageUpwards("ShowToast", message, SendMessageOptions.RequireReceiver);
    }

    public static void ShowToast(Component component, LCException e) {
        component.SendMessageUpwards("ShowToast", $"{e.Code} : {e.Message}", SendMessageOptions.RequireReceiver);
    }
}
