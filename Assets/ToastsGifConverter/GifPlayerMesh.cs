using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifPlayerMesh : GifPlayerInterface
{
    private MeshRenderer meshy;
    public GifData _g;
    GifData lastPlayed;
    Coroutine lastCoroutine;
    // Use this for initialization
    void Awake()
    {
        meshy = GetComponent<MeshRenderer>();
    }
    /*
    public Coroutine Init(GifData gif)
    {
       Coroutine lc;
       playing = true;
       lc= StartCoroutine(play(gif));
       return lc;
    }
  


    private void Start()
    {
         Init(_g);   
    }

    public void ChangeGif(GifData gd) {
        playing = false;
        StopCoroutine(lastCoroutine);
        lastCoroutine = Init(gd);
    }
      */

    public override void Initialize(GifData gif)

    {

        playing = true;
        StopAllCoroutines();
        StartCoroutine(Play(gif));


    }

    private void Start()

    {

        Initialize(_g);

    }



    public void ChangeGif(GifData gd)
    {
        lastPlayed = _g;
        playing = false;

        //StopCoroutine(play(_g));
        if (lastPlayed.name != gd.name)
        {
            Initialize(gd);
        }

    }



    IEnumerator Play(GifData gif)
    {
        _g = gif;
        while (playing)
        {
            for (int j = 0; j < _g.frames.Length; j++)
            {
                meshy.material.mainTexture = _g.frames[j];
                yield return new WaitForSeconds(_g.delay);
            }
        }
        playing = false;
    }
}
