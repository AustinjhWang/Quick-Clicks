using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] arrowKeyPrefabs;
    private float spawnPosX = -1f;
    private float spawnPosY = -0.5f;
    private float distanceBetweenArrows = 2f;

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
        for (int i = 0; i < waveNumber; i++)
        {
            int arrowIndex = Random.Range(0, arrowKeyPrefabs.Length);

            spawnPosX += distanceBetweenArrows;
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0);

            // store the copied object in an list
            arrowKeyList.Add(Instantiate(arrowKeyPrefabs[arrowIndex], spawnPos, arrowKeyPrefabs[arrowIndex].transform.rotation));
            
        }

        spawnPosX = -1;
        return arrowKeyList;
        
    }


}
