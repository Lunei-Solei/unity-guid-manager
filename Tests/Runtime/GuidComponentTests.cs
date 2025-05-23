using System;
using System.Collections;
using NUnit.Framework;
using Tests.Shared;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    public class GuidComponentTests
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator RegistrationTest()
        {
            GuidComponent guidComponent;

            using (Measure.Scope("GuidComponent Registration"))
            {
                guidComponent = GuidComponentSharedTests.Registration();
            }

            Assert.That(guidComponent, Is.Not.Null);

            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator UnregistrationTest()
        {
            GuidComponent guidComponent;

            using (Measure.Scope("GuidComponent Unregistration"))
            {
                guidComponent = GuidComponentSharedTests.Unregistration();
            }

            Assert.That(guidComponent, Is.Null);

            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator DestructionTest()
        {
            GuidComponent guidComponent = new GameObject().AddComponent<GuidComponent>();
            Guid guid = guidComponent.Guid;
            UnityEngine.Object.Destroy(guidComponent);

            yield return null;

            Assert.That(!GuidManager.GetGuidInfo(guid).GuidComponent, Is.True);
        }
    }
}
