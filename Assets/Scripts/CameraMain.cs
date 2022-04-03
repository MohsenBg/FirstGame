using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform PlayerTransform;
    [SerializeField] private GameObject prefabZombieGreen;
    [SerializeField] private float minX, maxX;

    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        Vector3 newPotion = transform.position;
        newPotion.x = PlayerTransform.position.x;
        if (newPotion.x > minX && newPotion.x < maxX)
            transform.position = newPotion;

    }

}
