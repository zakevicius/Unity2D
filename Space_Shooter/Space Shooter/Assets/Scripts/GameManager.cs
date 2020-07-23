using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void GameOver()
    {
        gameOver = true;
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                gameOver = false;
                Instantiate(player, Vector3.zero, Quaternion.identity);
                _uiManager.HideTitleScreen();
            }
        }
    }
}
