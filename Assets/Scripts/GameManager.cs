using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{

    private int waveNumber = 1;
    private SpawnManager spawnManager;
    private int arrowCount;
    private List<GameObject> arrowKeyList;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI bestTimeText;
    private bool isGameActive;
    private bool misclick;
    public Button playAgainButton;
    private float time;
    private float bestTime = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        isGameActive = true;
        LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            time = Time.timeSinceLevelLoad;
            timerText.text = "Time: " + (int)(time * 10f) / 10f;

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
        
    }
  
   IEnumerator PlayWave(List<GameObject> arrowKeyList) 
    {
        waveText.text = "Wave: " + waveNumber;

        for (int i = 0; i < arrowKeyList.Count; i++)
        {
            misclick = false;
            GameObject arrowKey = arrowKeyList[i];
            string direction = arrowKey.GetComponent<ArrowKey>().direction;

            if (direction == "Up")
            {
                while (!Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        ResetWave(arrowKeyList);
                        i = -1;
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
                        ResetWave(arrowKeyList);
                        i = -1;
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
                        ResetWave(arrowKeyList);
                        i = -1;
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
                        ResetWave(arrowKeyList);
                        i = -1;
                        break;
                    }
                    yield return null;
                }
            }

            // need to wait or else Input.GetKeyDown will be true multiple times in the same frame
            yield return null;

            // set arrowKey as inactive instead of deleting in case we need to reset wave
            if (misclick == false)
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

        isGameActive = false;
        playAgainButton.gameObject.SetActive(true);

        if (time < bestTime)
        {
            SaveScore();
            bestTime = time;
        }

        bestTimeText.text = "Best Time: " + (int)(bestTime * 10f) / 10f;
        bestTimeText.gameObject.SetActive(true);
        
    }

    void ResetWave(List<GameObject> arrowKeyList)
    {
        foreach (GameObject hiddenArrowKey in arrowKeyList)
        {
            hiddenArrowKey.SetActive(true);
        }
        misclick = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [System.Serializable]
    class SaveData
    {
        public float time;
    }

    private void SaveScore()
    {
        SaveData data = new SaveData();
        data.time = time;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestTime = data.time;
        }

        
    }


}

   
