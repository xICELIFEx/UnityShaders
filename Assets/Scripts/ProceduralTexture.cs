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
        Texture2D proceduralTexture = new Texture2D(widthHieght, widthHieght);

        Vector2 centerPixelPosition = centerPosition * widthHieght;

        for (int x = 0; x < widthHieght; x++)
        {
            for (int y = 0; y < widthHieght; y++)
            {
                proceduralTexture.SetPixel(x, y, GenerateScalarMultiplierWithAxis(x, y, centerPixelPosition));
            }
        }
        proceduralTexture.Apply();
        return proceduralTexture;
    }

    private Color GenerateCircleGradient(int x, int y, Vector2 centerPixelPosition)
    {
        Vector2 currentPosition = new Vector2(x, y);
        float pixelDistance = Vector2.Distance(currentPosition, centerPixelPosition)/(widthHieght * 0.5f);
        pixelDistance = Mathf.Abs(1 - Mathf.Clamp(pixelDistance, 0f, 1f));
        return new Color(pixelDistance, pixelDistance, pixelDistance, 1.0f);
    }

    private Color GenerateCirclesGradient(int x, int y, Vector2 centerPixelPosition)
    {
        Vector2 currentPosition = new Vector2(x, y);
        float pixelDistance = Vector2.Distance(currentPosition, centerPixelPosition) / (widthHieght * 0.5f);
        pixelDistance = Mathf.Abs(1 - Mathf.Clamp(pixelDistance, 0f, 1f));
        pixelDistance = (Mathf.Sin(pixelDistance*30.0f)*pixelDistance);
        return new Color(pixelDistance, pixelDistance, pixelDistance, 1.0f);
    }

    private Color GenerateScalarMultiplier(int x, int y, Vector2 centerPixelPosition)
    {
        Vector2 currentPosition = new Vector2(x, y);
        Vector2 pixelDirection = centerPixelPosition - currentPosition;
        pixelDirection.Normalize();
        float rightDirection = Vector2.Dot(pixelDirection, Vector2.right);
        float leftDirection = Vector2.Dot(pixelDirection, -Vector2.right);
        float upDirection = Vector2.Dot(pixelDirection, Vector2.up);
        return new Color(rightDirection, leftDirection, upDirection, 1.0f);
    }

    private Color GenerateScalarMultiplierWithAxis(int x, int y, Vector2 centerPixelPosition)
    {
        Vector2 currentPosition = new Vector2(x, y);
        Vector2 pixelDirection = centerPixelPosition - currentPosition;
        pixelDirection.Normalize();
        float rightDirection = Vector2.Angle(pixelDirection, Vector2.right)/360;
        float leftDirection = Vector2.Angle(pixelDirection, -Vector2.right)/360;
        float upDirection = Vector2.Angle(pixelDirection, Vector2.up)/360;
        return new Color(rightDirection, leftDirection, upDirection, 1.0f);
    }
}
