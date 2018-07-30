using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugTest : MonoBehaviour {
	public GifPlayerMesh gp;
	// Use this for initialization
	void Start () {
		if(gp==null)
			gp = this.GetComponent<GifPlayerMesh>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			gp.Initialize(gp._g);
		}
	}
}
