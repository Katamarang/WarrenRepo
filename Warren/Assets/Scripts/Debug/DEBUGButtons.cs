using UnityEngine;

public class DEBUGButtons : MonoBehaviour
{
    [SerializeField] GameObject ToEnable;
    bool isEnabled = true;

    public void OnClick()
    {
        isEnabled = !isEnabled;
        ToEnable.SetActive(isEnabled);
    }

}
