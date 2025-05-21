using System;
using Manager;
using UnityEngine;

namespace GuidInfo
{
    public class AssetInfo : IGuidInfo
    {
        public Guid Guid {get; set;}
        public GameObject GameObject {get;}
        public MonoBehaviour Component {get;}

        public AssetInfo()
        {
            // NotImplementedException
            throw new NotImplementedException();
        }

        public void UpdateSystemGuid(Guid guid) { Guid = guid; }

        public void ValidateGuidInfo(Guid targetGuid, IGuidManagerComponent service)
        {
            // NotImplementedException
            throw new NotImplementedException();
            // bool isSystemGuidValid = Guid.TryParse(targetGuid.ToString(), out _);
            // bool isCachedGuidValid = Guid.TryParse(Guid.ToString(), out _);
            //
            // // Attempt to repair or unregister any broken registrations
            // switch (isSystemGuidValid)
            // {
            //     case false when !isCachedGuidValid:
            //         // Both system and cached GUIDs are not valid
            //         service.Unregister(targetGuid);
            //
            //         break;
            //     case false:
            //         // Target GUID is not valid, but cached GUID is valid
            //         service.Register(target);
            //         service.Unregister(targetGuid);
            //
            //         break;
            //     case true when !isCachedGuidValid:
            //         // Target GUID is valid, but cached GUID is not
            //         UpdateSystemGuid(targetGuid);
            //
            //         break;
            // }
        }
    }
}
