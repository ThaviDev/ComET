using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    int _currentScreen;
    int _lastScreen; // Esto es para checar si se cambió de pantalla

    [Header("Screens")]
    [SerializeField] GameObject _s_titleScreen;
    [SerializeField] GameObject _s_mainMenu;
    [SerializeField] GameObject _s_options;
    [SerializeField] GameObject _s_credits;
    [SerializeField] GameObject _s_gamemodeSelect;

    [Header("EventSystem")]
    [SerializeField] EventSystem _eventSystem;

    [Header("FirstSelectedButton")]
    [SerializeField] GameObject _btnTitleScreen;
    [SerializeField] GameObject _btnMainMenu;
    [SerializeField] GameObject _btnOptions;
    [SerializeField] GameObject _btnCredits;
    [SerializeField] GameObject _btnGamemodeSelect;
    void Start()
    {
        _s_mainMenu.SetActive(false);
        _s_options.SetActive(false);
        _s_credits.SetActive(false);
        _s_gamemodeSelect.SetActive(false);

        _s_titleScreen.SetActive(true);

        _currentScreen = 0;
        _lastScreen = 0;
        CheckCurrentScreen();
    }

    void Update()
    {
        if (_lastScreen != _currentScreen)
        {
            CheckCurrentScreen();
            _lastScreen = _currentScreen;
        }
    }

    private void CheckCurrentScreen()
    {
        // Pantalla a desactivar
        switch (_lastScreen)
        {
            case 0:
                _s_titleScreen.SetActive(false);
                break;
            case 1: 
                _s_mainMenu.SetActive(false);
                break;
            case 2:
                _s_options.SetActive(false);
                break;
            case 3:
                _s_credits.SetActive(false);
                break;
            case 4:
                _s_gamemodeSelect.SetActive(false);
                break;
            default: 
                break;
        }
        // Pantalla a activar
        switch (_currentScreen) {
            case 0:
                _s_titleScreen.SetActive(true);
                _eventSystem.SetSelectedGameObject(_btnTitleScreen);
                break;
            case 1:
                _s_mainMenu.SetActive(true);
                _eventSystem.SetSelectedGameObject(_btnMainMenu);
                break;
            case 2:
                _s_options.SetActive(true);
                _eventSystem.SetSelectedGameObject(_btnOptions);
                break;
            case 3:
                _s_credits.SetActive(true);
                _eventSystem.SetSelectedGameObject(_btnCredits);
                break;
            case 4:
                _s_gamemodeSelect.SetActive(true);
                _eventSystem.SetSelectedGameObject(_btnGamemodeSelect);
                break;
            default:
                break;
        }
    }
    public void GoToMainMenu()
    {
        _currentScreen = 1;
    }
    public void GoToOptions()
    {
        _currentScreen = 2;
    }
    public void GoToCredits()
    {
        _currentScreen = 3;
    }
    public void GoToGamemodeSelect()
    {
        _currentScreen = 4;
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
