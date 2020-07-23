using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int enemyHealth = 1;

    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _explosion;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager)
        {
            _uiManager.UpdateScore(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -6)
        {
            float randomXPosition = Random.Range(-8, 8);
            transform.position = new Vector3(randomXPosition, 6.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                Player p = other.GetComponent<Player>();
                if (p) p.getDamage();
                Die();
                break;
            case "Laser":
                Destroy(other.gameObject);
                Die();
                break;
            default:
                break;
        }
    }

    private void Die()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        _uiManager.UpdateScore(100);
        Destroy(gameObject);
    }
}
