using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFightManager : MonoBehaviour
{

    // singleton
    private static SpaceFightManager _instance;
    public static SpaceFightManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    [SerializeField] private List<GameObject> bulletList;

    public List<GameObject> BulletList
    {
        get { return bulletList; }
        set { bulletList = value; }
    }
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
