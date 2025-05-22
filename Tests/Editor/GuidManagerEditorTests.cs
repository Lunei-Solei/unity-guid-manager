using System;
using System.Collections;
using Core;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Editor
{
    public class GuidManagerEditorTests
    {
        private const string DevSceneAssetPath = "Packages/com.luneisolei.guidmanager/Tests/Editor/DevScene.unity";
        private Scene _devScene;
        private Config _config;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            // Get and save the currently untitled scene
            _devScene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(_devScene, DevSceneAssetPath);

            // Set Configuration
            _config = Config.GetConfigAsset();

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            AssetDatabase.DeleteAsset(DevSceneAssetPath);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuidComponentTest()
        {
            // Test GuidComponent registration
            GuidComponent testGuidComponent = new GameObject().AddComponent<GuidComponent>();
            Guid guid = testGuidComponent.Guid;
            Assert.That(guid, Is.Not.EqualTo(Guid.Empty));

            IGuidInfoBase guidInfo = GuidManager.GetInfo(guid);
            Assert.That(guidInfo, Is.Not.Null);

            // Test GuidComponent unregistration
            UnityEngine.Object.DestroyImmediate(testGuidComponent);
            guidInfo = GuidManager.GetInfo(guid);
            Assert.That(guidInfo, Is.Not.Null);

            yield return null;
        }
    }
}
