using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed that laser moves down, could speed up with game timer???
    Vector3 laserSpeed;

    //all lasers must find the player
    [SerializeField]
    Collider2D saber;

    [SerializeField]
    Collider2D laser;


    // Start is called before the first frame update
    void Start()
    {
        //all lasers must find the player, this is probably not efficient
        saber = GameObject.Find("spaceguywalker").GetComponent<Collider2D>();
        transform.position = new Vector3(UnityEngine.Random.Range(-4f, 4f),
                                            UnityEngine.Random.Range(6f, 15f));
        //find their hitbox
        laser = GetComponent<Collider2D>();
        laserSpeed = new Vector3(0f, UnityEngine.Random.Range(-6f, -2f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += laserSpeed * Time.deltaTime;
        if (transform.position.y < -8)
        {
            SpaceFightManager.Instance.BulletList.Remove(gameObject);
            Destroy(gameObject);
        }
        if (laser.bounds.Intersects(saber.bounds))
        {
            transform.position = new Vector3(UnityEngine.Random.Range(-4f, 4f),
                                            UnityEngine.Random.Range(6f, 15f));
            //Debug.Log("HIT");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");

    }
}

