using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] presentPrefabs;
    public GameObject[] treeBranches;

    public TMP_Text presentCounter;
    public TMP_Text presentReturnText;

    public int roundCount = 1;
    public int presentCount = 0;
    public int presentsCollected;

    private bool presentsAreReturned;

    // Start is called before the first frame update
    void Start()
    {
        
        SpawnPresents(roundCount);
        RoundManager();

        presentReturnText.GetComponent<TextMeshProUGUI>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        presentCount = FindObjectsOfType<Present>().Length;
        presentsAreReturned = GameObject.Find("Christmas Tree").GetComponent<TreeTrigger>().isNearTree;
        presentCounter.text = "Presents: " + presentsCollected + " /" + roundCount;

        if (presentsAreReturned && (presentCount == 0))
        {
            presentReturnText.GetComponent<TextMeshProUGUI>().enabled = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                AddBranchesToTree();
                RoundManager();
                
            }
        }

        else if (!presentsAreReturned)
        {
            presentReturnText.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    // For Loop that increases number of presents spawned each round
    void SpawnPresents(int presentstoSpawn)
    {
        for(int i = 0; i < presentstoSpawn; i++)
        {
            int randomPresent = Random.Range(0, presentPrefabs.Length);
            Instantiate(presentPrefabs[randomPresent], GenerateSpawnPosition(), presentPrefabs[randomPresent].transform.rotation);
        }
    }

    void AddBranchesToTree()
    {
        for(int i = 0; i < presentsCollected; i++)
        {
            treeBranches[i].SetActive(true);
            
        }
        
        /*Random.Range(1, treeBranches.Length)*/;
        
    }

    // random spawn position generator
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(0, 4);
        float spawnPosZ = Random.Range(-8, 0);

        Vector3 randomSpawn = new Vector3(spawnPosX, .16f, spawnPosZ);

        return randomSpawn;
    }

    void RoundManager()
    {
        roundCount++;
        SpawnPresents(roundCount);
        
        presentsCollected = 0;
        presentsAreReturned = false;
        presentReturnText.GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
