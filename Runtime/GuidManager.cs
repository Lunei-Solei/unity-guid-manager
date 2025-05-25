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
    public static async void ShowCleanUpWindow()
    {
        try
        {
            for (int i = 0; i < 1000; i++)
            {
                guidToInfoMap.Add(GenerateUniqueGuid(), new GuidInfo());
            }
            await CleanUp();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    
    public static async Task CleanUp()
    {
        int totalItems = guidToInfoMap.Count;
        int i = 0;
        int progressId = Progress.Start("Refreshing GUIDs");
        List<Guid> guidsToRemove = new List<Guid>();
        
        foreach ((Guid key, IGuidInfo value) in guidToInfoMap)
        {
            Progress.Report(progressId, i / (float)totalItems, $"Processing {i + 1} / {totalItems}");
            await Task.Yield();
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
        
        foreach (Guid guid in guidsToRemove) guidToInfoMap.Remove(guid);
        Progress.Finish(progressId);
    }

    [MenuItem("Tools/GUID Manager/Clear All GUIDs")]
    public static void Clear() => guidToInfoMap.Clear();
}
