using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryController : MonoBehaviour
{
    public Transform start;
    public Transform end;

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

        if (TextManager.Instance.play)
        {
            valueOfX += Time.deltaTime * LevelManger.Instance.speed;

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
        if (Vector2.Distance(start.localPosition, end.localPosition) < 0.1f)
        {
            LevelManger.Instance.CoinCheck();
            if (LevelManger.Instance.allCoinCollected)
            { 
                GameManager.Instance.winPanel.SetActive(true);
                TextManager.Instance.winPanel.SetActive(true);
                TextManager.Instance.play = false;
            }
            Debug.Log("WIN");
        }

    }
}
