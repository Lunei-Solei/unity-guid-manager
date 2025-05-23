using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class AssetGuidInfo : IGuidInfoBase
{
    public Guid Guid {get; set;}
    public string AssetPath {get; set;}

    public AssetGuidInfo(Scene target)
    {
        AssetPath = target.path;
        Guid.TryParse(AssetDatabase.AssetPathToGUID(AssetPath), out Guid guid);
        Guid = guid;
    }
}
