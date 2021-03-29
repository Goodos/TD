using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class GameController : MonoBehaviour
{
    [SerializeField] RectTransform gameOverPanel;
    [SerializeField] Tower tower;
    [SerializeField] Spawner[] spawners;

    public int currWave = 1;
    public int enemyCounter = 0;
    public bool endWaveFlag = true;

    public UnityAction endWave;
    public UnityAction nextWave;

    void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
        nextWave += NextWave;
        enemyCounter = 0;
        NextWave();
    }

    void Update()
    {
        if (enemyCounter == 0 && endWaveFlag)
        {
            currWave++;
            endWave.Invoke();
            endWaveFlag = false;
        }
        if (tower.GetTowerHp() <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.gameObject.SetActive(true);
        }
    }

    void SpawnNextWave(int nextWave)
    {
        foreach (Spawner _spawner in spawners)
        {
            _spawner.StartSpawn(nextWave);
        }
    }

    void NextWave()
    {
        endWaveFlag = true;
        SpawnNextWave(currWave);
    }
}
