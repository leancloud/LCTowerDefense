using System;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public class MessageEntity {
    public LCUser User {
        get; set;
    }
    public LCIMMessage Message {
        get; set;
    }
}
