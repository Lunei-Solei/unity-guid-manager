using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class GuidManager
{
    private static readonly Dictionary<Guid, IGuidInfo> guidToInfoMap = new Dictionary<Guid, IGuidInfo>();
    public enum GuidType : ushort
    {
        Component,
        Asset
    }

    public static Guid GenerateUniqueGuid(Guid guid = default)
    {
        while (guidToInfoMap.TryGetValue(guid, out _) || guid == Guid.Empty) guid = Guid.NewGuid();

        return guid;
    }

    public static Guid Register(GuidComponent target)
    {
        GuidInfo targetInfo = new GuidInfo(target);
        Guid guid = GenerateUniqueGuid(target.Guid);
        if (guid != target.Guid) target.SetGuid(guid);

        return guidToInfoMap.TryAdd(guid, targetInfo) ? guid : Guid.Empty;
    }

    public static IGuidInfo GetGuidInfo(Guid guid)
    {
        guidToInfoMap.TryGetValue(guid, out IGuidInfo info);

        return info;
    }

    public static void Unregister(Guid guid) => guidToInfoMap.Remove(guid);

    public static Dictionary<Guid, IGuidInfo> GetGuidMap() => guidToInfoMap;

    [MenuItem("Tools/GUID Manager/Refresh All GUIDs")]
    public static void ShowCleanUpWindow()
    {
        CleanUp();
    }
    
    public static void CleanUp()
    {
        int totalItems = guidToInfoMap.Count;
        int i = 0;
        int progressId = Progress.Start("Refreshing GUIDs");
        Debug.Log("Refreshing GUIDs");
        List<Guid> guidsToRemove = new List<Guid>();
        
        try
        {
            foreach ((Guid key, IGuidInfo value) in guidToInfoMap)
            {
                Debug.Log($"Removing {key}");
                Progress.Report(progressId, i / (float)totalItems, $"Processing {i + 1} / {totalItems}");
                switch (value.GuidInfoType)
                {
                    case GuidType.Component:
                        if (!value.GuidComponent || !value.GameObject) guidsToRemove.Add(key);
                        break;
                    case GuidType.Asset:
                        break;
                    default:
                        break;
                }

                i++;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        
        foreach (Guid guid in guidsToRemove) guidToInfoMap.Remove(guid);
        Progress.Finish(progressId);
        Debug.Log("Finished refreshing GUIDs");
    }

    [MenuItem("Tools/GUID Manager/Clear All GUIDs")]
    public static void Clear() => guidToInfoMap.Clear();
}
