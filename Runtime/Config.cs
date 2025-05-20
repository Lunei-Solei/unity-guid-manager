using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("LuneiSolei.GuidManager.Editor")]

[CreateAssetMenu]
public class Config : ScriptableObject
{
    internal const string PackageName = "com.luneisolei.guidmanager";
    internal const string PackageInitializedKey = "LuneiSolei.GuidManager.Initialized";
    internal const string PackageEditorUIDirectory = "Packages/com.luneisolei.guidmanager/Editor/UI/";
    private const string PackageConfigAsset = "Packages/com.luneisolei.guidmanager/Runtime/Config.asset";

    private static Config s_configAsset;
    private bool _persistManagersSceneInEditor;

    [HideInInspector, SerializeField]
    private byte[] guidManager;

    [HideInInspector, SerializeField]
    private byte[] managersSceneGuid;

    public static Config GetOrCreateAsset()
    {
        if (s_configAsset) return s_configAsset;

        s_configAsset = AssetDatabase.LoadAssetAtPath<Config>(PackageConfigAsset);

        if (s_configAsset) return s_configAsset;

        s_configAsset = CreateInstance<Config>();
        AssetDatabase.CreateAsset(s_configAsset, PackageConfigAsset);
        AssetDatabase.SaveAssets();

        return s_configAsset;
    }

    // Editor Only Functionality
#if UNITY_EDITOR
    public SceneAsset GetManagersSceneAsset()
    {
        Guid.TryParse(managersSceneGuid.ToString(), out Guid systemGuid);
        if (systemGuid == Guid.Empty) return null;

        string scenePath = AssetDatabase.GUIDToAssetPath(systemGuid.ToString());
        SceneAsset managersSceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

        return managersSceneAsset;
    }

    public void SetManagersSceneAsset(SceneAsset sceneAsset)
    {
        // Generate and serialize GUID
        string sceneAssetPath = AssetDatabase.GetAssetOrScenePath(sceneAsset);
        managersSceneGuid = new Guid(AssetDatabase.AssetPathToGUID(sceneAssetPath)).ToByteArray();

        // Validate the GUID for the GuidManager
        Guid.TryParse(guidManager.ToString(), out Guid managerGuid);
        if (managerGuid == Guid.Empty) return;

        // Look inside the scene for a GuidManager that matches the GUID
        string sceneContent = File.ReadAllText(sceneAssetPath);


        // Register Managers Scene to GuidManager
        // Test line
    }
#endif
}
