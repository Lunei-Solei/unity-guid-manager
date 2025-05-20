using System;
using GuidComponent;
using GuidInfo;
using UnityEditor;

namespace Manager
{
    public interface IGuidManagerComponent
    {
        // Manager Specific Implementations
        public Guid SystemGuid {get;}
        public void Unregister(Guid systemGuid);
        public Guid GetGuid();
        public IGuidInfo GetInfo(Guid systemGuid);

        // GuidComponent Implementations
        public Guid Register(IGuidComponent target);
        public void Unregister(IGuidComponent target);
        public Guid GetGuid(IGuidComponent target);
        public IGuidInfo GetInfo(IGuidComponent target);

        // GuidManagerComponent Implementations
        public Guid Register(IGuidManagerComponent target);
        public void Unregister(IGuidManagerComponent target);
        public Guid GetGuid(IGuidManagerComponent target);
        public IGuidInfo GetInfo(IGuidManagerComponent target);

        // ManagersSceneAsset Implementations
        public Guid Register(SceneAsset target);
        public void Unregister(SceneAsset target);
        public Guid GetGuid(SceneAsset target);
        public IGuidInfo GetInfo(SceneAsset target);
    }
}
