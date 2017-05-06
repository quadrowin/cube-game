using UnityEditor;

[CustomEditor(typeof(BuildTimeText))]
public class BuildTimeEditor : Editor {

    public BuildTimeText scriptTarget;

    public void Awake()
    {
        scriptTarget = (BuildTimeText)target;
    }

    public override void OnInspectorGUI()
    {
        //scriptTarget.buildTime = BuildTimeData.buildTime;
        //scriptTarget.buildVersion = BuildTimeData.buildVersion != "" ? BuildTimeData.buildVersion : PlayerSettings.bundleVersion;

        //scriptTarget.buttoncount = EditorGUILayout.IntField("Button count : ", scriptTarget.buttoncount);

        scriptTarget.buildTime = EditorGUILayout.TextField("Build Time", BuildTimeData.GetBuildTime());
        scriptTarget.buildVersion = EditorGUILayout.TextField("Build Version", BuildTimeData.GetBuildVersion());
    }

}
