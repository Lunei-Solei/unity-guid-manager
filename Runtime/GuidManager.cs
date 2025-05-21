using System;
using System.Collections.Generic;
using GuidComponent;
using GuidInfo;
using Manager;
using UnityEditor;

public static class GuidManager
{
    public static void Unregister(Guid guid) => implementer.UnregisterImplementation(guid);
    public static IGuidInfo GetInfo(Guid guid) => implementer.GetInfoImplementation(guid);
    public static Guid Register(IGuidComponent target) => implementer.RegisterImplementation(target);
    public static void Unregister(IGuidComponent target) => implementer.UnregisterImplementation(target);
    public static IGuidInfo GetInfo(IGuidComponent target) => implementer.GetInfoImplementation(target);
    public static Guid Register(IGuidManagerComponent target) => implementer.RegisterImplementation(target);
    public static void Unregister(IGuidManagerComponent target) => implementer.UnregisterImplementation(target);
    public static IGuidInfo GetInfo(IGuidManagerComponent target) => implementer.GetInfoImplementation(target);
    public static Guid Register(SceneAsset target) => implementer.RegisterImplementation(target);
    public static void Unregister(SceneAsset target) => implementer.UnregisterImplementation(target);
    public static IGuidInfo GetInfo(SceneAsset target) => implementer.GetInfoImplementation(target);

    private static readonly IGuidManagerComponent implementer = new GuidManagerImplementer();

    static GuidManager()
    {
        if (implementer.Guid == Guid.Empty) implementer.Guid = Guid.NewGuid();
    }

    private class GuidManagerImplementer : IGuidManagerComponent
    {
        private static readonly Dictionary<Guid, IGuidInfo> guidToInfoMap = new Dictionary<Guid, IGuidInfo>();
        private static byte[] s_serializedGuid;
        public Guid Guid
        {
            get
            {
                Guid.TryParse(s_serializedGuid.ToString(), out Guid guid);

                return guid;
            }
            set => s_serializedGuid = value.ToByteArray();
        }

        public void UnregisterImplementation(Guid guid) => guidToInfoMap.Remove(guid);

        public IGuidInfo GetInfoImplementation(Guid guid)
        {
            guidToInfoMap.TryGetValue(guid, out IGuidInfo info);

            return info;
        }

        public Guid RegisterImplementation(IGuidComponent target)
        {
            IGuidInfo targetInfo = new GuidComponentInfo(target);

            return GuidManagerUtility.AddToMap(guidToInfoMap, targetInfo);
        }

        public void UnregisterImplementation(IGuidComponent target) => guidToInfoMap.Remove(target.Guid);

        public IGuidInfo GetInfoImplementation(IGuidComponent target)
        {
            guidToInfoMap.TryGetValue(target.Guid, out IGuidInfo info);

            return info;
        }

        public Guid RegisterImplementation(IGuidManagerComponent target)
        {
            IGuidInfo targetInfo = new GuidManagerComponentInfo(target);

            return GuidManagerUtility.AddToMap(guidToInfoMap, targetInfo);
        }

        public void UnregisterImplementation(IGuidManagerComponent target) => guidToInfoMap.Remove(target.Guid);

        public IGuidInfo GetInfoImplementation(IGuidManagerComponent target)
        {
            guidToInfoMap.TryGetValue(target.Guid, out IGuidInfo info);

            return info;
        }

        public Guid RegisterImplementation(SceneAsset target)
        {
            IGuidInfo targetInfo = new AssetInfo();

            return GuidManagerUtility.AddToMap(guidToInfoMap, targetInfo);
        }

        // NotImplementedException
        public void UnregisterImplementation(SceneAsset target) => throw new NotImplementedException();
        public IGuidInfo GetInfoImplementation(SceneAsset target) => throw new NotImplementedException();
    }
}
