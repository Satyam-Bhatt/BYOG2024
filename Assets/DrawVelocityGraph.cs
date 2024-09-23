using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawVelocityGraph : MonoBehaviour
{
    public Transform[] velocityPoints;
    //public string velocityText;

    CustomExpressionEvaluator evaluator;

    // Start is called before the first frame update
    void Start()
    {
        evaluator = new CustomExpressionEvaluator();
    }

    // Update is called once per frame
    void Update()
    {
        float tinc = 6.0f / velocityPoints.Length;
        float t = 0;
        for (int i = 0; i < velocityPoints.Length; i++)
        {
            float v_Value = LevelManger.Instance.YScale_velocityGraph * evaluator.EvaluateExpression(TextManager_Velocity.Instance.velocityText, t);
            if (!float.IsNaN(v_Value))
            {
                velocityPoints[i].localPosition = new Vector3(t, v_Value, 0);
                TextManager_Velocity.Instance.exclamationMark_Vel.SetActive(false);
            }
            else
            {
                velocityPoints[i].localPosition = new Vector3(0, 0, 0);
                TextManager_Velocity.Instance.exclamationMark_Vel.SetActive(true);
                Debug.LogError("Failed to evaluate expression");
            }
            t += tinc;
        }
    }
}
