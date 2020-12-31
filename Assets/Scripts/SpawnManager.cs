using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script is a mess. Sorry to anyone who reads it.


public class SpawnManager : MonoBehaviour
{
    public GameObject[] presentPrefabs;
    public GameObject[] treeBranches;

    public TMP_Text presentCounter;
    public TMP_Text presentReturnText;

    public int roundCount = 1;
    public int presentCount = 0;
    public int presentsCollected;

    private bool treeTrigger;
    public bool treeIsRestored = false;

    // Start is called before the first frame update
    void Start()
    {
        
        SpawnPresents(1);
        RoundManager();

        presentReturnText.GetComponent<TextMeshProUGUI>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        presentCount = FindObjectsOfType<Present>().Length;
        treeTrigger = GameObject.Find("Christmas Tree").GetComponent<TreeTrigger>().isNearTree;
        presentCounter.text = "Branches: " + presentsCollected;

        if (treeTrigger && (presentCount == 0))
        {
            if (!treeIsRestored)
            {
                presentReturnText.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                AddBranchesToTree();
                RoundManager();
                treeIsRestored = true;
            }
        }

        else if (!treeTrigger)
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
        
        for (int i = 0; i < treeBranches.Length; i++)
        {
            treeBranches[i].SetActive(true);
        }
        return;
        /*Random.Range(1, treeBranches.Length)*/;

    }

    // random spawn position generator
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(0, 3.5f);
        float spawnPosZ = Random.Range(-4, 0);

        Vector3 randomSpawn = new Vector3(spawnPosX, .16f, spawnPosZ);

        return randomSpawn;
    }

    void RoundManager()
    {
        roundCount++;
        //SpawnPresents(1);
        
        presentsCollected = 0;
        treeTrigger = false;
        presentReturnText.GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
