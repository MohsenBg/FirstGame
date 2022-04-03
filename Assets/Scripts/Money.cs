using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] private GlobalState state;

    [SerializeField] private Text MoneyText;
    // Start is called before the first frame update
    void Start()
    {
        MoneyText.text = state.Money.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = state.Money.ToString() + "$";
    }
}
