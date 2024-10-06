using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour
{
    // fire rate
    float fireRate;

    //laser object
    [SerializeField]
    GameObject laser;

    //laser spawnpoint
    [SerializeField]
    GameObject firePoint;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;
        if ( fireRate < 0f )
        {
            fireRate = Random.Range(.5f, 1f);
            Instantiate(laser, firePoint.transform);
        }
        
    }
}
