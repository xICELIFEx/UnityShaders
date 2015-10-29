using UnityEngine;
using System.Collections;

public class ProceduralTexture : MonoBehaviour {

    #region Public Variables
    public int widthHieght = 512;
    public Texture2D generatedTexture;
    #endregion

    #region Private Variables

    private Material currentMaterial;
    private Vector2 centerPosition;
    #endregion
    // Use this for initialization
    void Start ()
    {
        if (!currentMaterial)
        {
            currentMaterial = renderer.sharedMaterial;
            if (!currentMaterial)
            {
                Debug.LogWarning("Cannot find a matireal on: " + transform.name);
            }
        }

        if (currentMaterial)
        {
            centerPosition = new Vector2(0.5f, 0.5f);
            generatedTexture = GenerateGradient();
            currentMaterial.SetTexture("_MainTex", generatedTexture);
        }
	}

    private Texture2D GenerateGradient()
    {
        return GenerateCircleGradient();
    }

    private Texture2D GenerateCircleGradient()
    {
        Texture2D proceduralTexture = new Texture2D(widthHieght, widthHieght);

        Vector2 centerPixelPosition = centerPosition*widthHieght;

        for (int x = 0; x < widthHieght; x++)
        {
            for (int y = 0; y < widthHieght; y++)
            {
                Vector2 currentPosition = new Vector2(x, y);
                float pixelDistance = Vector2.Distance(currentPosition, centerPixelPosition)/(widthHieght * 0.5f);
                pixelDistance = Mathf.Abs(1 - Mathf.Clamp(pixelDistance, 0f, 1f));
                Color pixelColor = new Color(pixelDistance, pixelDistance, pixelDistance, 1.0f);
                proceduralTexture.SetPixel(x, y, pixelColor);
            }
        }
        proceduralTexture.Apply();
        return proceduralTexture;
    }
}
