using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] arrowKeyPrefabs;
    private float spawnPosX;
    private float spawnPosY = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[] SpawnWave(int waveNumber)
    {
        int[] arrowKeyList = new int[waveNumber];
        for (int i = 0; i < waveNumber; i++)
        {
            int arrowIndex = Random.Range(0, arrowKeyPrefabs.Length);
            arrowKeyList[i] = arrowIndex;

            spawnPosX = -waveNumber;
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0);

            Instantiate(arrowKeyPrefabs[arrowIndex], spawnPos, arrowKeyPrefabs[arrowIndex].transform.rotation);
        }

        return arrowKeyList;
        
    }


}
