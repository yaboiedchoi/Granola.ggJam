using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFightsManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bulletPrefab, new Vector3(0, 12, 0), Quaternion.identity, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
