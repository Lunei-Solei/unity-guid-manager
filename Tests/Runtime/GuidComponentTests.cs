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
            Measure.Method(GuidComponentSharedTests.RegistrationTest)
                .WarmupCount(10)
                .MeasurementCount(100)
                .IterationsPerMeasurement(10)
                .SampleGroup("GuidComponent Registration")
                .SetUp(GuidManager.Refresh)
                .CleanUp(GuidManager.Clear)
                .Run();

            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator UnregistrationTest()
        {
            Measure.Method(GuidComponentSharedTests.UnregistrationTest)
                .WarmupCount(10)
                .MeasurementCount(100)
                .IterationsPerMeasurement(10)
                .SampleGroup("GuidComponent Unregistration")
                .SetUp(GuidManager.Refresh)
                .CleanUp(GuidManager.Clear)
                .Run();

            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator DestructionTest()
        {
            GuidComponent guidComponent = new GameObject().AddComponent<GuidComponent>();
            Guid guid = guidComponent.Guid;
            UnityEngine.Object.Destroy(guidComponent);

            yield return null;

            Assert.That(GuidManager.GetGuidMap().Count, Is.EqualTo(0));
            Assert.That(GuidManager.GetGuidInfo(guid), Is.Null);
        }
    }
}
