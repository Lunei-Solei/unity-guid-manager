using System.Collections;
using System.Threading.Tasks;
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
            Measure.Method(() =>
                {
                    Task.Run(async () => await CleanUpTestAction()).Wait();
                })
                .WarmupCount(10)
                .MeasurementCount(100)
                .IterationsPerMeasurement(10)
                .SampleGroup("GuidManager Cleanup")
                .CleanUp(GuidManager.Clear)
                .Run();
            
            yield return null;
        }
        
        private async Task CleanUpTestAction()
        {
            for (int i = 0; i < 1000; i++)
            {
                GuidManager.GetGuidMap().TryAdd(GuidManager.GenerateUniqueGuid(), new MockGuidInfo());
            }

            await GuidManager.CleanUp();
        
            Assert.That(GuidManager.GetGuidMap().Count, Is.Zero);
        }
    }
}
