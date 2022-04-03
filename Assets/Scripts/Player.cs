using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float minHP = 0, maxHP = 100;
    [SerializeField] private Slider HP_Bar;
    [SerializeField] private Transform AttackPoints;
    [SerializeField] private float AttackRange;
    [SerializeField] private LayerMask EnemyLayers;
    [SerializeField] private float coolDownTime = 0;
    [SerializeField] private Transform RightShoulder;
    [SerializeField] private Transform RightHand;
    [SerializeField] private Animator UpperBodyAnimator;
    [SerializeField] private GameObject CavanGameOver;
    [SerializeField] private GameObject CavanGame;
    [SerializeField] private GameObject Game;
    [SerializeField] private GlobalState GlobalState;
    [SerializeField] private Texture2D aimIcon;
    private Weapon SelectedWeapon;
    private Animator LowerBodyAnimator;
    private float timeAttack;
    private float PlayerHP;
    private Rigidbody2D PlayerRb2d;
    private SpriteRenderer sr;
    private bool IsAimingWeapon = false;
    private bool isGrounded;
    private float coolDownSwitchWeapon;
    // Start is called before the first frame update
    void Start()
    {
        coolDownSwitchWeapon = 2 * Time.deltaTime;
        HP_Bar.maxValue = maxHP;
        HP_Bar.minValue = minHP;
        PlayerHP = maxHP;
        PlayerRb2d = GetComponent<Rigidbody2D>();
        LowerBodyAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        SelectWeapon(0);


    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 movenet = new Vector3(moveSpeed * x, 0, 0);
        transform.Translate(movenet * Time.deltaTime);
        if (x < 0) transform.localScale = new Vector3(-1, 1, 0);
        if (x > 0) transform.localScale = new Vector3(1, 1, 0);
        if (x != 0 && isGrounded)
        {
            LowerBodyAnimator.SetBool("isWalking", true);
            UpperBodyAnimator.SetBool("isWalking", true);
        }
        else
        {
            LowerBodyAnimator.SetBool("isWalking", false);
            UpperBodyAnimator.SetBool("isWalking", false);
        };
        if (SelectedWeapon.IsWeaponMelee)
        {

            MeleeAttack();
        }
    }
    void FixedUpdate()
    {
        HP_Bar.value = PlayerHP;
        PlayerJump();
        aiming();

    }


    private void PlayerJump()
    {
        bool isPlayerPressJumpKey = Input.GetButtonDown("Jump");
        if (isPlayerPressJumpKey && isGrounded)
        {
            LowerBodyAnimator.SetBool("isWalking", false);
            LowerBodyAnimator.SetBool("isJump", true);
            UpperBodyAnimator.SetBool("isWalking", false);
            UpperBodyAnimator.SetBool("isJump", true);
            isGrounded = false;
            PlayerRb2d.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
        }
    }
    void aiming()
    {
        bool isLeftClickMouseHold = Input.GetMouseButton(1);
        bool isWeaponMelee = SelectedWeapon.IsWeaponMelee;

        if (isLeftClickMouseHold && !isWeaponMelee)
        {
            IsAimingWeapon = true;
            Cursor.SetCursor(aimIcon, new Vector2(19, 19), CursorMode.Auto);
            UpperBodyAnimator.enabled = false;
            float ScalePlayerRotateX = transform.localScale.x;
            float ScaleRotateY = transform.localScale.x == -1 ? -1 : 1;
            RightShoulder.localScale = new Vector3(ScalePlayerRotateX, ScaleRotateY, 0);
            RightHand.localRotation = new Quaternion(0, 0, 0, 0);
            // //layer lower_body
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 perendicular = RightShoulder.position - mousePosition;
            Quaternion val = Quaternion.LookRotation(Vector3.forward, perendicular);
            //because unity circle system -90 deg diffrent form Math circle
            val *= Quaternion.Euler(0, 0, -90);
            RightShoulder.rotation = val;
            bool isRightClickMouseHold = Input.GetMouseButton(0);
            if (isRightClickMouseHold) SelectedWeapon.Shot();
        }
        else
        {
            IsAimingWeapon = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            UpperBodyAnimator.enabled = true;
            RightShoulder.localScale = new Vector3(1, 1, 0);
        }
    }
    void MeleeAttack()
    {
        bool isMouseClicked = Input.GetMouseButtonDown(0);
        if (isMouseClicked && timeAttack < 0)
        {
            Collider2D[] enemys = Physics2D.OverlapCircleAll(AttackPoints.position, AttackRange, EnemyLayers);
            LowerBodyAnimator.SetTrigger("isAttack");
            UpperBodyAnimator.SetTrigger("isAttack");
            foreach (Collider2D enemy in enemys)
            {
                enemy.GetComponent<EnemyBasic>().TakeDamage(SelectedWeapon.DamageWeapon);
            }
            timeAttack = coolDownTime;
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoints.position, AttackRange);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        bool isGameObjectTagGround = other.gameObject.CompareTag("Ground");
        if (isGameObjectTagGround)
        {
            LowerBodyAnimator.SetBool("isJump", false);
            UpperBodyAnimator.SetBool("isJump", false);
            isGrounded = true;
        }
    }
    public void TakeDamage(float damagePower)
    {

        PlayerHP -= damagePower;
        if (PlayerHP <= 0)
            GameOver();
    }

    public void SelectWeapon(int index)
    {
        GameObject currentWeapons = GameObject.FindGameObjectWithTag("Weapon");
        GameObject Prefab = GlobalState.PlayerWeapons[index].gameObject;
        GameObject child = GameObject.Instantiate(Prefab, currentWeapons.transform.position, currentWeapons.transform.rotation);
        Vector3 chidLocalSale = child.transform.localScale;
        if (transform.localScale.x == -1 && !IsAimingWeapon)
        {
            child.transform.localScale = new Vector3(
                  -chidLocalSale.x, chidLocalSale.y, chidLocalSale.z
              );
        }
        else if (transform.localScale.x == -1 && IsAimingWeapon)
        {
            child.transform.localScale = new Vector3(
                   chidLocalSale.x, -chidLocalSale.y, chidLocalSale.z
               );
        }

        SelectedWeapon = child.GetComponent<Weapon>();
        child.transform.parent = RightHand.GetChild(0).transform;
        Destroy(currentWeapons);
    }
    private void GameOver()
    {
        CavanGameOver.SetActive(true);
        CavanGame.SetActive(false);
        Game.SetActive(false);
    }
}
