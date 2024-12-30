using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainerPrefab;
    /* [SerializeField] private GameObject _trippleShotPowerUpPrefab;
     [SerializeField] private GameObject _speedPowerUpPrefab;*/

    [SerializeField] private GameObject[] _powerUps;

    private bool _stopSpawing = false;
   
    void Start()
    {
        StartCoroutine("SpawnEnemyRoutine");
        //or
        //StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerUpsRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        //yield return null; // this wait foe 1 frame.
        while (_stopSpawing == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainerPrefab.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpsRoutine()
    {
        while (_stopSpawing == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUps = Random.Range(0, 2);
            Instantiate(_powerUps[randomPowerUps], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4.0f,10.0f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawing = true;
        Destroy(gameObject);
    }
}
