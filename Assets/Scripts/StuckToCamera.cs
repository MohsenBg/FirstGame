using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckToCamera : MonoBehaviour
{
    private Transform PlayerTransform;
    [SerializeField]
    private float minX = -60, maxX = 60;
    // Start is called before the first frame update
    private Vector3 BasePotion;
    void Start()
    {
        BasePotion = transform.position;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPotion = transform.position;
        newPotion.x = PlayerTransform.position.x + BasePotion.x;
        if (newPotion.x > minX + BasePotion.x && newPotion.x < maxX + BasePotion.x)
            transform.position = newPotion;
    }
}
