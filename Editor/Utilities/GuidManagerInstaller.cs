using Core;
using UnityEditor;
using UnityEditor.PackageManager;

namespace Editor.Utilities
{
    [InitializeOnLoad]
    public class GuidManagerInstaller
    {
        static GuidManagerInstaller()
        {
            Events.registeringPackages += GuidManagerUninstaller.Uninstaller;

            if (EditorPrefs.GetBool(Config.PackageInitializedKey)) return;

            bool shouldMigrateNow = EditorUtility.DisplayDialog(
                "GUID Manager Package Installed",
                "Would you like to register existing assets with the GUID Manager now?",
                "Yes", "Later");

            if (shouldMigrateNow)
            {
                GuidManagerMigrationUtility.MigrateExistingAssets();
            }

            EditorPrefs.SetBool(Config.PackageInitializedKey, true);
        }
    }
}
