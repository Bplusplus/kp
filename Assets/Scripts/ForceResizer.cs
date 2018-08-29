using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class ForceResizer : MonoBehaviour {

    Canvas canvas;
    public List<Resizable> RS = new List<Resizable>();

    int rezX = 0;
    int rezY = 0;
	
	void Start () {
        canvas = GetComponent<Canvas>();

        foreach (Resizable r in RS)
        {
            if (r.UsingRect)
            {
                r.nominalScale = r.go.GetComponent<RectTransform>().localScale;
            }
            else
            {
                r.nominalScale = r.go.transform.localScale;
            }
        }

        
    }

    private void Update()
    {
        if(rezX != Screen.width || rezY != Screen.height)
        {
            rezX = Screen.width;
            rezY = Screen.height;
            RezUpdate();
        }
    }



    void RezUpdate () {
        int h = Screen.height;
        int w = Screen.width;

		foreach(Resizable r in RS)
        {
            float newH = 1;
            if (r.UsingZ) {
                newH = r.nominalScale.z * h * r.Scalar.z / 1000f;
            }
            else
            {
                newH = r.nominalScale.y * h * r.Scalar.y / 1000f;
            }
            float newW = r.nominalScale.x * w * r.Scalar.x / 1000f;

            if (r.UsingRect)
            {
                if (r.UsingZ)
                {
                    r.go.GetComponent<RectTransform>().localScale = new Vector3(newW, r.nominalScale.y, newH);
                }
                else
                {
                    r.go.GetComponent<RectTransform>().localScale = new Vector3(newW, newH, r.nominalScale.z);
                }
            }
            else
            {
                if (r.UsingZ)
                {
                    r.go.transform.localScale = new Vector3(newW, r.nominalScale.y, newH);
                }
                else
                {
                    r.go.transform.localScale = new Vector3(newW, newH, r.nominalScale.z);
                }
            }



            Debug.Log(r.go.name);
        }
	}
}

[System.Serializable]
public class Resizable
{
    public GameObject go;
    public Vector3 nominalScale;
    public Vector3 Scalar = Vector3.one;
    public bool UsingZ;

    public bool UsingRect;

    public Resizable(GameObject Go, Vector3 nominal, Vector3 scalar, bool usingZ)
    {
        go = Go;
        nominalScale = nominal;
        Scalar = scalar;
        UsingZ = usingZ;
    }
}
