using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SettingController : MonoBehaviour
{

    [Header("UI Panels")]
    public GameObject StartPanel;
    public GameObject SettingPanel;

    [Header("Audio Sliders")]
    public Slider soundSlider;
    public Slider musicSlider;

    [Header("Audio Sources")]
    public AudioSource footstepAudioSource;
    public AudioSource clickAudioSource;

    [Header("Click Sound")]
    public AudioClip clickSound;
    private AudioSource uiAudioSource;

    void Start()
    {
        // Set up internal AudioSource for UI sounds
        uiAudioSource = GetComponent<AudioSource>();

        // Slider setup
        if (soundSlider != null && footstepAudioSource != null)
        {
            soundSlider.value = footstepAudioSource.volume;
            soundSlider.onValueChanged.AddListener(SetFootstepVolume);
        }

        if (musicSlider != null && clickAudioSource != null)
        {
            musicSlider.value = clickAudioSource.volume;
            musicSlider.onValueChanged.AddListener(SetClickVolume);
        }
    }

    // Sets footstep audio volume from slider
    public void SetFootstepVolume(float volume)
    {
        if (footstepAudioSource != null)
        {
            footstepAudioSource.volume = volume;
        }
    }
    
        public void SetClickVolume(float volume)
    {
        if (clickAudioSource != null)
            clickAudioSource.volume = volume;
    }

    // Sets click audio volume from slider
    public void ShowSettings()
    {
        StartPanel.SetActive(false);
        SettingPanel.SetActive(true);
    }

    
    // Displays the settings panel
    public void BackToStart()
    {
        SettingPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    // Plays a click sound for UI interactions
    public void PlayClick()
    {
        if (clickSound != null && uiAudioSource != null)
        {
            uiAudioSource.PlayOneShot(clickSound);
        }
    }
}
