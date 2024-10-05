using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Show controls for the upcoming minigame (shown when minigame starts)
    /// 
    /// </summary>
    public abstract void ShowControls();
    public abstract void ShowAssets();
}
