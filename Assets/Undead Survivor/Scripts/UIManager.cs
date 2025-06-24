using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject settingsPanel;
    public GameObject howToPlayPanel;
    public GameObject mainMenuPanel;
    public GameObject exitConfirmationPanel;


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
                OnBackClick();
            else if (mainMenuPanel.activeSelf)
                OnExitClick();
        }
    }

    public void OnSettingsClick()
    {
        PlaySelectSFX();
        ShowPanel(settingsPanel);


    }

    public void OnHowToPlayClick()
    {
        PlaySelectSFX();
        ShowPanel(howToPlayPanel);
    }

    public void OnExitClick()
    {
        PlaySelectSFX();
        ShowPanel(exitConfirmationPanel);
    }

    public void OnExitYes()
    {
        PlaySelectSFX();
        StartCoroutine(ExitWithDelay());
    }

    public void OnExitNo()
    {
        PlaySelectSFX();
        ShowPanel(mainMenuPanel);
    }

    public void OnBackClick()
    {
        PlaySelectSFX();
        ShowPanel(mainMenuPanel);
    }

    public void OnCloseSettings()
    {
        PlaySelectSFX();
        ShowPanel(mainMenuPanel);
    }

    public void OnCloseHowToPlay()
    {
        PlaySelectSFX();
        ShowPanel(mainMenuPanel);
    }

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
        if (exitConfirmationPanel != null)
            exitConfirmationPanel.SetActive(panelToShow == exitConfirmationPanel);
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