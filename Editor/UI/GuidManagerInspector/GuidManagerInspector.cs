using Manager;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.UI.GuidManagerInspector
{
    [CustomEditor(typeof(GuidManagerComponent))]
    public class GuidManagerInspector : UnityEditor.Editor
    {
        [SerializeField]
        private GuidManagerComponent guidServiceProvider;
        private IGuidManagerComponent _guidService;

        private string _currentPath;

        public override VisualElement CreateInspectorGUI()
        {
            FindGuidService();
            VisualElement root = LoadTemplate();

            return root;

            void FindGuidService()
            {
                // Find and cache the IGuidService provider
                if (guidServiceProvider != null && guidServiceProvider is IGuidManagerComponent service)
                {
                    _guidService = service;
                }
            }

            VisualElement LoadTemplate()
            {
                // Load the template and apply the USS
                const string currentDirectory = "Packages/com.luneisolei.guidmanager/Editor/GuidManagerInspector/";

                VisualTreeAsset uxmlAsset = AssetDatabase
                    .LoadAssetAtPath<VisualTreeAsset>(currentDirectory + "GuidManagerInspectorTemplate.uxml");

                StyleSheet styleSheetAsset =
                    AssetDatabase.LoadAssetAtPath<StyleSheet>(currentDirectory + "/GuidManagerInspectorStyleSheet.uss");

                root = uxmlAsset.Instantiate();
                root.styleSheets.Add(styleSheetAsset);

                return root;
            }
        }
    }
}
