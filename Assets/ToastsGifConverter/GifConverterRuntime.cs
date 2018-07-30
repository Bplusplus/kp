using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class GifConverterRuntime : MonoBehaviour
{
    public bool finished;
    public GifData gifData;

    public void Init(string path)
    {
        finished = false;
        gifData = ScriptableObject.CreateInstance<GifData>();
        StartCoroutine(calculate(path));
    }

    public IEnumerator calculate(string path)
    {
        Image gif = Image.FromFile(path);
        FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);
        Texture2D[] result = new Texture2D[gif.GetFrameCount(fd)];
        for (int i = 0; i < result.Length; i++)
        {
            yield return new WaitForEndOfFrame();
            gif.SelectActiveFrame(fd, i);
            Bitmap bmp = (Bitmap)gif;
            Rectangle area = (new Rectangle(0, 0, bmp.Width, bmp.Height));
            BitmapData bitmapData = bmp.LockBits(area, ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            System.IntPtr ptr = bitmapData.Scan0;
            result[i] = new Texture2D(gif.Width, gif.Height, TextureFormat.BGRA32, false);
            byte[] data = ImageToByte(bmp);
            result[i].LoadRawTextureData(ptr, gif.Width * gif.Height * 4);
            result[i].Apply();
            bmp.UnlockBits(bitmapData);
            System.GC.Collect();
        }
        yield return new WaitForEndOfFrame();
        gifData.frames = result;
        PropertyItem item = gif.GetPropertyItem(0x5100);
        gifData.delay = item.Value[0] / 100f;
        gifData.endurance = gif.GetFrameCount(fd) * gifData.delay;
        finished = true;
    }

    public static byte[] ImageToByte(Image img)
    {
        using (var stream = new MemoryStream())
        {
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return stream.ToArray();
        }
    }
}
