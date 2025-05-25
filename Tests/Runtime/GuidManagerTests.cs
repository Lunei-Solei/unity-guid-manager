using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    public class GuidManagerTests
    {
        private GameObject _parentContainer;
        
        [UnityTearDown]
        public IEnumerator Teardown()
        {
            Object.Destroy(_parentContainer);
            yield return null;
        }
    }
}
