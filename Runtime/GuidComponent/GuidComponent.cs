using System;
using GuidInfo;
using Manager;
using UnityEngine;

namespace GuidComponent
{
    [
        Serializable,
        DisallowMultipleComponent
    ]
    public class GuidComponent : MonoBehaviour, ISerializationCallbackReceiver, IGuidComponent
    {
        [SerializeReference]
        private IGuidManagerComponent guidService;

        [SerializeField]
        internal byte[] serializedGuid;

        public Guid Guid
        {
            get
            {
                Guid.TryParse(serializedGuid.ToString(), out Guid result);

                return result;
            }
            private set => serializedGuid = value.ToByteArray();
        }

        private bool _isInitialized;

        private void Initialize()
        {
            if (_isInitialized || guidService == null) return;

            // TODO: Grab IGuidManagerComponent

            if (Guid == Guid.Empty) Guid = guidService.RegisterImplementation(this);

            serializedGuid = Guid.ToByteArray();
            IGuidInfo info = guidService.GetInfoImplementation(this);

            // if (info != null) info.OnUpdated += OnUpdatedEventHandler;

            _isInitialized = true;
        }

        private void Awake() { Initialize(); }

        private void OnValidate()
        {
#if UNITY_EDITOR
            // Called when copying a component or applying a prefab
            if (GuidManagerUtility.IsAssetOnDisk(gameObject))
            {
                serializedGuid = null;
                Guid = Guid.Empty;
            }
            else
#endif
            {
                Initialize();
            }
        }

        /// <summary>
        ///     Verifies and retrieves the currently stored serialized GUID.
        /// </summary>
        /// <returns>A deserialized system GUID.</returns>
        public Guid GetGuid()
        {
            if (Guid == Guid.Empty && serializedGuid is { Length: 16 })
            {
                Guid = new Guid(serializedGuid);
            }

            return Guid;
        }

        public void OnDestroy()
        {
            guidService.UnregisterImplementation(this);
        }

        // private void OnUpdatedEventHandler(object sender, GuidComponentInfo.OnUpdatedEventArgs eventArgs)
        // {
        //     Guid = eventArgs.Guid;
        //     serializedGuid = eventArgs.Guid.ToByteArray();
        // }

        public void OnBeforeSerialize()
        {
            // Convert the GUID to a byte array so Unity can serialize it.
#if UNITY_EDITOR
            if (GuidManagerUtility.IsAssetOnDisk(gameObject))
            {
                serializedGuid = null;
                Guid = Guid.Empty;
            }
            else
#endif
            {
                if (Guid != Guid.Empty)
                {
                    serializedGuid = Guid.ToByteArray();
                }
            }
        }

        public void OnAfterDeserialize()
        {
            _isInitialized = false;
            if (serializedGuid is { Length: 16 })
            {
                Guid = new Guid(serializedGuid);
            }
        }
    }
}
