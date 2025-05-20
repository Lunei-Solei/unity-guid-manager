using System;
using System.Collections.Generic;
using GuidInfo;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Manager
{
    public static class GuidManagerUtility
    {
        internal static Guid AddToMap(Dictionary<Guid, IGuidInfo> guidToInfoMap, IGuidInfo targetInfo,
            MonoBehaviour context)
        {
            GameObject gameObject = context.gameObject;
            guidToInfoMap.TryGetValue(targetInfo.SystemGuid, out IGuidInfo info);
            if (info != null) return targetInfo.SystemGuid;

#if UNITY_EDITOR
            if (IsAssetOnDisk(gameObject)) return Guid.Empty;

            Undo.RecordObject(context, "Registered GUID");

            bool isPartOfModifiedPrefabInstance = PrefabUtility.IsPartOfPrefabInstance(gameObject);
            if (isPartOfModifiedPrefabInstance) PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
#endif

            // GUID is not registered. Assign a new one
            Guid systemGuid = Guid.NewGuid();

            return guidToInfoMap.TryAdd(systemGuid, targetInfo) ? systemGuid : Guid.Empty;
        }

#if UNITY_EDITOR
        private static bool IsEditingInPrefabMode(GameObject target)
        {
            if (EditorUtility.IsPersistent(target)) return true;

            StageHandle mainStage = StageUtility.GetMainStageHandle();
            StageHandle currentStage = StageUtility.GetCurrentStageHandle();
            PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(target);
            bool isPartOfPrefabAsset = PrefabUtility.IsPartOfPrefabAsset(target);

            return currentStage != mainStage && prefabStage || isPartOfPrefabAsset;
        }

        internal static bool IsAssetOnDisk(GameObject target)
        {
            return PrefabUtility.IsPartOfPrefabAsset(target) || IsEditingInPrefabMode(target);
        }
#endif
    }
}
