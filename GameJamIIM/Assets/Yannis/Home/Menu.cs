using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Script Parameters

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject credtisMenu;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitButton;

    #endregion

    #region Unity Methods

    private void Start()
    {
        ActiveMainMenu();
        SetButtons();
    }

    #endregion

    #region Menu Methods

    private void ActiveMainMenu()
    {
        mainMenu.SetActive(true);
        credtisMenu.SetActive(false);
    }

    private void ActiveCreditsMenu()
    {
        mainMenu.SetActive(false);
        credtisMenu.SetActive(true);
    }

    #endregion

    #region Buttons Methods

    private void SetButtons()
    {
        playButton.onClick.AddListener(PlayClick);
        creditsButton.onClick.AddListener(CreditClick);
        backButton.onClick.AddListener(BackClick);
        quitButton.onClick.AddListener(QuitClick);
    }

    private void PlayClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void CreditClick()
    {
        ActiveCreditsMenu();
    }

    private void BackClick()
    {
        ActiveMainMenu();
    }

    private void QuitClick()
    {
        Application.Quit();
    }

    #endregion
}
