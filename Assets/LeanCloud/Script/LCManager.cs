using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using LeanCloud;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public class LCManager : MonoBehaviour {
    private static LCManager instance;

    public static LCManager Instance => instance;

    public LCUser User {
        get; private set;
    }
    public LCIMClient IMClient {
        get; private set;
    }

    private Dictionary<string, LCUser> users = new Dictionary<string, LCUser>();

    void Awake() {
        instance = this;

        LCApplication.Initialize("iApERatxkBFcbuIOjnQrb8eK-gzGzoHsz",
            "oS2bLixYBn7E7ftuhcrOWaJJ",
            "https://iaperatx.lc-cn-n1-shared.com");
        LCLogger.LogDelegate = (LCLogLevel level, string message) => {
            switch (level) {
                case LCLogLevel.Debug:
                    Debug.Log(message);
                    break;
                case LCLogLevel.Warn:
                    Debug.LogWarning(message);
                    break;
                case LCLogLevel.Error:
                    Debug.LogError(message);
                    break;
                default:
                    break;
            }
        };
    }

    public async Task<LCUser> Login(string username, string password) {
        try {
            User = await LCUser.Login(username, password);
            IMClient = new LCIMClient(User);
            await IMClient.Open();
            return User;
        } catch (LCException e) {
            LCUtils.LogException(e);
            throw e;
        }
    }

    public async Task<LCUser> Login(string token) {
        try {
            User = await LCUser.BecomeWithSessionToken(token);
            IMClient = new LCIMClient(User);
            await IMClient.Open();
            return User;
        } catch (LCException e) {
            LCUtils.LogException(e);
            throw e;
        }
    }

    public async Task<LCUser> GetUser(string id) {
        if (users.TryGetValue(id, out LCUser user)) {
            return user;
        }
        user = await LCUser.GetQuery().Get(id);
        users[id] = user;
        return user;
    }
}
