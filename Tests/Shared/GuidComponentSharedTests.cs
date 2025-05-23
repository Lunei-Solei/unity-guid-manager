using UnityEngine;

namespace Tests.Shared
{
    public static class GuidComponentSharedTests
    {
        public static GuidComponent Registration()
        {
            GameObject parentObject = new GameObject();
            GuidComponent guidComponent = parentObject.AddComponent<GuidComponent>();
            
            return GuidManager.GetGuidInfo(guidComponent.Guid)?.GuidComponent;
        }

        public static GuidComponent Unregistration()
        {
            GuidComponent guidComponent = new GameObject().AddComponent<GuidComponent>();
            GuidManager.Unregister(guidComponent.Guid);
            
            return GuidManager.GetGuidInfo(guidComponent.Guid)?.GuidComponent;
        }
    }
}
