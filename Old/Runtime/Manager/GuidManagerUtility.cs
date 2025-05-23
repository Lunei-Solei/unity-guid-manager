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
        if (targetInfo.Guid != Guid.Empty)
        {
            guidToInfoMap.TryGetValue(targetInfo.Guid, out T info);
            if (info != null) return info.Guid;
        }

#if UNITY_EDITOR
        // Make sure the GUID is not for something that is in a prefab context
        GameObject gameObject = targetInfo is IHasGameObject hasGameObject ? hasGameObject.GameObject : null;
        if (gameObject && IsInPrefabContext(gameObject)) return Guid.Empty;
#endif

        Guid guid = GenerateUniqueGuid();

#if UNITY_EDITOR
        if (gameObject)
        {
            Undo.RecordObject(config, "Registered GUID");

            bool isPartOfModifiedPrefabInstance = PrefabUtility.IsPartOfPrefabInstance(gameObject);
            if (isPartOfModifiedPrefabInstance) PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
        }
#endif
        guidToInfoMap.TryAdd(guid, targetInfo);

        return guid;
    }

    internal static Guid GenerateUniqueGuid()
    {
        Guid guid = Guid.NewGuid();
        while (GuidManager.GuidToInfoMap.ContainsKey(guid)) guid = Guid.NewGuid();

        return guid;
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

    internal static bool IsInPrefabContext(GameObject target) =>
        PrefabUtility.IsPartOfPrefabAsset(target) || IsEditingInPrefabMode(target);
#endif
}
