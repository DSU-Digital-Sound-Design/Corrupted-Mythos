using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godWipe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Destroy(collision);
        }
    }
}
