using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    public string WeaponName;
    public float DamageWeapon;
    public bool IsWeaponSold = false;
    public float Price = 1000;

    [SerializeField] private ParticleSystem FireGun;
    [SerializeField] private Transform StartFirePoint;
    [SerializeField] private float coolDownTime;
    public GameObject BulletPrefab;
    public bool IsWeaponMelee;
    public SpriteRenderer srWeapon;
    private float timeAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(StartFirePoint);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shot()
    {
        if (timeAttack > 0) timeAttack -= Time.deltaTime;
        else
        {
            FireGun.Play();
            GameObject Bullet = Instantiate(BulletPrefab, StartFirePoint.position, StartFirePoint.rotation);
            Bullet.GetComponent<Bullet>().setDamageWeapon(DamageWeapon);
            timeAttack = coolDownTime;
        }
    }
}
