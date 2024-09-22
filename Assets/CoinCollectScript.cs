using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CoinCollectScript : MonoBehaviour
{
    [SerializeField] private int coinValue = 0;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private AudioClip coinSound;

    private void Start()
    {
        if (coinText != null)
        { 
            coinText.text = coinValue.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (TextManager.Instance != null)
            {
                TextManager.Instance.PlayAudio(coinSound);
            }
            else if (TextManager_Velocity.Instance != null)
            {
                TextManager_Velocity.Instance.PlayAudio(coinSound);
            }
            coinValue--;
            if (coinValue <= 0)
            {
                LevelManger.Instance.CoinCheck();
                Destroy(this.gameObject);
            }

            if (coinText != null && coinValue > 0)
            { 
                coinText.text = coinValue.ToString();
            }
        }
    }
}
