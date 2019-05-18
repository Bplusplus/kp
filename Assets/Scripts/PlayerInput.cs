using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

   
    public float speed = 10f;
    public bool isPlayerOne =true;
    public float yMax = 4;
    public float yMin = -4;

    public string Axis1 = "Fire1";
    public string Axis2 = "Fire2";

    public serialMicrobitInput Microbit = null;

    public bool useButtons = true;

    void Update() {

        if (useButtons)
        {
            float input2 = 0f;

            if (Input.GetAxis(Axis1) > 0)
            {
                input2 = 1;
                Debug.Log(Input.GetAxis(Axis1).ToString());
            }
            else if (Input.GetAxis(Axis2) > 0)
            {
                input2 = -1;
                Debug.Log(Input.GetAxis(Axis2).ToString());
            }

            Vector3 Pos2 = gameObject.transform.position;
            Pos2.y += speed * input2;
            Pos2.y = Mathf.Clamp(Pos2.y, yMin, yMax);
            gameObject.transform.position = Pos2;
        }
        else
        {

            if (Microbit == null)
            {

                float input = 0f;

                if (isPlayerOne)
                {
                    input = Input.GetAxisRaw("Vertical");
                }
                else
                {
                    input = Input.GetAxisRaw("Vertical2");
                }

                Vector3 Pos = gameObject.transform.position;
                Pos.y += speed * Time.deltaTime * input;
                Pos.y = Mathf.Clamp(Pos.y, yMin, yMax);
                gameObject.transform.position = Pos;



            }
            else
            {
                Vector3 Pos = gameObject.transform.position;
                Pos.y = Microbit.outputF * yMax;
                gameObject.transform.position = Pos;
            }
        }
        

    }
}
