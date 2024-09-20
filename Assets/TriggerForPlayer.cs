using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.RestartScene();
        }
    }
}
