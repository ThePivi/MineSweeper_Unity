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
    private bool menuActive = true;
    public bool coverActive = false;
    public bool loseActive = false;
    public bool winActive = false;
    public Button newGameButton;
    private int score;
    public TextMeshProUGUI scoreText;
    public void ToggleNewGameButtonTo(bool status) {
        newGameButton.interactable = status;
    }
    public void HideMenu () {
        //menu.SetActive(false);
        menuActive = false;
    }
    public void ShowMenu () {
        //menu.SetActive(true);
        ToggleNewGameButtonTo(true);
        menuActive = true;
    }
    public void ShowCover () {
        //loading.SetActive(true);
        coverActive = true;
    }
    public void HideCover () {
        //loading.SetActive(false);
        coverActive = false;
    }
    public void ShowLose () {
        //lose.SetActive(true);
        coverActive = true;
        loseActive = true;
    }
    public void HideLose () {
        //menu.SetActive(true);
        //lose.SetActive(false);
        loseActive = false;
        menuActive = true;
    }
    public void ShowWin (int newScore) {
        //win.SetActive(true);
        coverActive = true;
        score += newScore;
        scoreText.text = "Total score: " + score + "";
        winText.text = "You found " + newScore + " mines!";
        winActive = true;
    }
    public void HideWin () {
        //menu.SetActive(true);
        //win.SetActive(false);
        winActive = false;
        menuActive = true;
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
            } else if (!loseActive && !winActive) {
                ShowMenu();
            }
        }
        menu.SetActive(menuActive);
        cover.SetActive(coverActive);
        lose.SetActive(loseActive);
        win.SetActive(winActive);
    }
}
