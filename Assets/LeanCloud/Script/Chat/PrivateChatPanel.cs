using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using Core.UI;
using LeanCloud;
using LeanCloud.Storage;
using LeanCloud.Realtime;
using Scrmizu;

public class PrivateChatPanel : SimpleMainMenuPage {
    public ConversationPanel conversationPanel;

    public Text nameText;

    public InfiniteScrollRect scrollRect;

    public InputField inputField;

    public LCUser target;

    private LCIMClient client;
    private LCIMConversation conversation;

    public override void Show() {
        base.Show();

        conversationPanel.Reload();
        Reload();
    }

    public override void Hide() {
        base.Hide();
        client.OnMessage -= OnMessage;
    }

    private async void Reload() {
        scrollRect.ClearItemData();

        nameText.text = target.GetNickname();

        client = LCManager.Instance.IMClient;
        client.OnMessage += OnMessage;
        conversation = await client.CreateConversation(new string[] { target.ObjectId });
        // 拉取历史消息
        ReadOnlyCollection<LCIMMessage> messages = await conversation.QueryMessages();
        foreach (LCIMMessage message in messages) {
            AddMessage(message);
        }
    }

    public void Chat(LCUser target) {
        this.target = target;
        Reload();
    }

    public async void Send() {
        string content = inputField.text;
        if (string.IsNullOrEmpty(content)) {
            return;
        }

        try {
            LCIMTextMessage message = new LCIMTextMessage(content);
            await conversation.Send(message);

            AddMessage(message);
        } catch (LCException e) {
            LCUtils.LogException(e);
        }
    }

    private void OnMessage(LCIMConversation conv, LCIMMessage msg) {
        if (conv.Id == conversation.Id &&
            msg is LCIMTextMessage textMsg) {
            AddMessage(textMsg);
        }
    }

    private async void AddMessage(LCIMMessage message) {
        LCUser user = await LCManager.Instance.GetUser(message.FromClientId);
        scrollRect.AddItemData(new MessageEntity {
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
