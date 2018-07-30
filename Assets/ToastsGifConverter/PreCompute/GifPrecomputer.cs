#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GifPrecomputer : MonoBehaviour {

    public GameObject runtimeConverter;
    public string sourcePath;
    public string savePath;
    public string saveName;
    public GifPlayerInterface gifPlayer;
    public bool hasAlpha;
    [Range(1, 100)]
    public int quality;
    public GameObject done;
    
    void Start () {
        StartCoroutine(waitForLoadingIsDone());
    }

    IEnumerator waitForLoadingIsDone()
    {
        GifConverterRuntime c = Instantiate(runtimeConverter).GetComponent<GifConverterRuntime>();
        c.Init(sourcePath);
        while (!c.finished)
        {
            yield return new WaitForEndOfFrame();
        }
        GifData g = ScriptableObject.CreateInstance<GifData>();
        g.delay = c.gifData.delay;
        g.endurance = c.gifData.endurance;
        g.frames = new Texture2D[c.gifData.frames.Length];
        if (!AssetDatabase.IsValidFolder(savePath))
        {
            Debug.Log("Save folder does not exist! Please create the folder: " + savePath);
            yield break;
        }
        for (int i = 0; i < c.gifData.frames.Length; i++)
        {
            string p = savePath + "/" + saveName + i.ToString();
            if (hasAlpha) { p += ".png"; } else { p += ".jpg"; }
            if (hasAlpha)
            {
                System.IO.File.WriteAllBytes(p, c.gifData.frames[i].EncodeToPNG());
            }
            else
            {
                System.IO.File.WriteAllBytes(p, c.gifData.frames[i].EncodeToJPG(quality));
            }
        }
        AssetDatabase.Refresh();
        for (int i = 0; i < c.gifData.frames.Length; i++)
        {
            string p = savePath + "/" + saveName + i.ToString();
            if (hasAlpha) { p += ".png"; } else { p += ".jpg"; }
            ((TextureImporter)TextureImporter.GetAtPath(p)).textureType = TextureImporterType.Default;
        }
        AssetDatabase.SaveAssets();
        for (int i = 0; i < c.gifData.frames.Length; i++)
        {
            string p = savePath + "/" + saveName + i.ToString();
            if (hasAlpha) { p += ".png"; } else { p += ".jpg"; }
            g.frames[i] = AssetDatabase.LoadAssetAtPath<Texture2D>(p);
        }
        AssetDatabase.CreateAsset(g, savePath + "/" + saveName + ".asset");
        AssetDatabase.SaveAssets();
        Destroy(c);
        gifPlayer.Initialize(g);
        done.SetActive(true);
    }
}
#endif