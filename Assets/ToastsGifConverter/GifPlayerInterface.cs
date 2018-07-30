using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifPlayerInterface : MonoBehaviour {

    public bool playing;
    
    public virtual void Initialize(GifData gif)
    {
        Debug.Log("Don't use the interface in your scene! Please use an instance (GifPlayerMesh/GifPlayerUI)!!");
        return;
    }

}
