using System;
using System.Collections.Generic;
using Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

internal static class GuidManagerUtility
{
    private static readonly Config config = Config.GetConfigAsset();

    internal static Guid AddToMap<T>(Dictionary<Guid, T> guidToInfoMap, T targetInfo) where T : IGuidInfoBase
    {
        guidToInfoMap.TryGetValue(targetInfo.Guid, out T info);
        if (info != null) return targetInfo.Guid;

#if UNITY_EDITOR
        GameObject gameObject = targetInfo is IHasGameObject hasGameObject ? hasGameObject.GameObject : null;
        if (gameObject)
        {
            if (IsAssetOnDisk(gameObject)) return Guid.Empty;

            Undo.RecordObject(config, "Registered GUID");

            bool isPartOfModifiedPrefabInstance = PrefabUtility.IsPartOfPrefabInstance(gameObject);
            if (isPartOfModifiedPrefabInstance) PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
        }
#endif

        // GUID is not registered. Assign a new one
        Guid guid = Guid.NewGuid();

        return guidToInfoMap.TryAdd(guid, targetInfo) ? guid : Guid.Empty;
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

    internal static bool IsAssetOnDisk(GameObject target) =>
        PrefabUtility.IsPartOfPrefabAsset(target) || IsEditingInPrefabMode(target);
#endif
}
