using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField]  // vacuum from player
    GameObject vacuum;

    private BoxCollider2D vacuumCollider;

    [SerializeField]
    GameObject ghostPrefab;  // ghost prefab


    private Vector2 platformBL;   // bottom left platform
    private Vector2 platformBR;  // bottom right platform
    private Vector2 platformM;  // middle platform
    private Vector2 platformTL;  // top left platform
    private Vector2 platformTR;  // top right platform

    private List<Vector2> platformLocations;  // list of platform locations

    [SerializeField]
    List<GameObject> spawnedGhosts;  // list of spawned ghosts



    // start method
    void Start()
    {
        // platfrom locations set
        platformBL = new Vector2(-4.3f, -1.5f);
        platformBR = new Vector2(4.3f, -1.5f);
        platformM = new Vector2(0.0f, 0.7f);
        platformTL = new Vector2(-4.3f, 2.9f);
        platformTR = new Vector2(4.3f, 2.9f);

        // platform location list made
        platformLocations = new List<Vector2>();

        // add each platform location to platform location list
        platformLocations.Add(platformBL);
        platformLocations.Add(platformBR);
        platformLocations.Add(platformM);
        platformLocations.Add(platformTL);
        platformLocations.Add(platformTR);

        vacuumCollider = vacuum.GetComponent<BoxCollider2D>();  // get collider for vacuum

        SpawnGhost();
    }


    // update method
    void Update()
    {
        VacuumHitGhost();  // check if vacuum collides with ghost, and if it does, ghost disappears
        if (spawnedGhosts.Count == 0)
        {
            Manager.Instance.EndMiniGame(true, true);
        }
        else if (Manager.Instance.MiniGameTime <= 0)
        {
            Manager.Instance.EndMiniGame(false, true);
        }
    }



    // spawn ghost method
    private void SpawnGhost()
    {
        // loop 3 times
        for (int i = 0; i < 3; i++) 
        {
            int platformPick = Random.Range(0, platformLocations.Count);  // pick one of the platforms
            
            GameObject newGhost = Instantiate(ghostPrefab, platformLocations[platformPick], Quaternion.identity, this.transform); // spawn a ghost at that platform
            spawnedGhosts.Add(newGhost);  // add new ghost to list

            platformLocations.RemoveAt(platformPick);  // Remove that platform from the list
        }

    }


    // vacuum collding with ghost method
    private void VacuumHitGhost()
    {
        for (int i = 0; i < spawnedGhosts.Count; i++)
        {
            if (vacuumCollider.IsTouching(spawnedGhosts[i].GetComponent<BoxCollider2D>()))
            {
                Destroy(spawnedGhosts[i]);
                spawnedGhosts.Remove(spawnedGhosts[i]);
            }
        }
    }
}
