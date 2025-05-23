using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Old.Editor.UI.GuidComponentInspector
{
    [CustomEditor(typeof(GuidComponent))]
    public class GuidComponentInspector : UnityEditor.Editor
    {
        [SerializeField]
        private MonoBehaviour guidServiceProvider;
        public MonoBehaviour GuidServiceProvider
        {
            get => guidServiceProvider;
            set => guidServiceProvider = value;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = SetupRoot();
            Label guidField = root.Q<Label>("guidField");
            GuidComponent guidComponent = (GuidComponent)target;
            guidField.text = guidComponent.Guid.ToString();

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
