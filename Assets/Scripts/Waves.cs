using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Waves : MonoBehaviour
{

    [SerializeField] private List<EnemyBasic> Enemies;
    [SerializeField] private float PercentDamageIncrese = 5;
    [SerializeField] private float PercentHeathIncrese = 3;
    [SerializeField] private Text WaveText;
    public int countEnemyAlive = 8;
    private float countSpawnEnemy;
    private float coolDownTime = 0;
    [SerializeField] private int Waves_Number = 1;
    [HideInInspector] public int WaveNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        setEnemyDamage();
        setEnemyHeath();
        setMoneyDrop();
        setCountEnemy();
    }
    // Update is called once per frame
    void Update()
    {
        if (WaveNumber != Waves_Number)
        {
            WaveNumber = Waves_Number;
            WaveText.text = "Wave " + WaveNumber.ToString() + "/∞";
            setEnemyDamage();
            setEnemyHeath();
            setCountEnemy();
            setMoneyDrop();
        }
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        if (countEnemyAlive == 0)
        {
            if (WaveNumber == 20)
            {
                WinGame();
                // Maybe there is no win
                // return;
            };
            Waves_Number++;
            return;
        }
        if (coolDownTime > 0) coolDownTime -= Time.deltaTime;
        else if (countSpawnEnemy > 0)
        {
            int randomIndex = Random.Range(0, Enemies.Count - 1);
            float PositiveOrNegative = Random.Range(0, 2) >= 1 ? 1 : -1;
            Instantiate(Enemies[randomIndex].gameObject, new Vector3(70 * PositiveOrNegative, 0, 0), new Quaternion(0, 0, 0, 0));
            coolDownTime = 2;
            countSpawnEnemy -= 1;
        }
    }
    private void setEnemyDamage()
    {
        //PRECENT
        float DamagePower = Mathf.Pow(((PercentDamageIncrese + 100) / 100), WaveNumber);
        for (int i = 0; i < Enemies.Count; i++)
        {

            Enemies[i].damagePower = Enemies[i].damage_Power * DamagePower;
        }
    }
    private void setMoneyDrop()
    {
        //PRECENT
        float MoneyDrop = Mathf.Pow(1.2f, WaveNumber);
        for (int i = 0; i < Enemies.Count; i++)
        {

            Enemies[i].MoneyDrop = (int)Enemies[i].Money_Drop * MoneyDrop;
        }
    }
    private void setEnemyHeath()
    {
        //PRECENT
        int Health = (int)Mathf.Pow(((PercentHeathIncrese + 100) / 100), WaveNumber);
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].HealthEnemy.MaxHealthBar = Enemies[i].HealthEnemy.MaxHealth_Bar * Health;
        }
    }
    private void setCountEnemy()
    {
        countEnemyAlive = (int)Mathf.Pow(1.13f, WaveNumber + 8);
        countSpawnEnemy = countEnemyAlive;
    }
    private void WinGame()
    {
        // is there any to wine this Game ?
        // Mohsen_BG
    }
}
