using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public TMP_Dropdown startDropdown;
    public TMP_Dropdown endDropdown;
    public Button startButton;
    public GameObject canvasRoot;
    public ThirdPersonAutoNavigator navigator;

    private List<string> doorOptions = new List<string> {
        "N53door1", "N53door2", "N28door", "N18door", "N76door1",
        "N76door2", "N66door", "N16door2", "N16door1", "caferosadoor"
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

    void UpdateEndDropdown(string selectedStart)
    {
        endDropdown.ClearOptions();

        // Create a new list excluding the selected start value
        List<string> filteredOptions = new List<string>(doorOptions);
        filteredOptions.Remove(selectedStart);

        endDropdown.AddOptions(filteredOptions);
    }

    void OnStartClicked()
    {
        string start = startDropdown.options[startDropdown.value].text;
        string end = endDropdown.options[endDropdown.value].text;

        navigator.SetRoute(start, end);
        canvasRoot.SetActive(false); // Hide UI
    }

    public void Quit()
    {
        Debug.Log("Quit button pressed");

        #if UNITY_EDITOR
            // Stop play mode if in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the application if built
            Application.Quit();
        #endif
    }
    
}
