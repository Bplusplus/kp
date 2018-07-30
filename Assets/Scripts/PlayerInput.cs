using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

   
    public float speed = 10f;
    public bool isPlayerOne =true;
    public float yMax = 4;
    public float yMin = -4;

	public serialMicrobitInput Microbit = null;

    void Update() {

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
