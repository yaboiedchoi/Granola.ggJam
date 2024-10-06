using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFightManager : MonoBehaviour
{
    [SerializeField] List<GameObject> bulletList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletList.Count != 4)
        {
            Debug.Log("YOU LOSE");
            Manager.Instance.EndMiniGame(false, true);
        }
        else if (Manager.Instance.MiniGameTime <= 0)
        {
            Debug.Log("YOU WIN");
            Manager.Instance.EndMiniGame(true, true);
        }
    }
}