using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private Button BtnPlay;

    [SerializeField] private Button BtnExit;
    private void Start()
    {
        BtnPlay.onClick.AddListener(OnClickPlayBtn);
        BtnExit.onClick.AddListener(OnClickExitBtn);
    }

    public void OnClickExitBtn()
    {
        Application.Quit();
    }

    public void OnClickPlayBtn()
    {
        SceneManager.LoadScene("Shop");
    }
}
