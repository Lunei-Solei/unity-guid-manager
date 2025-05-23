// using UnityEditor;
// using UnityEngine.UIElements;
//
// namespace Old.Editor.UI.GuidManagerProjectSettings
// {
//     public class GuidManagerProjectSettings : SettingsProvider
//     {
//         private Config _configAsset;
//         private SerializedObject _serializedConfig;
//         private const string ProjectSettingsPath = "Project/Lunei Solei Tools/Guid Manager Package";
//         private const string ConfigPanelDirectory = Config.PackageEditorUIDirectory + "GuidManagerProjectSettings/";
//         private const string UxmlName = "GuidManagerProjectSettings.uxml";
//         private const string UssName = "GuidManagerProjectSettings.uss";
//
//         private GuidManagerProjectSettings(string path, SettingsScope scope = SettingsScope.Project) : base(path, scope) { }
//
//         [SettingsProvider]
//         public static SettingsProvider CreateGuidManagerSettingsProvider() =>
//             new GuidManagerProjectSettings(ProjectSettingsPath);
//
//         public override void OnActivate(string searchContext, VisualElement root)
//         {
//             // Setup template
//             InitializeTemplate(root);
//
//             // Setup variables
//             _configAsset = Config.GetConfigAsset();
//             _serializedConfig = new SerializedObject(_configAsset);
//         }
//
//         private static void InitializeTemplate(VisualElement root)
//         {
//             VisualTreeAsset uxmlAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ConfigPanelDirectory + UxmlName);
//             StyleSheet ussAsset = AssetDatabase.LoadAssetAtPath<StyleSheet>(ConfigPanelDirectory + UssName);
//
//             root.Add(uxmlAsset.Instantiate());
//             root.styleSheets.Add(ussAsset);
//         }
//     }
// }


