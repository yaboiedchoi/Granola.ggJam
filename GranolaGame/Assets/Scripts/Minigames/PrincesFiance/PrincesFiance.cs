using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class PrincesFiance : MonoBehaviour
{
    [SerializeField]
    Button redCup;

    [SerializeField]
    Button greenCup;

    [SerializeField]
    Button blueCup;

    [SerializeField]
    GameObject timeline1;

    [SerializeField]
    GameObject timeline2;

    [SerializeField]
    GameObject timeline3;

    public List<GameObject> timelines;


    // Start is called before the first frame update
    void Start()
    {
        timeline1.SetActive(false);
        timeline2.SetActive(false);
        timeline3.SetActive(false);

        timelines.Add(timeline1);
        timelines.Add(timeline2);
        timelines.Add(timeline3);

        PickTimeline();
    }


    // pick a random timeline method
    private void PickTimeline()
    {  
        int pick = Random.Range(0, timelines.Count);

        timelines[pick].SetActive(true);
    }


    // player picked poison method
    public void PickedPoison()
    {
        Debug.Log("YOU DIED");
    }


    // player picked correctly method
    public void PickedCorrect()
    {
        Debug.Log("correct!");
    }





}
