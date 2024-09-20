using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public Transform spike;

    float valueOfX = 0;
    float valueOfY = 0;

    CustomExpressionEvaluator evaluator;

    public Transform[] drawingCircles;

    private void Start()
    {
        evaluator = new CustomExpressionEvaluator();
    }

    private void Update()
    {
        for(int i=0; i < drawingCircles.Length; i++)
        {
            float y_Value = evaluator.EvaluateExpression(TextManager.Instance.combinedText, i);
            if (!float.IsNaN(y_Value))
            {
                drawingCircles[i].localPosition = new Vector3(i, y_Value, 0);
                //Debug.Log("Result: " + valueOfY);
            }
            else
            {
                drawingCircles[i].localPosition = new Vector3(0, 0, 0);
                Debug.LogError("Failed to evaluate expression");
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            valueOfX += Time.deltaTime;
            
            //Debug.Log(TextManager.Instance.combinedText);
            valueOfY = evaluator.EvaluateExpression(TextManager.Instance.combinedText, valueOfX);

            if (!float.IsNaN(valueOfY))
            {
                start.localPosition = new Vector3(valueOfX, valueOfY, 0);
                //Debug.Log("Result: " + valueOfY);
            }
            else
            {
                Debug.LogError("Failed to evaluate expression");
                valueOfX = 0;
            }
        }
        //Debug.Log(valueOfY);
        //valueOfY = func; //valueOfX % 2; //Mathf.Sin(valueOfX);

        //Distance Check Between 2 Objects --------------WIN CONDITION--------------
        if (Vector2.Distance(start.localPosition, end.localPosition) < 0.01f)
        {
            Debug.Log("WIN");
        }
        if (spike!=null && Vector2.Distance(start.localPosition, spike.localPosition) < 0.01f)
        {
            Debug.Log("DEAD");
        }

    }
}
