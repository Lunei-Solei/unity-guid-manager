using System.Collections;
using Manager;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Editor
{
    public class GuidManagerComponentTests
    {
        private const string ManagersSceneName = "ManagersScene";
        private const string ManagersSceneAssetPath = "Packages/com.luneisolei.guidmanager/Tests/Editor/ManagersScene.unity";
        private const string DevSceneAssetPath = "Packages/com.luneisolei.guidmanager/Tests/Editor/DevScene.unity";
        private const string GuidManagerObjectName = "GuidManagerObject";
        private Scene _managersScene;
        private Scene _devScene;
        private SceneAsset _managersSceneAsset;
        private IGuidManagerComponent _managerGameObject;

        private Config _config;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            // Get and save the current untitled scene
            _devScene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(_devScene, DevSceneAssetPath);

            // Create Managers Scene
            _managersScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
            _managersScene.name = ManagersSceneName;
            SceneManager.SetActiveScene(_managersScene);

            // Create GuidManager object
            _managerGameObject = new GameObject(GuidManagerObjectName).AddComponent<GuidManagerComponent>();
            EditorSceneManager.SaveScene(_managersScene, ManagersSceneAssetPath);
            SceneAsset managersSceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(ManagersSceneAssetPath);

            // Set Configuration
            _config = Config.GetOrCreateAsset();
            _config.SetManagersSceneAsset(managersSceneAsset);

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.DestroyImmediate(_managersSceneAsset);
            AssetDatabase.DeleteAsset(ManagersSceneAssetPath);
            AssetDatabase.DeleteAsset(DevSceneAssetPath);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuidManagerCreateTest()
        {
            // Use the Assert class to test conditions

            yield return null;
        }

        // [UnityTest]
        // public IEnumerator
    }
}
