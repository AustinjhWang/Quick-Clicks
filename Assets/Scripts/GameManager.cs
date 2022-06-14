using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int waveNumber = 1;
    private SpawnManager spawnManager;
    private int arrowCount;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        arrowCount = FindObjectsOfType<ArrowKey>().Length;
        if (arrowCount == 0)
        {
            spawnManager.SpawnWave(waveNumber);
            waveNumber++;
        }
    }
}
