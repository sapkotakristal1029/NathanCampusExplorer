using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class MenuController : MonoBehaviour
{
    [Header("Panel References")]
    public GameObject startPanel;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject destinationReachedPanel;

    [Header("View & Accessibility Toggles")]
    public Toggle firstPersonToggle;
    public Toggle thirdPersonToggle;
    public Toggle accessibilityToggle;

    [Header("Dropdown Selections")]
    public TMP_Dropdown startDropdown;
    public TMP_Dropdown endDropdown;

    [Header("Player & Speed Settings")]
    public PlayerController navigator;
    public Slider playerSpeedSlider;

    [Header("Font Size Controls")]
    public Slider fontSizeSlider;
    public List<TMP_Text> resizableTexts; // assign all texts you want to resize
    

    //All doors of the Scene
    private List<string> allDoors = new List<string>
    {
        "N53door1", "N53door2", "N28door", "N18door", "N76door1", "N76door2", "N16door1", "N16door2", "N66door", "caferosadoor"
    };

    void Start()
    {
        //Set toogle to first person view when start the game 
        if (firstPersonToggle != null)
        {
            firstPersonToggle.isOn = true;
            OnFirstPersonToggled(true);
            firstPersonToggle.onValueChanged.AddListener(OnFirstPersonToggled);
        }

        if (thirdPersonToggle != null)
        {
            thirdPersonToggle.isOn = false;
            thirdPersonToggle.onValueChanged.AddListener(OnThirdPersonToggled);
        }

        if (accessibilityToggle != null)
        {
            accessibilityToggle.isOn = false;
            accessibilityToggle.onValueChanged.AddListener(OnAccessibilityToggled);
        }

        //Set up the slider
        if (fontSizeSlider != null)
        {
            fontSizeSlider.onValueChanged.AddListener(OnFontSizeChanged);
        }

        if (playerSpeedSlider != null)
        {
            playerSpeedSlider.onValueChanged.AddListener(OnSpeedChanged);
        }


        RefreshDoorDropdowns(includeInaccessible: true); // by default show all
    }

    // Shows the menu panel
    public void ShowMenu()
    {
        startPanel.SetActive(false);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    // Shows the settings panel
    public void ShowSettings()
    {
        startPanel.SetActive(false);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Returns to the start screen
    public void BackToStart()
    {
        startPanel.SetActive(true);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    // Handles first-person toggle
    private void OnFirstPersonToggled(bool isOn)
    {
        if (isOn)
        {
            if (thirdPersonToggle != null) thirdPersonToggle.isOn = false;
            if (navigator != null) navigator.SetViewMode(false);
        }
    }

    // Handles third-person toggle
    private void OnThirdPersonToggled(bool isOn)
    {
        if (isOn)
        {
            if (firstPersonToggle != null) firstPersonToggle.isOn = false;
            if (navigator != null) navigator.SetViewMode(true);
        }
    }

    // Handles accessibility toggle to filter doors
    private void OnAccessibilityToggled(bool isOn)
    {
        RefreshDoorDropdowns(!isOn);
    }

    // Refreshes start and end dropdowns based on accessibility setting
    public void RefreshDoorDropdowns(bool includeInaccessible)
    {
        if (startDropdown == null || endDropdown == null)
        {
            Debug.LogWarning(".");
            return;
        }

        // Then safely continue...
        List<string> filtered = allDoors;

        if (!includeInaccessible)
        {
            filtered = allDoors.Where(name =>
                name != "N16door1" &&
                name != "N76door2"
            ).ToList();
        }

        startDropdown.ClearOptions();
        endDropdown.ClearOptions();
        startDropdown.AddOptions(filtered);
        endDropdown.AddOptions(filtered);
    }

    // Adjusts font size for all resizable texts
    private void OnFontSizeChanged(float newSize)
    {
        foreach (TMP_Text text in resizableTexts)
        {
            if (text != null)
            {
                text.fontSize = newSize;
            }
        }
    }
    
    // Updates the navigator's speed based on the slider
    public void OnSpeedChanged(float speed)
    {
        if (navigator != null)
        {
            navigator.SetAgentSpeed(speed);
        }
    }


}
