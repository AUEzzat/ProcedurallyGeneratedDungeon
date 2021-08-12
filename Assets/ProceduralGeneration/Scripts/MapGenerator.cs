using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    RawImage rawImage;
    [SerializeField]
    int texWidth = 1920;
    [SerializeField]
    int texHeight = 1080;

    Texture2D texture;

    // Start is called before the first frame update
    void Start()
    {
        rawImage.texture = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);
        texture = (Texture2D)rawImage.texture;

        DrawBlock(0, 0, texWidth, texHeight, Color.gray);
        DrawBlock(100, 100, 10, 110, Color.black);
        DrawBlock(100, 210, 110, 110, Color.black);
        DrawBlock(210, 210, 110, 10, Color.black);
        DrawBlock(320, 210, 110, 110, Color.black);
        texture.Apply();
        rawImage.texture = texture;
    }

    void DrawBlock(int x, int y, int width, int height, Color color)
    {
        int startX = x - (width / 2);
        int startY = texHeight - (y - (height / 2));
        for (int i = startX; i < startX + width; i++)
        {
            for (int j = startY; j > startY - height; j--)
            {
                texture.SetPixel(i, j, color);
            }
        }
    }
}