using System;
using GuidComponent;
using GuidInfo;
using UnityEditor;

namespace Manager
{
    public interface IGuidManagerComponent
    {
        // Miscellaneous Implementations
        public Guid Guid {get; set;}
        public void UnregisterImplementation(Guid guid);
        public IGuidInfo GetInfoImplementation(Guid guid);

        // GuidComponent Implementations
        public Guid RegisterImplementation(IGuidComponent target);
        public void UnregisterImplementation(IGuidComponent target);
        public IGuidInfo GetInfoImplementation(IGuidComponent target);

        // GuidManagerComponent Implementations
        public Guid RegisterImplementation(IGuidManagerComponent target);
        public void UnregisterImplementation(IGuidManagerComponent target);
        public IGuidInfo GetInfoImplementation(IGuidManagerComponent target);

        // ManagersSceneAsset Implementations
        public Guid RegisterImplementation(SceneAsset target);
        public void UnregisterImplementation(SceneAsset target);
        public IGuidInfo GetInfoImplementation(SceneAsset target);
    }
}
