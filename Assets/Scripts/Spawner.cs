using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameController gc;

    enum enemyType
    {
        ghoul,
        pudge
    }

    public void StartSpawn(int waveNum)
    {
        StartCoroutine(SpawnRoutine(waveNum));
    }

    IEnumerator SpawnRoutine(int waveNumber)
    {
        var enemyCount = GetEnemyCount(waveNumber);

        for (int i = 1; i <= enemyCount.gnoul; i++)
        {
            SpawnEnemy(enemyType.ghoul);
            yield return new WaitForSeconds(1f);
        }
        if (enemyCount.pudge != 0)
        {
            for (int i = 1; i <= enemyCount.pudge; i++)
            {
                SpawnEnemy(enemyType.pudge);
                yield return new WaitForSeconds(1f);
            }
        }
    }

    (int gnoul, int pudge) GetEnemyCount(int waveNumber)
    {
        switch (waveNumber)
        {
            case 1:
                return (1, 0);
            case 2:
                return (2, 0);
            case 3:
                return (2, 1);
            case 4:
                return (3, 1);
            case 5:
                return (3, 2);
            default:
                return (4, 2);//ES> доделать формулу
        }
    }

    void SpawnEnemy(enemyType type)
    {
        gc.enemyCounter++;
        if (type == enemyType.ghoul)
        {
            GameObject newEnemy = Instantiate(enemies[0], null);
            newEnemy.transform.position = gameObject.transform.position;
            newEnemy.GetComponent<Enemy>().SetParameters(8, 1, Random.Range(12, 15));//13
        }
        else
        {
            GameObject newEnemy = Instantiate(enemies[1], null);
            newEnemy.transform.position = gameObject.transform.position;
            newEnemy.GetComponent<Enemy>().SetParameters(17, 4, Random.Range(6, 9));//7
        }
    }
}