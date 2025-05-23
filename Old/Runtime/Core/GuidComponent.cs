using System;
using UnityEngine;

namespace Core
{
    [
        Serializable,
        DisallowMultipleComponent
    ]
    public class GuidComponent : MonoBehaviour
    {
        private bool _isInitialized;
        private Guid _cachedGuid;

        [SerializeField]
        private byte[] serializedGuid;
        public Guid Guid
        {
            get
            {
                // Use cached value, if available
                if (_cachedGuid != Guid.Empty) return _cachedGuid;

                // Parse from serialized data, if valid
                if (serializedGuid is { Length: 16 }) return new Guid(serializedGuid);

                // Generate new GUID, if needed
                Guid = GuidManager.GenerateUniqueGuid();

                return Guid;
            }
            private set
            {
                _cachedGuid = value;
                serializedGuid = value.ToByteArray();
            }
        }

        private void Awake() { Initialize(); }

        private void Initialize()
        {
            if (_isInitialized) return;

            Guid = GuidManager.Register(this);
            _isInitialized = true;
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            // Called when copying a component or applying a prefab
            if (GuidManagerUtility.IsInPrefabContext(gameObject))
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

        private void OnDestroy()
        {
            GuidManager.Unregister(this);
        }
    }
}
