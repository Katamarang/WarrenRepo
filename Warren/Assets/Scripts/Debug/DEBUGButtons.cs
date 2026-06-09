using UnityEngine;

public class DEBUGButtons : MonoBehaviour
{
    // This is a debug script to enable or disable a GameObject when a button is clicked.

    [SerializeField] GameObject ToEnable;
    bool isEnabled = true;

    public void OnClick()
    {
        isEnabled = !isEnabled;
        ToEnable.SetActive(isEnabled);
    }
}
