using System.Collections;
using NUnit.Framework;
using Tests.Shared;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace Tests.Editor
{
    public class GuidManagerEditorTests
    {
        [UnityTest, Performance]
        public IEnumerator CleanUpTest()
        {
            Measure.Method(CleanUpTestAction)
                .WarmupCount(10)
                .MeasurementCount(100)
                .IterationsPerMeasurement(10)
                .SampleGroup("GuidManager Cleanup")
                .CleanUp(GuidManager.Clear)
                .Run();

            yield return null;
        }
        
        private void CleanUpTestAction()
        {
            for (int i = 0; i < 100; i++)
            {
                GuidManager.GetGuidMap().TryAdd(GuidManager.GenerateUniqueGuid(), new MockGuidInfo());
            }

            GuidManager.CleanUp();
            
            Assert.That(GuidManager.GetGuidMap().Count, Is.Zero);
        }
    }
}
