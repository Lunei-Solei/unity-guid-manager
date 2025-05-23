using System;
using System.Collections.Generic;
using Core;

public static class GuidManager
{
    private static readonly Config config;
    internal static readonly Dictionary<Guid, IGuidInfoBase> GuidToInfoMap = new Dictionary<Guid, IGuidInfoBase>();
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
        private set
        {
            s_serializedGuid = value.ToByteArray();
            config.ManagerGuid = value;
        }
    }

    static GuidManager()
    {
        config = Config.GetConfigAsset();
    }

    public static Guid GenerateUniqueGuid() => GuidManagerUtility.GenerateUniqueGuid();

    public static void Unregister(Guid guid) => GuidToInfoMap.Remove(guid);

    public static IGuidInfoBase GetInfo(Guid guid)
    {
        GuidToInfoMap.TryGetValue(guid, out IGuidInfoBase info);

        return info;
    }

    public static T GetInfo<T>(Guid guid) where T : IGuidInfoBase
    {
        GuidToInfoMap.TryGetValue(guid, out IGuidInfoBase info);

        return (T)info;
    }

    public static Guid Register(GuidComponent target)
    {
        IGuidInfoBase targetInfo = new ComponentGuidInfo(target);

        return GuidManagerUtility.AddToMap(GuidToInfoMap, targetInfo);
    }

    public static void Unregister(GuidComponent target) => GuidToInfoMap.Remove(target.Guid);

    public static IGuidInfoBase GetInfo(GuidComponent target)
    {
        GuidToInfoMap.TryGetValue(target.Guid, out IGuidInfoBase info);

        return info;
    }

    public static T GetInfo<T>(GuidComponent target) where T : IGuidInfoBase
    {
        GuidToInfoMap.TryGetValue(target.Guid, out IGuidInfoBase info);

        return (T)info;
    }
}
