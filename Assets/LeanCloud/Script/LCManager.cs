using UnityEngine;
using LeanCloud;

public class LCManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
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
}
