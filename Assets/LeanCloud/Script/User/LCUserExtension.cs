using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeanCloud.Storage;

public static class LCUserExtension {
    public static string GetNickname(this LCUser user) {
        return user["nickname"] as string;
    }

    public static void SetNickname(this LCUser user, string nickname) {
        user["nickname"] = nickname;
    }

    public static bool IsMe(this LCUser user) {
        if (user == null) {
            return false;
        }
        if (LCManager.Instance.User == null) {
            return false;
        }
        return LCManager.Instance.User.ObjectId == user.ObjectId;
    }
}
