using System;
using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Editor
{
    public class GuidComponentEditorTests
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
                GameObject parentObject = new GameObject();
                guidComponent = parentObject.AddComponent<GuidComponent>();
                guidComponent = GuidManager.GetGuidComponent(guidComponent.Guid);
            }

            Assert.That(guidComponent, Is.Not.Null);

            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator UnregistrationTest()
        {
            GuidComponent guidComponent = new GameObject().AddComponent<GuidComponent>();

            using (Measure.Scope("GuidComponent Unregistration"))
            {
                GuidManager.Unregister(guidComponent.Guid);
                guidComponent = GuidManager.GetGuidComponent(guidComponent.Guid);
            }

            Assert.That(guidComponent, Is.Null);

            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator DestructionTest()
        {
            GuidComponent guidComponent = new GameObject().AddComponent<GuidComponent>();
            Guid guid = guidComponent.Guid;
            UnityEngine.Object.DestroyImmediate(guidComponent);

            Assert.That(!GuidManager.GetGuidComponent(guid), Is.True);

            yield return null;
        }
    }
}
