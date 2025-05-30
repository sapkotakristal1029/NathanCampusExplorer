using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGamePanel : MonoBehaviour
{
    [Header("Speed Control")]
    public Slider speedSlider;
    public PlayerController navigator;


    [Header("Pause Control")]
    public Button pauseButton;
    public TMP_Text pauseButtonText;
    private bool isPaused = false;

    [Header("UI Panels")]
    public GameObject PausedPanel;
    public GameObject inGamePanel;

    // Initializes speed slider and pause button listeners
    void Start()
    {
        if (speedSlider != null)
        {
            speedSlider.onValueChanged.AddListener(OnSpeedChanged);
            if (navigator != null)
            {
                speedSlider.value = navigator.agent.speed;
            }
        }

        // Pause Button Setup
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
    }

    // Updates navigator speed based on slider value
    void OnSpeedChanged(float value)
    {
        if (navigator != null)
        {
            navigator.SetAgentSpeed(value);
        }
    }

    // Toggles pause state, updates UI, and shows/hides pause panel
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pauseButtonText != null)
        {
            pauseButtonText.text = isPaused ? "Resume" : "Pause";
        }

        if (navigator != null)
            navigator.PauseFootsteps(isPaused);



        if (inGamePanel.activeSelf && isPaused)
        {
            PausedPanel.SetActive(true);
        }
        else
        {
            PausedPanel.SetActive(false);
        }
    }

    // Resumes the game if it's currently paused
    public void ResumeIfPaused()
    {
        if (isPaused)
            TogglePause(); // same method toggles it back
    }
    
    
}
