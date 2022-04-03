using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform Fill;
    private Transform PlayerTransform;

    private float RatioSizeBarToHeath;
    public float MaxHealthBar = 50f;
    [HideInInspector] public float MaxHealth_Bar = 50f;
    [SerializeField] private float Health;

    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Health = MaxHealthBar;
        RatioSizeBarToHeath = Fill.localScale.x / Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(5);
        }
    }
    public float TakeDamage(float damagePower)
    {
        if (Health - damagePower < 0) Health = 0;
        else Health -= damagePower;
        UpdateHealthBar();
        return Health;
    }
    private void UpdateHealthBar()
    {
        float ConvertHeathToSizeOfHeathBar = Health * RatioSizeBarToHeath;
        Vector3 vectorScaleHeathBar = Fill.localScale;
        vectorScaleHeathBar.x = ConvertHeathToSizeOfHeathBar;
        Fill.localScale = vectorScaleHeathBar;
    }
    void FlipX()
    {
        float distancePlayerAndEnemy = PlayerTransform.position.x - transform.position.x;
        if (Mathf.Abs(distancePlayerAndEnemy) < 1) return;
        bool isZombieRightOfPlayer = transform.position.x > PlayerTransform.position.x ? true : false;
        if (isZombieRightOfPlayer)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {

            transform.localScale = new Vector3(-1, 1, 0);
        }

    }
}
