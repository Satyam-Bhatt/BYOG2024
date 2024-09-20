using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    public Transform start;
    public Transform end;

    float valueOfX = 0;
    float valueOfY = 0;

    CustomExpressionEvaluator evaluator;

    private void Start()
    {
        evaluator = new CustomExpressionEvaluator();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        { 
            valueOfX += Time.deltaTime;
            //Debug.Log(TextManager.Instance.combinedText);
            valueOfY = evaluator.EvaluateExpression(TextManager.Instance.combinedText, valueOfX);
        }
        //Debug.Log(valueOfY);
        //valueOfY = func; //valueOfX % 2; //Mathf.Sin(valueOfX);


        start.localPosition = new Vector3(valueOfX, valueOfY, 0);

    }
}
