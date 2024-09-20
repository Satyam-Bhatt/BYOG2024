using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VelocityController : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public Transform spike;
    public Transform Graph;

    float valueOfX = 0;
    float valueOfY = 0;
    float SumTime = 0;
    

    public string velocityEquation = "5*x";

    CustomExpressionEvaluator evaluator;

    public Transform[] drawingCircles;
    public Transform[] VelocityCircles;

    private void Start()
    {
        evaluator = new CustomExpressionEvaluator();
    }

    private void Update()
    {

        //DRAWING TRAJECTORY
        float detail = 260;
        for (int i = 0; i < detail; i++)
        {
            float placer = (i / detail) * 12;
            if (i >= drawingCircles.Length - 1)
            {
                break;
            }
            float y_Value = evaluator.EvaluateExpression(TextManager.Instance.combinedText, placer);
            if (!float.IsNaN(y_Value))
            {
                drawingCircles[i].localPosition = new Vector3(placer, y_Value, 0);
                TextManager.Instance.exclamationMark.SetActive(false);
                //Debug.Log("Result: " + valueOfY);
            }
            else
            {
                drawingCircles[i].localPosition = new Vector3(0, 0, 0);
                TextManager.Instance.exclamationMark.SetActive(true);
                Debug.LogError("Failed to evaluate expression");
            }
        }

        //DRAWING VELOCITY
        float tinc=15.0f/VelocityCircles.Length;
        float Yscale = 1.0f;
        float t = 0;
        for (int i = 0; i < VelocityCircles.Length; i++)
        {
            float v_Value = Yscale* evaluator.EvaluateExpression(velocityEquation, t);
            if (!float.IsNaN(v_Value))
            {
                VelocityCircles[i].localPosition = new Vector3(t, v_Value, 0);
            }
            else
            {
                VelocityCircles[i].localPosition = new Vector3(0, 0, 0);
                Debug.LogError("Failed to evaluate expression");
            }
            t+=tinc;
        }

        if (TextManager.Instance.play)
        {
            SumTime += Time.deltaTime;

            /*f (velocityEquation == "")
            {
                valueOfX = SumTime;
            }
            else
            {
                }*/

            valueOfX += Time.deltaTime * evaluator.EvaluateExpression(velocityEquation, SumTime);


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
                SumTime = 0;
                valueOfX = 0;
            }
        }
        //Debug.Log(valueOfY);
        //valueOfY = func; //valueOfX % 2; //Mathf.Sin(valueOfX);

        //Distance Check Between 2 Objects --------------WIN CONDITION--------------
        if (Vector2.Distance(start.localPosition, end.localPosition) < 0.05f)
        {
            if (LevelManger.Instance.allCoinCollected)
            {
                GameManager.Instance.winPanel.SetActive(true);
                TextManager.Instance.play = false;
            }
            Debug.Log("WIN");
        }
        if (spike != null && Vector2.Distance(start.localPosition, spike.localPosition) < 0.01f)
        {
            Debug.Log("DEAD");
        }

    }
}
