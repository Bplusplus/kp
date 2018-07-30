using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
    bool hasTriggered = false;
    public ParticleSystem ps;
	// Use this for initialization
	void Start () {
       
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P)) {
            ps.Emit(Random.Range(200,450));
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Ball") {
            hasTriggered = true;
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position =other.transform.position;
            ps.Emit(Random.Range(200, 450));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        hasTriggered = false;
    }
}
