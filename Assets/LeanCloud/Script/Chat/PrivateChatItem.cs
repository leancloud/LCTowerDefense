using UnityEngine;
using UnityEngine.UI;
using Scrmizu;
using LeanCloud.Realtime;

public class PrivateChatItem : MonoBehaviour, IInfiniteScrollItem {
    public HorizontalLayoutGroup layoutGroup;

    public Text firstNameText;
    public Text contentText;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void UpdateItemData(object data) {
        if (data is MessageEntity messageEntity) {
            gameObject.SetActive(true);
            
            string nickname = messageEntity.User.GetNickname();
            if (nickname.Length > 0) {
                firstNameText.text = nickname.Substring(0, 1);
            }
            LCIMTextMessage message = messageEntity.Message as LCIMTextMessage;
            contentText.text = message.Text;

            bool isMe = messageEntity.User.IsMe();
            layoutGroup.reverseArrangement = isMe;
            layoutGroup.childAlignment = isMe ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
            contentText.alignment = isMe ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
        }
    }
}
