using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lerpwithcurve : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    public Transform[] bPoints;

    public bool bezier = false;

    public GameObject[] toInstance;

    [Range(0, 1)]
    public float t = 0;

    [Space(10)]
    [Header("Hermite Variables")]
    public float a, b = 0;

    private float timer = 0f;

    private bool start = false;

    private void Start()
    {
        transform.position = p1.position;
    }

    private void Update()
    {
        //Manipulating time with button press
        if (Input.GetKey(KeyCode.L))
        {
            start = true;
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            start = false;
        }

        

        //Manipulation Speed

        //Level 1
        //t = timer;

        //Level 2
        //t = Mathf.Clamp01(timer);

        //Level 3
        //t = Mathf.Cos(timer);

        //Level 4
        //t = Mathf.Cos(timer + Mathf.PI);

        //Level 5 
        //t = Mathf.Cos(timer + Mathf.PI) * 0.5f + 0.5f;

        //Level 6
        //t = timer * timer;

        //Level 7
        //t = Mathf.Clamp01(timer * timer);

        // Level Final - With Curve shown
        t = Mathf.Clamp01(timer);
        //t = Mathf.Cos(timer + Mathf.PI) * 0.5f + 0.5f; <---------- Use this to occilate
        t = (a + b - 2) * t * t * t + (-a - 2 * b + 3) * t * t + b * t;

        //Manipulation Trajectory

        Debug.Log(t + "-----" + timer);

        Vector2 p12 = Vector2.zero;

        if (!bezier)
        { 
            p12 = Vector2.LerpUnclamped(p1.position, p2.position, t);
        }
        else
        {
            Vector2 AB = Vector2.Lerp(bPoints[0].position, bPoints[1].position, t);
            Vector2 BC = Vector2.Lerp(bPoints[1].position, bPoints[2].position, t);
            Vector2 CD = Vector2.Lerp(bPoints[2].position, bPoints[3].position, t);

            Vector2 AB_BC = Vector2.Lerp(AB, BC, t);
            Vector2 BC_CD = Vector2.Lerp(BC, CD, t);
            p12 = Vector2.Lerp(AB_BC, BC_CD, t);

            int detail = 10;
            for (int i = 0; i <= detail; i++)
            {
                t = i / (float)detail;
                //Debug.Log(t);

                Vector3 l1 = Vector3.Lerp(bPoints[0].localPosition, bPoints[1].localPosition, t);
                Vector3 l2 = Vector3.Lerp(bPoints[1].localPosition, bPoints[2].localPosition, t);
                Vector3 l3 = Vector3.Lerp(bPoints[2].localPosition, bPoints[3].localPosition, t);

                Vector3 l12 = Vector3.Lerp(l1, l2, t);
                Vector3 l23 = Vector3.Lerp(l2, l3, t);

                Vector3 l123 = Vector3.Lerp(l12, l23, t);

                toInstance[i].transform.position = l123;
            }


        }

        if (start)
        {
            transform.position = p12;
        }
    }
}
