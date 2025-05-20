using System.IO;
using Manager;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.UI.GuidComponentInspector
{
    [CustomEditor(typeof(GuidComponent.GuidComponent))]
    public class GuidComponentInspector : UnityEditor.Editor
    {
        [SerializeField]
        private MonoBehaviour guidServiceProvider;
        public MonoBehaviour GuidServiceProvider
        {
            get => guidServiceProvider;
            set => guidServiceProvider = value;
        }

        private IGuidManagerComponent _guidService;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = SetupRoot();
            Label guidField = root.Q<Label>("guidField");
            GuidComponent.GuidComponent guidComponent = (GuidComponent.GuidComponent)target;
            guidField.text = guidComponent.GetGuid().ToString();

            root.Bind(serializedObject);

            Button refreshButton = new Button { text = "Refresh" };
            refreshButton.clicked += RefreshButtonClicked;

            return root;

            VisualElement SetupRoot()
            {
                string currentPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                currentPath = Path.GetDirectoryName(currentPath) + "/GuidComponentInspectorTemplate.uxml";
                VisualTreeAsset targetUxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(currentPath);

                return targetUxml.Instantiate();
            }
        }

        private void RefreshButtonClicked()
        {

        }
    }
}
