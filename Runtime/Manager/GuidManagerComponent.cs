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
            Guid = Register(this);
        }

        // Manager Specific Implementations
        public Guid Guid {get; private set;}

        public void UnregisterImplementation(Guid guid)
        {
            _guidToInfoMap.Remove(guid);
        }

        public Guid GetManagerGuidImplementation()
        {
            if (Guid.TryParse(_serializedGuid.ToString(), out Guid guid)) return guid;

            Guid newGuid = Guid.NewGuid();
            _serializedGuid = newGuid.ToByteArray();

            return newGuid;
        }

        public IGuidInfo GetInfoImplementation(Guid guid)
        {
            _guidToInfoMap.TryGetValue(guid, out IGuidInfo info);
            info?.ValidateGuidInfo(guid, this);

            return info;
        }

        // GuidComponent Implementations
        public Guid RegisterImplementation(IGuidComponent target)
        {
            IGuidInfo targetInfo = new GuidComponentInfo(target);

            return GuidManagerUtility.AddToMap(_guidToInfoMap, targetInfo, this);
        }

        public void UnregisterImplementation(IGuidComponent target)
        {
            _guidToInfoMap.Remove(target.Guid);
        }

        public Guid GetGuidImplementation(IGuidComponent target) => target.Guid;

        public IGuidInfo GetInfoImplementation(IGuidComponent target)
        {
            _guidToInfoMap.TryGetValue(target.Guid, out IGuidInfo info);
            info?.ValidateGuidInfo(target.Guid, this);

            return info;
        }

        // GuidManagerComponent Implementations
        public Guid Register(IGuidManagerComponent target)
        {
            IGuidInfo targetInfo = new GuidManagerComponentInfo(target);

            return GuidManagerUtility.AddToMap(_guidToInfoMap, targetInfo, this);
        }

        public void UnregisterImplementation(IGuidManagerComponent target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        public Guid GetGuidImplementation(IGuidManagerComponent target) => throw
            // NotImplementedException
            new NotImplementedException();

        public IGuidInfo GetInfoImplementation(IGuidManagerComponent target) => throw
            // NotImplementedException
            new NotImplementedException();

        // ManagersSceneAsset Implementations
        public Guid RegisterImplementation(SceneAsset target) => throw
            // NotImplementedException
            new NotImplementedException();

        public void UnregisterImplementation(SceneAsset target)
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        public Guid GetGuidImplementation(SceneAsset target) => throw
            // NotImplementedException
            new NotImplementedException();

        public IGuidInfo GetInfoImplementation(SceneAsset target) => throw
            // NotImplementedException
            new NotImplementedException();
    }
}
