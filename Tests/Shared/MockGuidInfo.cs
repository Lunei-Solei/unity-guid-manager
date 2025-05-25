using System.Runtime.CompilerServices;
using UnityEngine;

[
    assembly: InternalsVisibleTo("LuneiSolei.GuidManager.Editor.Tests"),
              InternalsVisibleTo("LuneiSolei.GuidManager.Tests"),
              InternalsVisibleTo("LuneiSolei.GuidManager")
]

namespace Tests.Shared
{
    internal class MockGuidInfo : IGuidInfo
    {
        public GameObject GameObject {get;}
        public GuidComponent GuidComponent {get;}
        public GuidManager.GuidType GuidInfoType {get;}

        public MockGuidInfo()
        {
            GuidInfoType = GuidManager.GuidType.Component;
            GameObject = null;
            GuidComponent = null;
        }
    }
}