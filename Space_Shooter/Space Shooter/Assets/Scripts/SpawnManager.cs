using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyShipPrefab;
    [SerializeField] private GameObject[] _powerups;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-8f, 8f), 6f, 0), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-8f, 8f), 6f, 0), Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }
    }
}
