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

        [SerializeField]
        private byte[] serializedGuid;
        public Guid Guid
        {
            get
            {
                if (serializedGuid is { Length: 16 })
                {
                    return new Guid(serializedGuid);
                }

                Guid guid = Guid.NewGuid();
                serializedGuid = guid.ToByteArray();

                return guid;
            }
            private set => serializedGuid = value.ToByteArray();
        }

        private void Awake() { Initialize(); }

        private void Initialize()
        {
            if (_isInitialized) return;

            if (Guid == Guid.Empty) Guid = Guid.NewGuid();

            Guid = GuidManager.Register(this);
            _isInitialized = true;
        }

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

        private void OnDestroy()
        {
            GuidManager.Unregister(this);
        }
    }
}
