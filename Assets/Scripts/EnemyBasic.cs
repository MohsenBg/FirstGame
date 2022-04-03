using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    [SerializeField] private GlobalState state;
    public float EnemySpeed = 3;
    [SerializeField] private float StopRange = 1;
    [SerializeField] private Animator BloodAnimator;
    [SerializeField] private float DelayDie = 0;
    [SerializeField] private float coolDownTime;
    [SerializeField] private ParticleSystem blood;
    //baseDamage on start
    public float damage_Power;
    //chang on waves
    [HideInInspector] public float damagePower;
    public HealthBar HealthEnemy;
    public float Money_Drop;

    public float MoneyDrop = 10;

    private Player Player;
    private float timeAttack;
    private Transform PlayerTransform;
    private float diraction;
    private Animator EnemyAnimator;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyAnimator = GetComponent<Animator>();
        timeAttack = coolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform != null)
        {
            setScaleXEnemy();
            EnemyMoveToPlayer();
        }
        else EnemyAnimator.SetBool("isWalking", false);
    }

    private void setScaleXEnemy()
    {
        float distancePlayerAndEnemy = PlayerTransform.position.x - transform.position.x;
        if (Mathf.Abs(distancePlayerAndEnemy) < 1) return;
        bool isZombieRightOfPlayer = transform.position.x > PlayerTransform.position.x ? true : false;
        if (isZombieRightOfPlayer)
        {
            transform.localScale = new Vector3(-1, 1, 0);
            diraction = -1;
        }
        else
        {

            transform.localScale = new Vector3(1, 1, 0);
            diraction = 1;
        }
    }

    private void EnemyMoveToPlayer()
    {
        float distancePlayerAndEnemy = PlayerTransform.position.x - transform.position.x;
        if (Mathf.Abs(distancePlayerAndEnemy) < StopRange)
        {
            EnemyAnimator.SetBool("isWalking", false);
            Attack();
        }
        else
        {
            EnemyAnimator.SetBool("isWalking", true);
            transform.Translate(new Vector3(diraction * EnemySpeed, 0, 0) * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (timeAttack > 0)
        {
            timeAttack -= Time.deltaTime;
        }
        else
        {
            EnemyAnimator.SetTrigger("Attack");
            Player.TakeDamage(damagePower);
            timeAttack = coolDownTime;
        }
    }
    public void TakeDamage(float damageReceve)
    {
        float Health = HealthEnemy.TakeDamage(damageReceve);

        if (Health == 0)
        {
            Destroy(gameObject, DelayDie);
            state.Money += MoneyDrop;
            Waves mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Waves>();
            mainCameraScript.countEnemyAlive -= 1;
            BloodAnimator.SetTrigger("Die");
        }
        else
        {
            blood.Play();
        }
    }
}
