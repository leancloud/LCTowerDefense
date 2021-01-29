using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Scrmizu;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public class ConversationItem : MonoBehaviour, IInfiniteScrollItem {
    public Text nameText;

    private LCUser target;

    public void Hide() {
        gameObject.SetActive(false);
        target = null;
    }

    public async void UpdateItemData(object data) {
        gameObject.SetActive(true);
        if (data is LCIMConversation conv) {
            nameText.text = await GetConversationName(conv);
        }
    }

    public void OnClick() {
        SendMessageUpwards("Chat", target, SendMessageOptions.RequireReceiver);
    }

    private async Task<string> GetConversationName(LCIMConversation conv) {
        if (conv.Name != null) {
            return conv.Name;
        }

        LCIMClient client = LCManager.Instance.IMClient;
        if (conv.MemberIds.Count == 2) {
            // 私聊
            string targetId = conv.MemberIds[0] == client.Id ? conv.MemberIds[1] : conv.MemberIds[0];
            target = await LCManager.Instance.GetUser(targetId);
            return target.GetNickname();
        }
        return "Group";
    }
}
