using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class BuildTimeData : MonoBehaviour
{
    public static string buildName;

    public static string buildTime;

    public static string buildVersion;

    public void OnGUI()
    {
        GUILayout.Label("Build name: " + buildName);
    }

    public static string GetBuildTime()
    {
        if (buildTime == null || buildTime.Length < 3)
        {
            buildTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        return buildTime;
    }

    public static string GetBuildVersion()
    {
        return PlayerSettings.bundleVersion;
    }

#if UNITY_EDITOR
    public void Awake()
    {
        if (!Application.isPlaying)
        {
            buildTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buildName = buildTime + " (" + (UnityEditor.BuildPipeline.isBuildingPlayer ? "during build)" : "during edit)");
            buildVersion = PlayerSettings.bundleVersion;
        }
    }
#endif
}