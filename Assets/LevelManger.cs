using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    private static LevelManger _instance;
    public static LevelManger Instance
    {
        get
        {
            _instance = FindObjectOfType<LevelManger>();
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManger>();
            }

            return _instance;
        }
    }

    public bool allCoinCollected = false;
    public float speed = 1f;

    [SerializeField] private GameObject[] toDisable;
    [SerializeField] private GameObject[] toEnable;

    public void CoinCheck()
    {
        int num = FindObjectsOfType<CoinCollectScript>().Count();
        //Debug.Log(num);
        if(num-1 <= 0)
        {
            allCoinCollected = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in toDisable)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in toEnable)
        { 
            obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
