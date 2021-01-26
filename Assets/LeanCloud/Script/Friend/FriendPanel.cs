using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using Core.UI;
using LeanCloud;
using LeanCloud.Storage;
using Scrmizu;

public class FriendPanel : SimpleMainMenuPage {
    public Toggle friendsToggle;
    public Toggle requestsToggle;

    public override void Show() {
        base.Show();
        friendsToggle.isOn = true;
        requestsToggle.isOn = false;
    }
}
