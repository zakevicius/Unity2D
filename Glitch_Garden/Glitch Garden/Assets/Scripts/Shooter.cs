using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject zucchini, gun;

    public void Fire()
    {
        
        Instantiate(zucchini, gun.transform.position, Quaternion.identity);
    }
}
