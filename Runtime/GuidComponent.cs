using System;
using UnityEngine;

public class GuidComponent : MonoBehaviour, IGuidInfo
{
    private bool _isInitialized;
    private Guid _cachedGuid;

    [SerializeField]
    private byte[] serializedGuid;

    public Guid Guid
    {
        get
        {
            // Attempt to use cached GUID first
            if (_cachedGuid != Guid.Empty) return _cachedGuid;

            // Attempt to use serialized GUID second
            if (serializedGuid is { Length: 16 }) return new Guid(serializedGuid);

            // There is no GUID assigned to this component, assume it is not registered
            Guid newGuid = GuidManager.GenerateUniqueGuid();
            Guid = newGuid;

            return newGuid;
        }
        private set
        {
            _cachedGuid = value;
            serializedGuid = value.ToByteArray();
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_isInitialized) return;

        Guid = GuidManager.Register(this);
        _isInitialized = true;
    }

    void IGuidInfo.UpdateGuid(Guid guid) => Guid = guid;
}
