using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    private int waveNumber = 1;
    private SpawnManager spawnManager;
    private int arrowCount;
    private List<GameObject> arrowKeyList;
    public TextMeshProUGUI timerText;
    private bool isGameActive;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            float seconds = Time.time;
            timerText.text = "Time: " + (int)(seconds * 10f) / 10f;

            arrowCount = FindObjectsOfType<ArrowKey>().Length;
            if (arrowCount == 0 && waveNumber <= 10)
            {
                arrowKeyList = spawnManager.SpawnWave(waveNumber);
                StartCoroutine("PlayWave", arrowKeyList);

            }

            else if (waveNumber > 10)
            {
                GameOver();
            }
        }
        
    }
  
   IEnumerator PlayWave(List<GameObject> arrowKeyList) 
    {
        print(waveNumber);

        for (int i = 0; i < arrowKeyList.Count; i++)
        {
            bool flag = false;
            GameObject arrowKey = arrowKeyList[i];
            string direction = arrowKey.GetComponent<ArrowKey>().direction;
            print(direction);

            if (direction == "Up")
            {
                while (!Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        foreach (GameObject hiddenArrowKey in arrowKeyList)
                        {
                            hiddenArrowKey.SetActive(true);
                        }
                        i = -1;
                        flag = true;
                        break;
                    }
                    yield return null;
                }
            }
            else if (direction == "Down")
            {
                while (!Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        foreach (GameObject hiddenArrowKey in arrowKeyList)
                        {
                            hiddenArrowKey.SetActive(true);
                        }
                        i = -1;
                        flag = true;
                        break;
                    }
                    yield return null;
                }
            }
            else if (direction == "Left")
            {
                while (!Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        foreach (GameObject hiddenArrowKey in arrowKeyList)
                        {
                            hiddenArrowKey.SetActive(true);
                        }
                        i = -1;
                        flag = true;
                        break;
                    }
                    yield return null;
                }
            }
            else if (direction == "Right")
            {
                while (!Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        foreach (GameObject hiddenArrowKey in arrowKeyList)
                        {
                            hiddenArrowKey.SetActive(true);
                        }
                        i = -1;
                        flag = true;
                        break;
                    }
                    yield return null;
                }
            }

            // need to wait or else Input.GetKeyDown will be true multiple times in the same frame
            yield return new WaitForSeconds(Time.deltaTime);
            
            // set arrowKey as inactive instead of deleting in case we need to reset wave
            if (flag == false)
            {
                arrowKey.SetActive(false);
            }
            
        }

        waveNumber++;

        // delete all arrowKeys at the end of the wave
        foreach (GameObject arrowKey in arrowKeyList)
        {
            Destroy(arrowKey);
        }

    }

    public void GameOver()
    {
        score = Time.deltaTime;
        isGameActive = false;
        
    }

    void ResetWave(List<GameObject> arrowKeyList)
    {

    }
}

   
