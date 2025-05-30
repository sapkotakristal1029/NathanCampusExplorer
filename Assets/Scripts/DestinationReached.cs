using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class DestinationPanelController : MonoBehaviour
{
    [Header("Main UI Panels")]
    public GameObject NavigationUI;
    public GameObject startPanel;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject inGamePanel;
    public GameObject destinationReachedPanel;

    [Header("Dependencies")]
    public PlayerController navigator;
    public UIManager uiManager;
    
    // Open destination reached Panel and hides the destination panel at the start
    void Start()
    {

        if (navigator != null)
            navigator.onDestinationReached += ShowDestinationReachedPanel;

        if (destinationReachedPanel != null)
            destinationReachedPanel.SetActive(false);
    }

    // Displays the destination reached panel and hides in-game UI
    void ShowDestinationReachedPanel()
    {
        Debug.LogWarning("destiantion reached");
        if (destinationReachedPanel != null)
            destinationReachedPanel.SetActive(true);

        if (inGamePanel != null) inGamePanel.SetActive(false);
        
    }

    // back to main menu from the destination screen
    public void mainmenu()
    {
        Debug.LogWarning("main menu clicked");
        destinationReachedPanel.SetActive(false);
        NavigationUI.SetActive(true);
        startPanel.SetActive(true);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        if (inGamePanel != null) inGamePanel.SetActive(false);

    }

    // Restarts the current navigation 
    public void RestartRoute()
    {
        if (destinationReachedPanel != null)
            destinationReachedPanel.SetActive(false);

        if (uiManager != null)
        {
            uiManager.RestartRoute();
        }
    }


}
