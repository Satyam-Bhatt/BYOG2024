using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CoinCollectScript : MonoBehaviour
{
    [SerializeField] private int coinValue = 0;
    [SerializeField] private TMP_Text coinText;

    private void Start()
    {
        coinText.text = coinValue.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            coinValue--;
            if (coinValue <= 0)
            {
                LevelManger.Instance.CoinCheck();
                Destroy(this.gameObject);
            }

            if (coinValue > 0)
            { 
                coinText.text = coinValue.ToString();
            }
        }
    }
}
