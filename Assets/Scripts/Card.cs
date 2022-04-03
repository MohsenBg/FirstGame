using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{

    [SerializeField] private Button btnBuy;
    [SerializeField] private Image btnBuyImage;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Text weaponName;
    [SerializeField] private GlobalState state;
    [SerializeField] private int WeaponIndex;
    [SerializeField] GameObject ChangeSlot;
    private Weapon WeaponCard;
    private float PriceWeapon;
    public void setWeaponCard(int index, Weapon w)
    {
        WeaponCard = w;
        WeaponIndex = index;
        weaponName.text = w.WeaponName;
        weaponImage.sprite = w.srWeapon.sprite;
        weaponImage.preserveAspect = true;
        Text price = btnBuy.transform.GetChild(0).GetComponent<Text>();
        price.text = w.Price.ToString() + " $";
        PriceWeapon = w.Price;
    }
    void Start()
    {
        btnBuy.onClick.AddListener(buy);
    }
    // Start is called before the first frame update
    private void Update()
    {
        if (findWeaponIsEquip(WeaponCard) != "None") changeBtnText(findWeaponIsEquip(WeaponCard));
        else if (WeaponCard.IsWeaponSold) changeBtnText("Equip");
        else changeBtnText(WeaponCard.Price.ToString());
    }
    void buy()
    {
        if (state.allWeapons[WeaponIndex].IsWeaponSold)
        {
            setChangeSlot();
            return;
        }
        if (state.Money >= PriceWeapon)
        {
            state.Money -= PriceWeapon;
            state.allWeapons[WeaponIndex].IsWeaponSold = true;
            setChangeSlot();

        }
    }

    void setChangeSlot()
    {
        ChangeSlot.SetActive(true);
        Button Slot1 = ChangeSlot.transform.GetChild(0).GetComponent<Button>();
        Button Slot2 = ChangeSlot.transform.GetChild(1).GetComponent<Button>();
        Button Slot3 = ChangeSlot.transform.GetChild(2).GetComponent<Button>();
        Slot1.onClick.AddListener(() => EquipWeapon(0));
        Slot2.onClick.AddListener(() => EquipWeapon(1));
        Slot3.onClick.AddListener(() => EquipWeapon(2));

    }
    void EquipWeapon(int slot)
    {
        List<Weapon> playerWeapon = state.PlayerWeapons;
        if (playerWeapon[slot].WeaponName == WeaponCard.WeaponName) { ChangeSlot.SetActive(false); return; }
        for (int i = 0; i < playerWeapon.Count; i++)
        {
            if (playerWeapon[i].WeaponName == WeaponCard.WeaponName)
            {
                Weapon prvWeapon = playerWeapon[slot];
                state.PlayerWeapons[slot] = WeaponCard;
                state.PlayerWeapons[i] = prvWeapon;
                ChangeSlot.SetActive(false);
                return;
            };
        }
        state.PlayerWeapons[slot] = WeaponCard;
        ChangeSlot.SetActive(false);
    }
    public void changeBtnText(string text)
    {
        Text btnText = btnBuy.transform.GetChild(0).GetComponent<Text>();
        btnText.text = text;
        if (text == btnText.text) return;
        switch (text)
        {
            case "Equip":
                btnBuyImage.color = Color.red;
                break;
            case "Slot 1":
            case "Slot 2":
            case "Slot 3":
                btnBuyImage.color = Color.green;
                break;
            default:
                break;
        }
    }
    private string findWeaponIsEquip(Weapon w)
    {
        for (int i = 0; i < state.PlayerWeapons.Count; i++)
        {
            if (state.PlayerWeapons[i].WeaponName == w.WeaponName)
                return "Slot " + (i + 1).ToString();
        }
        return "None";
    }
    public void CloseChangeSlot()
    {
        ChangeSlot.SetActive(false);
    }
}

