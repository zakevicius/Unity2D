using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int powerupID; // 0 = triple shot, 1 = speed boost, 2 = shields
    [SerializeField] AudioClip _audioClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);      
        
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotPowerupOn();
                        break;
                    case 1:
                        player.SpeedPowerupOn();
                        break;
                    case 2:
                        player.ShieldPowerUp();
                        break;
                    default:
                        break;
                }

                AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
                Destroy(this.gameObject);
            }
        }
    }
}
