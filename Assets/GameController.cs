using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class GameController : MonoBehaviour
{
    [SerializeField] Text towerHpText;
    [SerializeField] Text currWaveText;
    [SerializeField] Text enemiesToNextWave;

    [SerializeField] Tower tower;
    [SerializeField] Spawner[] spawners;

    public int currWave = 1;
    public int enemyCounter = 0;
    public bool endWaveFlag = true;

    public UnityAction endWave;
    public UnityAction nextWave;

    void Start()
    {
        nextWave += NextWave;
        enemyCounter = 0;
        NextWave();
    }

    void Update()
    {
        //Debug.Log(enemyCounter);
        if (enemyCounter == 0 && endWaveFlag)
        {
            currWave++;
            endWave.Invoke();
            endWaveFlag = false;
        }
    }

    private void OnGUI()
    {
        towerHpText.text = "Tower HP: " + tower.GetTowerHp().ToString();
        currWaveText.text = "Wave: " + currWave.ToString();
        enemiesToNextWave.text = "Enemies until the next wave: " + enemyCounter.ToString();
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
