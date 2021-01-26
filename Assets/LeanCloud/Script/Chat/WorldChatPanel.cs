using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud;
using LeanCloud.Storage;
using LeanCloud.Realtime;
using Scrmizu;
using Core.UI;

public class WorldChatPanel : SimpleMainMenuPage {
    private const string WordConversationId = "5ff4237496157d710eb2d6c7";

    public InfiniteScrollRect scrollRect;

    public InputField inputField;

    public override async void Show() {
        base.Show();

        LCIMClient client = LCManager.Instance.IMClient;
        client.OnMessage += OnMessage;
        LCIMChatRoom chatRoom = await client.GetConversation(WordConversationId) as LCIMChatRoom;
        await chatRoom.Join();
    }

    public override void Hide() {
        base.Hide();
        LCManager.Instance.IMClient.OnMessage -= OnMessage;
    }

    private void OnMessage(LCIMConversation conv, LCIMMessage msg) {
        if (conv.Id == WordConversationId) {
            AddMessage(msg);
        }
    }

    public async void Send() {
        string content = inputField.text;
        if (string.IsNullOrEmpty(content)) {
            return;
        }

        try {
            LCIMTextMessage message = new LCIMTextMessage(content);
            LCIMConversation conversation = await LCManager.Instance.IMClient.GetConversation(WordConversationId);
            await conversation.Send(message);

            AddMessage(message);
        } catch (LCException e) {
            LCUtils.LogException(e);
        }
    }

    private async void AddMessage(LCIMMessage message) {
        LCUser user = await LCManager.Instance.GetUser(message.FromClientId);
        scrollRect.AddItemData(new ChatItem.MessageEntity {
            User = user,
            Message = message
        });
        StartCoroutine(Resize());
    }

    IEnumerator Resize() {
        yield return new WaitForSeconds(0.01f);
        if (scrollRect.MaxScrollPosition > 0) {
            scrollRect.MovePositionAt(scrollRect.Count);
        }
    }
}
