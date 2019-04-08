using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class TakeImage : MonoBehaviour
{
    private int resWidth;
    private int resHeight;
    Camera camera;

    [HideInInspector] public bool takeHiResShot = false;

    private void Awake()
    {
        camera = GetComponent<Camera>();


    }

    private void Start()
    {

        resWidth = camera.targetTexture.width;
        resHeight = camera.targetTexture.height;

    }

    private static Texture2D BytesToTexture2D(byte[] bytes, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height);
        tex.LoadImage(bytes);
        return tex;

    }

    //must call this from a late update function
    public Texture2D TakePicture_2()
    {

        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = camera.targetTexture;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        byte[] bytes = screenShot.EncodeToPNG();

        return BytesToTexture2D(bytes, resWidth, resHeight);

    }
}
