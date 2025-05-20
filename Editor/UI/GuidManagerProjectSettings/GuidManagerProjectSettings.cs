using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.UI.GuidManagerProjectSettings
{
    public class GuidManagerProjectSettings : SettingsProvider
    {
        private Config _configAsset;
        private SerializedObject _serializedConfig;
        private SerializedProperty _guidManagerProperty;

        private const string ProjectSettingsPath = "Project/Lunei Solei Tools/Guid Manager Package";
        private const string ConfigPanelDirectory = Config.PackageEditorUIDirectory + "GuidManagerProjectSettings/";
        private const string UxmlName = "GuidManagerProjectSettings.uxml";
        private const string UssName = "GuidManagerProjectSettings.uss";

        private const string GuidManagerFieldId = "guidManagerField";
        private const string GuidManagerPropertyName = "guidManager";

        private const string ManagersSceneAssetFieldId = "managersSceneAssetField";
        private const string ManagersSceneAssetPropertyName = "managersSceneAsset";


        private GuidManagerProjectSettings(string path, SettingsScope scope = SettingsScope.Project) : base(path, scope) { }

        [SettingsProvider]
        public static SettingsProvider CreateGuidManagerSettingsProvider()
        {
            return new GuidManagerProjectSettings(ProjectSettingsPath);
        }

        public override void OnActivate(string searchContext, VisualElement root)
        {
            // Setup template
            InitializeTemplate(root);

            // Setup variables
            _configAsset = Config.GetOrCreateAsset();
            _serializedConfig = new SerializedObject(_configAsset);
            _guidManagerProperty = _serializedConfig.FindProperty(GuidManagerPropertyName);

            // Set up object field for Manager SceneAsset
            ObjectField managersSceneAssetField = root.Q<ObjectField>(ManagersSceneAssetFieldId);
            SceneAsset managersSceneAsset = _configAsset.GetManagersSceneAsset();
            managersSceneAssetField.RegisterValueChangedCallback(OnManagersSceneAssetChanged);
            if (managersSceneAsset) managersSceneAssetField.value = managersSceneAsset;

            // Set up object field for GuidManager GameObject
            ObjectField guidManagerField = root.Q<ObjectField>(GuidManagerFieldId);
            guidManagerField.SetEnabled(managersSceneAsset);
            // guidManagerField.value = _configAsset.GetGuidManagerObject(root, managersSceneAsset);
            // guidManagerField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(OnGuidManagerObjectChanged);
        }

        private static void InitializeTemplate(VisualElement root)
        {
            VisualTreeAsset uxmlAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ConfigPanelDirectory + UxmlName);
            StyleSheet ussAsset = AssetDatabase.LoadAssetAtPath<StyleSheet>(ConfigPanelDirectory + UssName);

            root.Add(uxmlAsset.Instantiate());
            root.styleSheets.Add(ussAsset);
        }

        private void OnManagersSceneAssetChanged(ChangeEvent<Object> evt)
        {
            _configAsset.SetManagersSceneAsset(evt.newValue as SceneAsset);
        }
    }
}
