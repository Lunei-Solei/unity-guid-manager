using System;
using System.Collections.Generic;

public static class GuidManager
{
    private static readonly Dictionary<Guid, IGuidInfo> guidToInfoMap = new Dictionary<Guid, IGuidInfo>();

    public static Guid GenerateUniqueGuid(Guid guid = default)
    {
        while (guidToInfoMap.TryGetValue(guid, out _)) guid = Guid.NewGuid();

        return guid;
    }

    public static Guid Register(IGuidInfo target)
    {
        Guid guid = GenerateUniqueGuid(target.Guid);
        if (guid != target.Guid) target.UpdateGuid(guid);

        return guidToInfoMap.TryAdd(guid, target) ? guid : Guid.Empty;
    }

    public static GuidComponent GetGuidComponent(Guid guid)
    {
        guidToInfoMap.TryGetValue(guid, out IGuidInfo info);

        return (GuidComponent)info;
    }

    public static void Unregister(Guid guid) => guidToInfoMap.Remove(guid);
}
