using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public float Money;
    public List<Weapon> allWeapons;

    public List<Weapon> PlayerWeapons;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
