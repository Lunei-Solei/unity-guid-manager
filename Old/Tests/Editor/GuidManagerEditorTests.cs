// using System;
// using System.Collections;
// using NUnit.Framework;
// using UnityEditor;
// using UnityEditor.SceneManagement;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.TestTools;
//
// namespace Packages.com.luneisolei.guidmanager.Old.Tests.Editor
// {
//     public class GuidManagerEditorTests
//     {
//         private const string DevSceneAssetPath = "Packages/com.luneisolei.guidmanager/Tests/Editor/DevScene.unity";
//         private Scene _devScene;
//         private Config _config;
//
//         [UnitySetUp]
//         public IEnumerator Setup()
//         {
//             // Get and save the currently untitled scene
//             _devScene = SceneManager.GetActiveScene();
//             EditorSceneManager.SaveScene(_devScene, DevSceneAssetPath);
//
//             // Set Configuration
//             _config = Config.GetConfigAsset();
//
//             yield return null;
//         }
//
//         [UnityTearDown]
//         public IEnumerator TearDown()
//         {
//             AssetDatabase.DeleteAsset(DevSceneAssetPath);
//
//             yield return null;
//         }
//
//         [UnityTest, Performance]
//         public IEnumerator GuidComponentTest()
//         {
//             GuidComponent guidComponentObject;
//             Guid guid;
//             IGuidInfoBase guidInfo;
//
//             // Test GuidComponent registration
//             GameObject gameObject = new GameObject();
//             using (Measure.Scope("GuidComponent Registration"))
//             {
//                 guidComponentObject = gameObject.AddComponent<GuidComponent>();
//                 guid = guidComponentObject.Guid;
//             }
//
//             Assert.That(guid, Is.Not.EqualTo(Guid.Empty));
//
//             using (Measure.Scope("GuidComponent GuidInfo Retrieval"))
//             {
//                 guidInfo = GuidManager.GetInfo(guid);
//             }
//
//             Assert.That(guidInfo, Is.Not.Null);
//
//             // Test GuidComponent unregistration
//             using (Measure.Scope("GuidComponent Unregistration"))
//             {
//                 UnityEngine.Object.DestroyImmediate(guidComponentObject);
//                 guidInfo = GuidManager.GetInfo(guid);
//             }
//
//             Assert.That(guidInfo, Is.Null);
//
//             yield return null;
//         }
//     }
// }


