using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Shop : MonoBehaviour
{
    [SerializeField] private Card card;
    [SerializeField] private Text textMoney;
    [SerializeField] Transform background;
    [SerializeField] Button playBtn;
    [SerializeField] Button backBtn;
    [SerializeField] private GlobalState globalState;    // Start is called before the first frame update

    void Start()
    {

        textMoney.text = globalState.Money.ToString() + "$";
        float PositionX = -530;
        float PositionY = 400;

        for (int i = 0; i < globalState.allWeapons.Count; i++)
        {
            Weapon weapon = globalState.allWeapons[i];
            Vector3 position = new Vector3(PositionX, PositionY, 0);
            Quaternion rotate = new Quaternion(0, 0, 0, 0);
            Card newCard = Instantiate(card);
            newCard.transform.parent = background.transform;
            newCard.transform.localPosition = position;
            newCard.setWeaponCard(i, weapon);

            if ((i + 1) % 3 == 0)
            {
                PositionY -= 450;
                PositionX = -530;
            }
            else
                PositionX += 530;

        }

        playBtn.onClick.AddListener(onClickPlayBtn);
    }

    // Update is called once per frame
    void Update()
    {
        if (textMoney.text != globalState.Money.ToString()) textMoney.text = globalState.Money.ToString() + "$";
    }

    void onClickPlayBtn()
    {
        SceneManager.LoadScene("Game");
    }
    void onClickPlayBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
