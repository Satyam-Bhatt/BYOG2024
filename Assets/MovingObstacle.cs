using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePath;

    [Range(0, 1f)]
    [SerializeField] private float t= 0.0f;
    [SerializeField] private float speed = 1.0f;

    private float increaseValue = 0.0f;

    private void Start()
    {
        transform.position = movePath[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (TextManager_Velocity.Instance.play == true)
        {
            increaseValue += Time.deltaTime * speed;

            t = Mathf.Cos(increaseValue + Mathf.PI) * 0.5f + 0.5f;

            transform.position = Vector3.Lerp(movePath[0].position, movePath[1].position, t);
        }

    }
}
