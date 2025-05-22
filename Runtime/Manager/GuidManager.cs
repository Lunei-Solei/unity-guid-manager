using System;
using System.Collections.Generic;
using Core;
using UnityEditor;
using UnityEngine.SceneManagement;

public static class GuidManager
{
    private static readonly Config config;
    private static readonly Dictionary<Guid, IGuidInfoBase> guidToInfoMap = new Dictionary<Guid, IGuidInfoBase>();
    private static byte[] s_serializedGuid;

    public static Guid Guid
    {
        get
        {
            Guid.TryParse(s_serializedGuid?.ToString(), out Guid guid);
            if (guid != Guid.Empty) return guid;

            Guid cachedGuid = config.ManagerGuid;
            if (cachedGuid != Guid.Empty)
            {
                Guid = cachedGuid;
                return cachedGuid;
            }

            Guid newGuid = Guid.NewGuid();
            config.ManagerGuid = newGuid;
            Guid = newGuid;

            return newGuid;
        }
        set
        {
            s_serializedGuid = value.ToByteArray();
            config.ManagerGuid = value;
        }
    }

    static GuidManager()
    {
        config = Config.GetConfigAsset();
    }

    public static void Unregister(Guid guid) => guidToInfoMap.Remove(guid);

    public static IGuidInfoBase GetInfo(Guid guid)
    {
        guidToInfoMap.TryGetValue(guid, out IGuidInfoBase info);

        return info;
    }

    public static T GetInfo<T>(Guid guid) where T : IGuidInfoBase
    {
        guidToInfoMap.TryGetValue(guid, out IGuidInfoBase info);

        return (T)info;
    }

    public static Guid Register(GuidComponent target)
    {
        IGuidInfoBase targetInfo = new ComponentGuidInfo(target);

        return GuidManagerUtility.AddToMap(guidToInfoMap, targetInfo);
    }

    public static void Unregister(GuidComponent target) => guidToInfoMap.Remove(target.Guid);

    public static IGuidInfoBase GetInfo(GuidComponent target)
    {
        guidToInfoMap.TryGetValue(target.Guid, out IGuidInfoBase info);

        return info;
    }

    public static T GetInfo<T>(GuidComponent target) where T : IGuidInfoBase
    {
        guidToInfoMap.TryGetValue(target.Guid, out IGuidInfoBase info);

        return (T)info;
    }

    public static Guid Register(Scene target)
    {
        IGuidInfoBase targetInfo = new AssetGuidInfo(target);

        return GuidManagerUtility.AddToMap(guidToInfoMap, targetInfo);
    }

    public static void Unregister(Scene target)
    {
        string guidString = AssetDatabase.AssetPathToGUID(target.path);
        Guid.TryParse(guidString, out Guid guid);
        guidToInfoMap.Remove(guid);
    }

    public static IGuidInfoBase GetInfo(Scene target)
    {
        string guidString = AssetDatabase.AssetPathToGUID(target.path);
        Guid.TryParse(guidString, out Guid guid);
        guidToInfoMap.TryGetValue(guid, out IGuidInfoBase info);

        return info;
    }

    public static T GetInfo<T>(Scene target) where T : IGuidInfoBase
    {
        string guidString = AssetDatabase.AssetPathToGUID(target.path);
        Guid.TryParse(guidString, out Guid guid);
        guidToInfoMap.TryGetValue(guid, out IGuidInfoBase info);

        return (T)info;
    }
}
