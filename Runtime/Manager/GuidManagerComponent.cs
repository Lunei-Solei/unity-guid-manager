using System;
using System.Collections.Generic;
using GuidComponent;
using GuidInfo;
using UnityEditor;
using UnityEngine;

namespace Manager
{
    [
        AddComponentMenu("Lunei Solei/Tools/Guid Manager"),
        DisallowMultipleComponent
    ]
    public class GuidManagerComponent : MonoBehaviour, IGuidManagerComponent
    {
        private readonly Dictionary<Guid, IGuidInfo> _guidToInfoMap = new Dictionary<Guid, IGuidInfo>();
        private byte[] _serializedGuid;

        private void OnEnable() { Awake(); }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SystemGuid = Register(this);
        }

        // Manager Specific Implementations
        public Guid SystemGuid {get; private set;}

        public void Unregister(Guid systemGuid)
        {
            _guidToInfoMap.Remove(systemGuid);
        }

        public Guid GetGuid()
        {
            if (Guid.TryParse(_serializedGuid.ToString(), out Guid systemGuid)) return systemGuid;

            Guid newGuid = Guid.NewGuid();
            _serializedGuid = newGuid.ToByteArray();

            return newGuid;
        }

        public IGuidInfo GetInfo(Guid systemGuid)
        {
            _guidToInfoMap.TryGetValue(systemGuid, out IGuidInfo info);
            info?.ValidateGuidInfo(systemGuid, this);

            return info;
        }

        // GuidComponent Implementations
        public Guid Register(IGuidComponent target)
        {
            IGuidInfo targetInfo = new GuidComponentInfo(target);

            return GuidManagerUtility.AddToMap(_guidToInfoMap, targetInfo, this);
        }

        public void Unregister(IGuidComponent target)
        {
            _guidToInfoMap.Remove(target.SystemGuid);
        }

        public Guid GetGuid(IGuidComponent target)
        {
            return target.SystemGuid;
        }

        public IGuidInfo GetInfo(IGuidComponent target)
        {
            _guidToInfoMap.TryGetValue(target.SystemGuid, out IGuidInfo info);
            info?.ValidateGuidInfo(target.SystemGuid, this);

            return info;
        }

        // GuidManagerComponent Implementations
        public Guid Register(IGuidManagerComponent target)
        {
            IGuidInfo targetInfo = new GuidManagerComponentInfo(target);

            return GuidManagerUtility.AddToMap(_guidToInfoMap, targetInfo, this);
        }

        public void Unregister(IGuidManagerComponent target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        public Guid GetGuid(IGuidManagerComponent target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        public IGuidInfo GetInfo(IGuidManagerComponent target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        // ManagersSceneAsset Implementations
        public Guid Register(SceneAsset target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        public void Unregister(SceneAsset target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        public Guid GetGuid(SceneAsset target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        public IGuidInfo GetInfo(SceneAsset target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }
    }
}
