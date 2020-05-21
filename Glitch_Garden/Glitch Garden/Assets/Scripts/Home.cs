using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        FindObjectOfType<Lives>().TakeLife();
        Destroy(otherObject.gameObject);
    }
}
