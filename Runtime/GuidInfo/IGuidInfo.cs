using System;
using Manager;
using UnityEngine;

namespace GuidInfo
{
    public interface IGuidInfo
    {
        public Guid Guid {get;}
        public GameObject GameObject {get;}
        public MonoBehaviour Component {get;}

        public void UpdateSystemGuid(Guid guid);
        public void ValidateGuidInfo(Guid guid, IGuidManagerComponent service);
    }
}
