using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("LuneiSolei.GuidManager.Editor")]

namespace Core
{
    public class Config : ScriptableObject
    {
        internal const string PackageName = "com.luneisolei.guidmanager";
        internal const string PackageInitializedKey = "LuneiSolei.GuidManager.Initialized";
        internal const string PackageEditorUIDirectory = "Packages/com.luneisolei.guidmanager/Editor/UI/";
        private const string PackageConfigAsset = "Packages/com.luneisolei.guidmanager/Runtime/Config.asset";

        private static Config s_configAsset;

        [SerializeField]
        private byte[] serializedGuid;
        public Guid ManagerGuid
        {
            get
            {
                // TODO: byte[].ToString() returns "System.Byte[]"
                Guid.TryParse(serializedGuid?.ToString(), out Guid guid);

                return guid;
            }
            internal set => serializedGuid = value.ToByteArray();
        }

        public static Config GetConfigAsset()
        {
            if (s_configAsset) return s_configAsset;

            s_configAsset = AssetDatabase.LoadAssetAtPath<Config>(PackageConfigAsset);

            if (s_configAsset) return s_configAsset;

            s_configAsset = CreateInstance<Config>();
            AssetDatabase.CreateAsset(s_configAsset, PackageConfigAsset);
            AssetDatabase.SaveAssets();

            return s_configAsset;
        }
    }
}
