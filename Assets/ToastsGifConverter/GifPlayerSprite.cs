using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifPlayerSprite : GifPlayerInterface
{
    private SpriteRenderer spritey;

    // Use this for initialization
    void Awake()
    {
        spritey = GetComponent<SpriteRenderer>();
    }

    public override void Initialize(GifData gif)
    {
        playing = true;
        StartCoroutine(play(gif));
    }

    IEnumerator play(GifData gif)
    {
        while (playing)
        {
            for (int j = 0; j < gif.frames.Length; j++)
            {
                spritey.sprite = Sprite.Create(gif.frames[j],new Rect(0,0,gif.frames[j].width, gif.frames[j].height),new Vector2(0.5f,0.5f));
                yield return new WaitForSeconds(gif.delay);
            }
        }
        playing = false;
    }
}

