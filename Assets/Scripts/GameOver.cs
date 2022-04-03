using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

    [SerializeField] Button btnTryAgain;
    [SerializeField] Button btnShop;
    [SerializeField] Button btnExit;
    [SerializeField] GameObject CavansShop;
    [SerializeField] GameObject Game;
    [SerializeField] GameObject CavanGame;

    // Start is called before the first frame update
    void Start()
    {
        btnTryAgain.onClick.AddListener(onClickbtnTryAgain);
        btnShop.onClick.AddListener(onClickbtnShop);
        btnExit.onClick.AddListener(onClickbtnExit);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void onClickbtnTryAgain()
    {
        SceneManager.LoadScene("Game");
    }
    private void onClickbtnShop()
    {
        SceneManager.LoadScene("Shop");
    }
    private void onClickbtnExit()
    {
        Application.Quit();
    }
}

