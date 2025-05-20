using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class MenuController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject menuPanel;
    public GameObject settingsPanel;

    public Toggle firstPersonToggle;
    public Toggle thirdPersonToggle;
    public Toggle accessibilityToggle;

    public TMP_Dropdown startDropdown;
    public TMP_Dropdown endDropdown;

    public ThirdPersonAutoNavigator navigator;

    public Slider fontSizeSlider;
    public List<TMP_Text> resizableTexts; // assign all texts you want to resize

    public Slider playerSpeedSlider;

    private List<string> allDoors = new List<string>
    {
        "N53door1", "N53door2", "N28door", "N18door", "N76door1", "N16door1", "N76door2", "N66door", "caferosadoor", "N16door 2"
    };

    private HashSet<string> inaccessibleDoors = new HashSet<string> { "N16door1", "N76door2" };

    void Start()
    {
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


    public void ShowMenu()
    {
        startPanel.SetActive(false);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        startPanel.SetActive(false);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToStart()
    {
        startPanel.SetActive(true);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void OnFirstPersonToggled(bool isOn)
    {
        if (isOn)
        {
            if (thirdPersonToggle != null) thirdPersonToggle.isOn = false;
            if (navigator != null) navigator.SetViewMode(false);
        }
    }

    private void OnThirdPersonToggled(bool isOn)
    {
        if (isOn)
        {
            if (firstPersonToggle != null) firstPersonToggle.isOn = false;
            if (navigator != null) navigator.SetViewMode(true);
        }
    }

    private void OnAccessibilityToggled(bool isOn)
    {
        RefreshDoorDropdowns(!isOn);
    }

    public void RefreshDoorDropdowns(bool includeInaccessible)
    {
        if (startDropdown == null || endDropdown == null)
        {
            Debug.LogWarning("Dropdowns not assigned in MenuController.");
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
    
    private void OnSpeedChanged(float speed)
    {
        if (navigator != null)
        {
            navigator.SetAgentSpeed(speed);
        }
    }


}
