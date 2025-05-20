using System;
using GuidComponent;
using Manager;
using UnityEngine;

namespace GuidInfo
{
    public class GuidComponentInfo : IGuidInfo
    {
        public Guid SystemGuid {get; set;}
        public GameObject GameObject {get;}
        public MonoBehaviour Component {get;}

        internal GuidComponentInfo(IGuidComponent target)
        {
            SystemGuid = target.GetGuid();
            Component = (MonoBehaviour)target;
            GameObject = Component.gameObject;
        }

        public void UpdateSystemGuid(Guid guid) { SystemGuid = guid; }

        public void ValidateGuidInfo(Guid targetGuid, IGuidManagerComponent service)
        {
            bool isSystemGuidValid = Guid.TryParse(targetGuid.ToString(), out _);
            bool isCachedGuidValid = Guid.TryParse(SystemGuid.ToString(), out _);

            // Attempt to repair or unregister any broken registrations
            switch (isSystemGuidValid)
            {
                case false when !isCachedGuidValid:
                    // Both system and cached GUIDs are not valid
                    service.Unregister(targetGuid);

                    break;
                case false:
                    // Target GUID is not valid, but cached GUID is valid
                    service.Register((IGuidComponent)Component);
                    service.Unregister(targetGuid);

                    break;
                case true when !isCachedGuidValid:
                    // Target GUID is valid, but cached GUID is not
                    UpdateSystemGuid(targetGuid);

                    break;
            }
        }
    }
}
