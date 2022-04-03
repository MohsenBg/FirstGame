using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PAUSE : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private Button BtnQuit;
    [SerializeField] private Button BtnResume;
    [SerializeField] private Button BtnMenu;
    [SerializeField] private GameObject PauseMenu;

    private void Start()
    {
        BtnQuit.onClick.AddListener(OnClickQuitBtn);
        BtnResume.onClick.AddListener(Resume);
        BtnMenu.onClick.AddListener(OnClickMenu);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
    public void OnClickQuitBtn()
    {
        Application.Quit();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}