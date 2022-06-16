using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int waveNumber = 1;
    private SpawnManager spawnManager;
    private int arrowCount;
    private List<GameObject> arrowKeyList;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        arrowCount = FindObjectsOfType<ArrowKey>().Length;
        if (arrowCount == 0 && waveNumber <= 10)
        {
            arrowKeyList = spawnManager.SpawnWave(waveNumber);

            StartCoroutine("PlayWave", arrowKeyList);

            waveNumber++;
            
        }

        else if (waveNumber > 10)
        {
            GameOver();
        }
    }
  
   IEnumerator PlayWave(List<GameObject> arrowKeyList) 
    {
        print(waveNumber);

        foreach (GameObject arrowKey in arrowKeyList)
        {
            string direction = arrowKey.GetComponent<ArrowKey>().direction;
            Debug.Log(direction);

            if (direction == "Up")
            {
                while (!Input.GetKeyDown(KeyCode.UpArrow))
                {
                    yield return null;
                }
            }
            else if (direction == "Down")
            {
                while (!Input.GetKeyDown(KeyCode.DownArrow))
                {
                    yield return null;
                }
            }
            else if (direction == "Left")
            {
                while (!Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    yield return null;
                }
            }
            else if (direction == "Right")
            {
                while (!Input.GetKeyDown(KeyCode.RightArrow))
                {
                    yield return null;
                }
            }

            // need to wait or else Input.GetKeyDown will be true multiple times in the same frame
            yield return new WaitForSeconds(Time.deltaTime);
            Destroy(arrowKey);
        }
    }

    public void GameOver()
    {

    }

}

   
