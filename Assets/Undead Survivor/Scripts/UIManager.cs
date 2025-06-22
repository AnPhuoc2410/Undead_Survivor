using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject settingsPanel;
    public GameObject howToPlayPanel;
    public GameObject mainMenuPanel;

    
    [Header("Audio")]
    public bool enableSFX = true;

    void Start()
    {
        // Ensure main menu is visible and other panels are hidden on start
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);

    }

    // Settings button click handler
    public void OnSettingsClick()
    {
        PlaySelectSFX();
        
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(false);
        }
    }

    // How To Play button click handler
    public void OnHowToPlayClick()
    {
        PlaySelectSFX();
        
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true);
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(false);
        }
    }

    // Exit button click handler
    public void OnExitClick()
    {
        PlaySelectSFX();
        
        // Add a small delay to let the SFX play before exiting
        StartCoroutine(ExitWithDelay());
    }

    // Back button handler (to return to main menu)
    public void OnBackClick()
    {
        PlaySelectSFX();
        
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    // Close settings panel
    public void OnCloseSettings()
    {
        PlaySelectSFX();
        
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    // Close how to play panel
    public void OnCloseHowToPlay()
    {
        PlaySelectSFX();
        
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    // Play Select SFX if enabled and AudioManager exists
    private void PlaySelectSFX()
    {
        if (enableSFX && AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(SFX.Select);
        }
    }

    private System.Collections.IEnumerator ExitWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to let SFX play
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 