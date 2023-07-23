using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class MenuController : MonoBehaviour
{

    public GameObject menu;
    public GameObject cover;
    public GameObject lose;
    public GameObject win;
    public TextMeshProUGUI winText;
    public bool menuActive = true;
    public bool coverActive = false;
    public bool loseActive = false;
    public bool winActive = false;
    public bool gameRunning = false;
    public Button newGameButton;
    private int score;
    public TextMeshProUGUI scoreText;
    public void ToggleNewGameButtonTo(bool status) {
        newGameButton.interactable = status;
    }
    public void HideMenu () {
        menuActive = false;
    }
    public void ShowMenu () {
        ToggleNewGameButtonTo(true);
        menuActive = true;
    }
    public void ShowCover () {
        coverActive = true;
    }
    public void HideCover () {
        coverActive = false;
    }
    public void ShowLose () {
        ToggleGameRunning(false);
        loseActive = true;
    }
    public void HideLose () {
        loseActive = false;
        menuActive = true;
    }
    public void ShowWin (int newScore) {
        ToggleGameRunning(false);
        score += newScore;
        scoreText.text = "Total score: " + score + "";
        winText.text = "You found " + newScore + " mines!";
        winActive = true;
    }
    public void HideWin () {
        winActive = false;
        menuActive = true;
    }
    public void ToggleGameRunning (bool isRunnuing) {
        gameRunning = isRunnuing;
    }

    private static MenuController instance;
    public static MenuController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<MenuController>();

                if (instance == null) {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<MenuController>();
                    singletonObject.name = "MenuController";
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }
    }
    private void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        ShowMenu();
    }

    // Update is called once per frame
    public void Quit () {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (menuActive && FindObjectOfType<MapGenerator>().GetGameIsActive()) {
                HideMenu();
                if (!gameRunning) {
                    ShowCover();   
                }
            } else if (!loseActive && !winActive) {
                ShowMenu();
                HideCover();
            }
        }
        menu.SetActive(menuActive);
        cover.SetActive(coverActive);
        lose.SetActive(loseActive);
        win.SetActive(winActive);
    }
}
