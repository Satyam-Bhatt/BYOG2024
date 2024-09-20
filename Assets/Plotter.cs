using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : MonoBehaviour
{
    public Transform[] Cot;
    public Vector3 start, end;
    public float cout=0.0f, cin=0.0f, aout=1.0f, ain=1.0f;
    public enum functions
    {
        k,
        cos,
        sin,
        quad,
        cube,
        mod,
        Total
    };
    public functions f1=0;
    float [] funcY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = (transform.position.x - transform.lossyScale.x/2);// 2); *10/transform.lossyScale.x;
        float y = transform.position.y;

        //float funcY [functions.Total];
        float xinc = transform.lossyScale.x/Cot.Length;

        if (f1 == functions.k)
        {
            for (int i = 0; i < Cot.Length; i++)
                {
                    Cot[i].transform.position=new Vector3 (x+xinc,y+cout,0);
                    x+= xinc;
                }
        }
        else if (f1 == functions.cos)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y+aout*Mathf.Cos(ain*(x + xinc)+cin)+cout, 0);
                x += xinc;
            }
        }
        else if (f1 == functions.sin)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y+aout*Mathf.Sin(ain*(x + xinc)+cin)+cout, 0);
                x += xinc;
            }
        }
        else if (f1 == functions.quad)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y + aout * ((x + xinc) + cin)* ( (x + xinc) + cin) + cout, 0);
                x += xinc;
            }
        }
        else if (f1 == functions.cube)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y + aout * ((x + xinc) + cin) * ((x + xinc) + cin)* ((x + xinc) + cin) + cout, 0);
                x += xinc;
            }
        }
        else if (f1 == functions.mod)
        {
            cout = 2.0f;
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y + 1+(aout * ((x + xinc) + cin))%cout, 0);
                x += xinc;
            }
        }
    }


   /* public float Plot(float x)
    {
        if (f1 == functions.k)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                y = y;
                x += xinc;
            }
        }
        else if (f1 == functions.cos)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y + aout * Mathf.Cos(ain * (x + xinc) + cin) + cout, 0);
                x += xinc;
            }
        }
        else if (f1 == functions.sin)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y + aout * Mathf.Sin(ain * (x + xinc) + cin) + cout, 0);
                x += xinc;
            }
        }
        else if (f1 == functions.quad)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y + aout * ((x + xinc) + cin) * ((x + xinc) + cin) + cout, 0);
                x += xinc;
            }
        }
        else if (f1 == functions.cube)
        {
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y + aout * ((x + xinc) + cin) * ((x + xinc) + cin) * ((x + xinc) + cin) + cout, 0);
                x += xinc;
            }
        }
        else if (f1 == functions.mod)
        {
            cout = 2.0f;
            for (int i = 0; i < Cot.Length; i++)
            {
                Cot[i].transform.position = new Vector3(x + xinc, y + 1 + (aout * ((x + xinc) + cin)) % cout, 0);
                x += xinc;
            }
        }
    }*/
}
