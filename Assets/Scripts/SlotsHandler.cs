using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsHandler : MonoBehaviour
{
    [SerializeField] GameObject[] Slots = new GameObject[3];
    [SerializeField] private GlobalState GlobalState;
    private Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for (int i = 0; i < 3; i++)
        {
            Image imageWeapon = Slots[i].transform.GetChild(0).GetComponent<Image>();
            if (GlobalState.PlayerWeapons[i] != null)
            {
                imageWeapon.sprite = GlobalState.PlayerWeapons[i].srWeapon.sprite;
                imageWeapon.preserveAspect = true;
                if (GlobalState.PlayerWeapons[i].WeaponName == "Empty") imageWeapon.color = new Color(0, 0, 0, 0);
            }

            else
                imageWeapon.color = new Color(0, 0, 0, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) player.SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) player.SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) player.SelectWeapon(2);
    }
}
