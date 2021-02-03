using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scrmizu;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public class ChatItem : MonoBehaviour, IInfiniteScrollItem {
    public Text chatText;

    private LCUser user;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void UpdateItemData(object data) {
        if (data is MessageEntity messageEntity) {
            gameObject.SetActive(true);
            user = messageEntity.User;
            LCIMTextMessage message = messageEntity.Message as LCIMTextMessage;
            chatText.text = $"{user.GetNickname()}: {message.Text}";
        }
    }

    public void OnClick() {
        SendMessageUpwards("ClickUser", user, SendMessageOptions.DontRequireReceiver);
    }
}
