﻿using System.Collections;
using System.Collections.Generic;
﻿using System;
using UnityEngine;

public class BallMovement1 : MonoBehaviour {

    public float speed = 5f;
	public float tolerance = 0f;

    public float predictionRange = 1f;


    public float spawnVariance = 3;
    Vector3 dirV3;
    Vector2 dir;
    Rigidbody rb;
    Vector3 storedVelocity;

	public float leftBound = -12;
	public float rightBound = 12;
    public Transform player1;
    public Transform player2;

    public bool RandoRot = false;


	float interactionTimerL = 0.05f;
	float iTimer = 0;

    public Action<int> OnBallHit;

    public static Action<int> ballHit;

    public float NudgeForce;

    public Action<int> OnScore;

    public static Action<int> score;


    public ScoreController sc;
    bool isPaused = false;

    public Color defaultColor;
   public Material myMaterial;
    TrailRenderer tr;

	// Use this for initialization
	void Awake () {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        storedVelocity = Vector3.zero;
        Spawn();
       
      

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > rightBound)
        {
			try
			{
				ScoreKeeper.Instance.ScoreME(2, 1);
			}
			catch { }
            try
            {
                score(1);

                sc.UpdateScore(transform.position.x);

            }
            catch { }
            Spawn();
        }
		else if(transform.position.x < leftBound)
		{
			try
			{
				ScoreKeeper.Instance.ScoreME(1, 1);
			}
			catch { }
            try
            {
                score(0);

                sc.UpdateScore(transform.position.x);

            }
            catch { }
			Spawn();
		}

        if (Input.GetKeyDown(KeyCode.J)) {
            NudgeInDirection(Vector3.left, NudgeForce);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            NudgeInDirection(Vector3.right, NudgeForce);
        }

        if (iTimer > 0)
		{
			iTimer -= Time.deltaTime;
		}
	}
    private void FixedUpdate()
    {
        checkAhead();
    }

    private void LateUpdate()
	{
        //if(!isPaused)
        {
            if (rb.velocity.magnitude > speed - tolerance)
            {
                rb.velocity = rb.velocity.normalized * speed;
            }
            if (rb.velocity.magnitude < speed + tolerance)
            {
                rb.velocity = rb.velocity.normalized * speed;
            }
        }
	}

	public void Spawn() {
        myMaterial.color = defaultColor;
        transform.position = Vector3.zero + Vector3.up *UnityEngine.Random.Range(-spawnVariance, spawnVariance) ;
      
        dir = UnityEngine.Random.insideUnitCircle;
  
       
        dirV3 = new Vector3(dir.x, dir.y);
        rb.velocity = dirV3 * speed;
    }
    void checkAhead() {
        RaycastHit rayHit;
        Vector3 rayOrigin = this.transform.position;
        Vector3 ray = rb.velocity * predictionRange;
        if (Physics.Raycast(rayOrigin, ray, out rayHit, ray.magnitude))
        {
            if (iTimer <= 0)
            {
                if (rayHit.transform.gameObject.tag == "Paddle")
                {
                    myMaterial.color = rayHit.transform.gameObject.GetComponent<Renderer>().material.color;

                    Vector3 paddle = rayHit.transform.position;
                    Vector3 angle = paddle - transform.position;
                    float mag = Vector3.Magnitude(angle);

                    Vector3 reflection = Vector3.Reflect(rb.velocity, rayHit.normal);

                    //rb.velocity = -angle * speed;
                    //speed = (1f / mag) * 5;

                    rb.velocity = (-angle + reflection + rb.velocity.magnitude * rayHit.normal * -1f) * speed;
                    float myYpos = transform.position.y;

                    float paddleYpos = paddle.y;

                    int higherLower = 0;

                    if (paddleYpos > (myYpos + .5f))

                    {

                        //ball is lower

                        higherLower = 1;

                        // print(higherLower);

                    }

                    else if (paddleYpos < (myYpos - .05f))

                    {

                        //ball is higher

                        higherLower = 2;

                    }

                    // print("Paddle ypos: " + paddle.y + " Ball ypos : " + myYpos);

                    ballHit(higherLower);

                
            }
                else if (rayHit.transform.gameObject.tag == "Boundary")
                {
                    if (rb.velocity.x > 0)
                    {
                        rb.velocity = rb.velocity.normalized * 0.9f + Vector3.right * 0.1f;


                    }
                    else if (rb.velocity.x < 0)
                    {
                        rb.velocity = rb.velocity.normalized * 0.9f + Vector3.left * 0.1f;

                    }
                    ballHit(3);

                }
                iTimer = interactionTimerL;

                if (RandoRot)
                {
                    rb.maxAngularVelocity = 45;
                    rb.angularVelocity = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(-45, 45);
                }
            }
        }

     }


    public void pause() {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            storedVelocity = this.GetComponent<Rigidbody>().velocity;
            rb.velocity = Vector3.zero;
        }
        else {
            rb.velocity = storedVelocity;
        }
    }
    public void NudgeInDirection(Vector3 dir, float mag) {
        rb.AddForce(dir * mag);
    }
}
