using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool isSpeedBoosted = false;
    public bool isShieldActive = false;
    public int playerHealth = 3;

    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _tripleShot;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _shield;
    [SerializeField] private float _initialSpeed = 5.0f;
    [SerializeField] private float _speedBoost = 2.0f;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private float _powerupTimer = 5.0f;
    [SerializeField] private GameObject[] engines;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private float _nextFire = 0.0f;
    private Vector3 _laserPos = new Vector3(0, 1f, 0);
    private AudioSource _audioSource;
    private int hitCount = 0;

       
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_uiManager) {
            _uiManager.UpdateLives(playerHealth);
        }

        _spawnManager.StartSpawnRoutines();
        _audioSource = GetComponent<AudioSource>();

        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {        
        if (Time.time > _nextFire)
        {
            _audioSource.Play();

            if (canTripleShot)
            {
                Instantiate(_tripleShot, transform.position, Quaternion.identity);
            } 
            else
            {
                Instantiate(_laser, transform.position + _laserPos, Quaternion.identity);
            }

            _nextFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float speed = _initialSpeed;

        if (isSpeedBoosted)
        {
            speed = _initialSpeed * _speedBoost;
        }


        // transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * speed * horizontalInput);
        // transform.Translate(new Vector3(0, 1f, 0) * Time.deltaTime * speed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        else if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
    }

    public void getDamage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            _shield.SetActive(false);
            return;
        } 
        else
        {
            playerHealth--;
            hitCount++;

            if (hitCount == 1)
            {
                engines[0].SetActive(true);
            }

            if (hitCount == 2)
            {
                engines[1].SetActive(true);
            }
        }        

        if (_uiManager)
        {
            _uiManager.UpdateLives(playerHealth);
        }

        if (playerHealth < 1)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(gameObject);
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotTimerRoutine());
    }

    public IEnumerator TripleShotTimerRoutine()
    {
        yield return new WaitForSeconds(_powerupTimer);
        canTripleShot = false;
    }

    public void SpeedPowerupOn()
    {
        isSpeedBoosted = true;
        StartCoroutine(SpeedPowerupTimer());
    }

    public IEnumerator SpeedPowerupTimer()
    {
        yield return new WaitForSeconds(_powerupTimer);
        isSpeedBoosted = false;
    }

    public void ShieldPowerUp()
    {
        isShieldActive = true;
        _shield.SetActive(true);
    }
}
