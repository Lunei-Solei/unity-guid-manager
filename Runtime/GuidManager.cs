using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GuidManager
{
    private static readonly Dictionary<Guid, GuidInfo> guidToInfoMap = new Dictionary<Guid, GuidInfo>();
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

    public static GuidInfo GetGuidInfo(Guid guid)
    {
        guidToInfoMap.TryGetValue(guid, out GuidInfo info);

        return info;
    }

    public static void Unregister(Guid guid) => guidToInfoMap.Remove(guid);
    
    public static Dictionary<Guid, GuidInfo> GetGuidMap() => guidToInfoMap;

    [MenuItem("Tools/GUID Manager/Refresh All GUIDs")]
    public static void Refresh()
    {
        foreach ((Guid key, GuidInfo value) in guidToInfoMap)
        {
            switch (value.GuidInfoType)
            {
                case GuidType.Component:
                    if (!value.GuidComponent || !value.GameObject) Unregister(key); return;
                    break;
                case GuidType.Asset:
                    break;
                default:
                    break;
            }
        }
    }

}
