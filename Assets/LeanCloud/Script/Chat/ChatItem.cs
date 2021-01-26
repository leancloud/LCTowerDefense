using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scrmizu;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public class ChatItem : MonoBehaviour, IInfiniteScrollItem {
    public class MessageEntity {
        public LCUser User {
            get; set;
        }
        public LCIMMessage Message {
            get; set;
        }
    }

    public Text chatText;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void UpdateItemData(object data) {
        if (data is MessageEntity messageEntity) {
            gameObject.SetActive(true);
            LCUser user = messageEntity.User;
            LCIMTextMessage message = messageEntity.Message as LCIMTextMessage;
            chatText.text = $"{user["nickname"]}: {message.Text}";
        }
    }
}
