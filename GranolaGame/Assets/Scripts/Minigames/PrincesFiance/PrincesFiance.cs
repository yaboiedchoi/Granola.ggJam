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
    PlayableDirector timeline1;

    [SerializeField]
    PlayableDirector timeline2;

    [SerializeField]
    PlayableDirector timeline3;

    public List<PlayableDirector> timelines;

    private float timer;

    int pick;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        // set as uninteractable
        redCup.interactable = false;
        greenCup.interactable = false;
        blueCup.interactable = false;

        timeline1.gameObject.SetActive(false);
        timeline2.gameObject.SetActive(false);
        timeline3.gameObject.SetActive(false);

        timelines.Add(timeline1);
        timelines.Add(timeline2);
        timelines.Add(timeline3);

        PickTimeline();
        Manager.Instance.PlayLoop("Prices Fiance Loop");
        // adds 2 seconds to minigame for shuffling
        Manager.Instance.MiniGameTime += 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // wait until timeline is done
        if (timer >= timelines[pick].duration)
        {
            // set as interactable
            redCup.interactable = true;
            greenCup.interactable = true;
            blueCup.interactable = true;
        }
        // lose state (time runs out)
        if (Manager.Instance.MiniGameTime <= 0)
        {
            Manager.Instance.EndMiniGame(false, true);
        }
    }

    // pick a random timeline method
    private void PickTimeline()
    {  
        pick = Random.Range(0, timelines.Count);

        timelines[pick].gameObject.SetActive(true);
    }


    // player picked poison method
    public void PickedPoison()
    {
        Debug.Log("YOU DIED");
        Manager.Instance.EndMiniGame(false, true);
    }


    // player picked correctly method
    public void PickedCorrect()
    {
        Debug.Log("correct!");
        Manager.Instance.EndMiniGame(true, true);
    }





}
