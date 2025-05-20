using UnityEditor;
using UnityEditor.PackageManager;

namespace Editor.Utilities
{
    public static class GuidManagerUninstaller
    {
        internal static void Uninstaller(PackageRegistrationEventArgs eventArgs)
        {
            if (eventArgs.removed.Count == 0) return;

            foreach (UnityEditor.PackageManager.PackageInfo package in eventArgs.removed)
            {
                if (package.name != Config.PackageName) continue;

                EditorPrefs.DeleteKey(Config.PackageInitializedKey);
            }
        }
    }
}
