using UnityEngine;
using UnityEditor;

public class GenerateStaticCubeMap : ScriptableWizard
{
    [SerializeField]
    private Transform _renderPosition;

    [SerializeField]
    private Cubemap _cubemap;

    public void OnWizardUpdate()
    {
        helpString = "Select transform to render from and cubemap to render into";
        isValid = (_renderPosition != null && _cubemap != null);
    }

    public void OnWizardCreate()
    {
        GameObject tempCamera = new GameObject("CabeCam", typeof(Camera));
        tempCamera.transform.position = _renderPosition.position;
        tempCamera.transform.rotation = Quaternion.identity;

        tempCamera.camera.RenderToCubemap(_cubemap);

        Destroy(tempCamera);
    }

    [MenuItem("CookBook/Render Cubemap")]
    public static void RenderCubemap()
    {
        ScriptableWizard.DisplayWizard("Render CubeMap", typeof (GenerateStaticCubeMap), "Render!");
    }
}
