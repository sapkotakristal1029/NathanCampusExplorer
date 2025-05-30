using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

    [Header("Dropdowns")]
    public TMP_Dropdown startDropdown;
    public TMP_Dropdown endDropdown;

    [Header("Start Button")]
    public Button startButton;

     [Header("UI Panels")]
    public GameObject canvasRoot;
    public GameObject inGamePanel;

     [Header("Script References")]
    public PlayerController navigator;
    public InGamePanel inGameController; 

    private string lastStart;
    private string lastEnd;


    private List<string> doorOptions = new List<string> {
        "N53door1", "N53door2", "N28door", "N18door", "N76door1",
        "N76door2", "N66door", "N16door1", "N16door2", "caferosadoor"
    };

    void Start()
    {
        // Initialize dropdowns
        startDropdown.ClearOptions();
        endDropdown.ClearOptions();

        //Making Dropdown list bold
        startDropdown.AddOptions(new List<string>(doorOptions));
        startDropdown.captionText.fontStyle = FontStyles.Bold;
        endDropdown.captionText.fontStyle = FontStyles.Bold;
        endDropdown.itemText.fontStyle = FontStyles.Bold;


        startDropdown.itemText.fontStyle = FontStyles.Bold;

        UpdateEndDropdown(startDropdown.options[startDropdown.value].text);

        startDropdown.onValueChanged.AddListener(delegate
        {
            string selectedStart = startDropdown.options[startDropdown.value].text;
            UpdateEndDropdown(selectedStart);
        });

        startButton.onClick.AddListener(OnStartClicked);
    }

    // Updates the end location dropdown to exclude selected start
    void UpdateEndDropdown(string selectedStart)
    {
        endDropdown.ClearOptions();

        // Create a new list excluding the selected start value
        List<string> filteredOptions = new List<string>(doorOptions);
        filteredOptions.Remove(selectedStart);

        endDropdown.AddOptions(filteredOptions);
    }

    // Starts the route when "Start" is clicked
    void OnStartClicked()
    {
        string start = startDropdown.options[startDropdown.value].text;
        string end = endDropdown.options[endDropdown.value].text;

        navigator.SetRoute(start, end);
        canvasRoot.SetActive(false); // Hide UI

        if (inGameController != null)
            inGameController.ResumeIfPaused();

        if (inGamePanel != null) inGamePanel.SetActive(true);
    }

    // Restarts the last selected route
    public void RestartRoute()
    {
        lastStart = startDropdown.options[startDropdown.value].text;
        lastEnd = endDropdown.options[endDropdown.value].text;

        navigator.SetRoute(lastStart, lastEnd);
        canvasRoot.SetActive(false);


        if (inGameController != null)
            inGameController.ResumeIfPaused();
        if (inGamePanel != null) inGamePanel.SetActive(true);
    }

    // Exits play mode 
    public void Quit()
    {
        Debug.Log("Quit button pressed");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                            Application.Quit();
        #endif
    }
    
}
