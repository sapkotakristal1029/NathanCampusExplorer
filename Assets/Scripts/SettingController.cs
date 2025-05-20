using UnityEngine;

public class SettingController : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject SettingPanel;

    // Called when the "Settings" button is pressed
    public void ShowSettings()
    {
        StartPanel.SetActive(false);
        SettingPanel.SetActive(true);
    }

    // Called when the "BACK" button in settings is pressed
    public void BackToStart()
    {
        SettingPanel.SetActive(false);
        StartPanel.SetActive(true);
    }
}
