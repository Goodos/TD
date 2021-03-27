using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Text towerHpText;
    [SerializeField] Text currWaveText;
    [SerializeField] Text enemiesToNextWave;
    [SerializeField] Button nextWaveButton;


    [SerializeField] Tower tower;
    [SerializeField] Spawner[] spawners;

    public int currWave = 1;
    public int enemyCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        nextWaveButton.onClick.AddListener(NextWaveButton);
        NextWave(currWave);        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemyCounter);
        EndOfWave();        
    }

    private void OnGUI()
    {
        towerHpText.text = "HP: " + tower.GetTowerHp().ToString();
        currWaveText.text = "Wave: " + currWave.ToString();
        enemiesToNextWave.text = "Enemies until the next wave: " + enemyCounter.ToString();
    }

    void NextWave(int nextWave)
    {
        foreach (Spawner _spawner in spawners)
        {
            _spawner.StartSpawn(nextWave);
        }
    }

    void NextWaveButton()
    {
        currWave++;
        NextWave(currWave);
    }

    void EndOfWave()
    {
        if (enemyCounter == 0)
        {
            currWave++;
            NextWave(currWave);
        }
    }
}
