using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] arrowKeyPrefabs;
    private float spawnPosX = -2f;
    private float spawnPosY = -0.5f;
    private float distanceBetweenArrows = 2f;
    private int numEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> SpawnWave(int waveNumber)
    {
        List<GameObject> arrowKeyList = new List<GameObject>();

        DetermineEnemyNumber(waveNumber);
        DetermineSpawnPosX(numEnemies);

        for (int i = 0; i < numEnemies; i++)
        {
            int arrowIndex = Random.Range(0, arrowKeyPrefabs.Length);

            spawnPosX += distanceBetweenArrows;
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0);

            GameObject arrowKey = Instantiate(arrowKeyPrefabs[arrowIndex], spawnPos, arrowKeyPrefabs[arrowIndex].transform.rotation);
            arrowKey.GetComponent<ArrowKey>().spawnPos = spawnPos;

            // store the copied object in an list
            arrowKeyList.Add(arrowKey);
            
        }

        return arrowKeyList;
        
    }

    public void spawnExistingWave(List<GameObject> arrowKeyList)
    {
        // delete all existing arrows
        foreach (ArrowKey arrowKey in FindObjectsOfType<ArrowKey>())
        {
            Destroy(arrowKey);
        }

        // spawn previous wave
        foreach (GameObject arrowKey in arrowKeyList)
        {
            Instantiate(arrowKey, arrowKey.GetComponent<ArrowKey>().spawnPos, arrowKey.transform.rotation);
        }
    }

    void DetermineEnemyNumber(int waveNumber)
    {
        if (waveNumber == 1)
        {
            numEnemies = 4;
        }
        else if (waveNumber == 2)
        {
            numEnemies = 6;
        }
        else if (waveNumber == 3 || waveNumber == 4 || waveNumber == 5)
        {
            numEnemies = 7;
        }
        else if (waveNumber == 6 || waveNumber == 7 || waveNumber == 8)
        {
            numEnemies = 8;
        }
        else if (waveNumber == 9 || waveNumber == 10)
        {
            numEnemies = 10;
        }
    }

    void DetermineSpawnPosX(int numEnemies)
    {
        // start at -2 because every loop increments by 2
        // shift starting spawn by half of total length 
        spawnPosX = -2 - (numEnemies - 1) * distanceBetweenArrows / 2; 
    }


}
