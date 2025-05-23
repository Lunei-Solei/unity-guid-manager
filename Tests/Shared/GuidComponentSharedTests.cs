using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Shared
{
    public static class GuidComponentSharedTests
    {
        public static void RegistrationTest()
        {
            GuidComponent guidComponent = new GameObject().AddComponent<GuidComponent>();
            Assert.That(GuidManager.GetGuidMap().Count, Is.EqualTo(1));
        }

        public static void UnregistrationTest()
        {
            GuidComponent guidComponent = new GameObject().AddComponent<GuidComponent>();
            GuidManager.Unregister(guidComponent.Guid);
            
            Assert.That(GuidManager.GetGuidMap().Count, Is.EqualTo(0));
        }
    }
}
