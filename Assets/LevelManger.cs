using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

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
    public float YScale_velocityGraph = 1f;
    public RaycastHit2D hit;

    public GameObject coordinatePlane;

    [SerializeField] private GameObject[] toDisable;
    [SerializeField] private GameObject[] toEnable;

    public string chapterName = "Chapter 1";
    public string levelName = "Level 1";

    [Space(10)]
    [Header("FOR TRAJECTORY LEVEL ONLY")]
    public string trajectory_OnlySolution = "";

    [Space(10)]
    [Header("FOR VELOCITY LEVEL ONLY")]
    public string trajSolution = "";
    public string velSolution = "";

    public void CoinCheck()
    {
        int num = FindObjectsOfType<CoinCollectScript>().Count();
        //Debug.Log(num);
        if(num <= 0)
        {
            allCoinCollected = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coordinatePlane = GameObject.Find("CoordinatePanel");

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            coordinatePlane.SetActive(true);
            coordinatePlane.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "X=" + hit.collider.gameObject.transform.localPosition.x.ToString() + " Y=" + hit.collider.gameObject.transform.localPosition.y.ToString();
            coordinatePlane.transform.position = new Vector3(hit.collider.gameObject.transform.position.x + 1f, hit.collider.gameObject.transform.position.y + 1f, 0);
        }
        else
        {
            coordinatePlane.SetActive(false);
        }
    }
}
