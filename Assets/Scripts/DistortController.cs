using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortController : MonoBehaviour {

    public Material[] playerDistorts;
    float[] playerDistortionIntensity;
    public float distortScalar=.002f;

	// Use this for initialization
	void Start () {
        playerDistortionIntensity = new float[2];
        playerDistortionIntensity[0] = 0;
        playerDistortionIntensity[1] = 0;
        resetDistortion();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   public void resetDistortion() {
        playerDistorts[0].SetVector("_IntensityAndScrolling", new Vector4(0, 0, .2f, 1f));
        playerDistorts[1].SetVector("_IntensityAndScrolling", new Vector4(0, 0, .2f, 1f));
    }
   public void UpdateDistortion(int leader,int lead) {
        //print("Checked");
        switch (leader) {
            case 0:
                playerDistortionIntensity[0] = lead * distortScalar;
                playerDistortionIntensity[1] = 0;
                break;
            case 1:
                playerDistortionIntensity[1] = lead * distortScalar;
                playerDistortionIntensity[0] = 0;
                break;
            default:
                playerDistortionIntensity[0] = 0;
                playerDistortionIntensity[1] = 0;
                break;

        }
        playerDistorts[0].SetVector("_IntensityAndScrolling", new Vector4(playerDistortionIntensity[0],playerDistortionIntensity[0],.2f,1f));
        playerDistorts[1].SetVector("_IntensityAndScrolling", new Vector4(playerDistortionIntensity[1], playerDistortionIntensity[1], .2f, 1f));
    }
}
