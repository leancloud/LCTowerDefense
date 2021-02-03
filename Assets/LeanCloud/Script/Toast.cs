using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour {
    public GameObject toast;

    public void ShowToast(string message) {
        StopAllCoroutines();
        toast.SetActive(true);

        Text text = toast.GetComponentInChildren<Text>();
        text.text = message;

        Canvas.ForceUpdateCanvases();

        VerticalLayoutGroup layoutGroup = toast.GetComponentInChildren<VerticalLayoutGroup>();
        layoutGroup.enabled = false;
        layoutGroup.enabled = true;

        StartCoroutine(DelayToDismiss());
    }

    IEnumerator DelayToDismiss() {
        yield return new WaitForSeconds(2);
        toast.SetActive(false);
    }
}
