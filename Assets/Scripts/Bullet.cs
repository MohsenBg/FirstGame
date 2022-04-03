using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float SpeedBullet;
    [SerializeField] private float CountBullet;
    [SerializeField] private float RangeBullet;
    [SerializeField] private Transform StartFirePoint;
    private float DamageWeapon;

    private Rigidbody2D BulletRb2d;
    // Start is called before the first frame update
    void Start()
    {
        BulletRb2d = GetComponent<Rigidbody2D>();
        StartFirePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;
        BulletRb2d.AddForce(StartFirePoint.right * SpeedBullet, ForceMode2D.Impulse);
    }
    public void setDamageWeapon(float damagePower)
    {
        DamageWeapon = damagePower;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        checkRangeBullet();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                other.gameObject.GetComponent<EnemyBasic>().TakeDamage(DamageWeapon);
                Destroy(gameObject);
                break;
            case "Ground":
            case "Wall":
                Destroy(gameObject);
                break;

            default:
                break;
        }
    }
    private void checkRangeBullet()
    {
        if (gameObject == null || StartFirePoint == null) return;
        Vector3 BulletPostion = transform.position - StartFirePoint.position;
        if (Mathf.Abs(BulletPostion.x) > RangeBullet || Mathf.Abs(BulletPostion.y) > RangeBullet)
            Destroy(gameObject);
    }

}
