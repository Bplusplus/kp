using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifPlayerUI : GifPlayerInterface
{
    private RawImage img;
    public GifData current;

    // Use this for initialization
    void Awake()
    {
        img = GetComponent<RawImage>();
    }

    public override void Initialize(GifData gif)
    {
        playing = true;
        current = gif;
        StartCoroutine(play(gif));
    }

    IEnumerator play(GifData gif)
    {
        while (playing)
        {
            for (int j = 0; j < gif.frames.Length; j++)
            {
                img.texture = gif.frames[j];
                yield return new WaitForSeconds(gif.delay);
            }
        }
        playing = false;
    }
}

