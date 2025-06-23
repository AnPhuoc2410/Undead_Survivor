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
        AudioManager.instance.PlayBGM(true);
        ShowPanel(mainMenuPanel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf || howToPlayPanel.activeSelf)
                OnBackClick(); // Back to main menu
            else if (mainMenuPanel.activeSelf)
                OnExitClick(); // Exit from main menu
        }
    }

    // Settings button click handler
    public void OnSettingsClick()
    {
        PlaySelectSFX();
        ShowPanel(settingsPanel);


    }


    // How To Play button click handler
    public void OnHowToPlayClick()
    {
        PlaySelectSFX();
        ShowPanel(howToPlayPanel);
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
        ShowPanel(mainMenuPanel);
    }

    // Close settings panel
    public void OnCloseSettings()
    {
        PlaySelectSFX();
        ShowPanel(mainMenuPanel);
    }

    // Close how to play panel
    public void OnCloseHowToPlay()
    {
        PlaySelectSFX();
        ShowPanel(mainMenuPanel);
    }

    // Play Select SFX if enabled and AudioManager exists
    private void PlaySelectSFX()
    {
        if (enableSFX && AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(SFX.Select);
        }
    }

    private void ShowPanel(GameObject panelToShow)
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(panelToShow == mainMenuPanel);
        if (settingsPanel != null)
            settingsPanel.SetActive(panelToShow == settingsPanel);
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(panelToShow == howToPlayPanel);
    }

    private System.Collections.IEnumerator ExitWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Small delay to let SFX play

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}