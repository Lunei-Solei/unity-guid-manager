# Guid Manager Documentation

## Table of Contents

- [How It Works](#how-it-works)
- [Types](#types)
- [GuidManager](#guidmanager)
- [GuidInfo](#guidinfo)
- [IGuidService](#iguidservice)
- [GuidComponent](#guidcomponent)
- [GuidComponentSO](#guidcomponentso)
- [GuidManagerReferenceSO](#guidmanagerreferenceso)

## How It Works
This package contains: GuidComponent and GuidManager

## Types

- GuidManager
- GuidInfo
- IGuidService
- GuidComponent
- GuidComponentSO
- GuidManagerReferenceSO

## GuidManager

``public class GuidManager : MonoBehaviour, IGuidService``

Responsible for the storing the GUIDs and their mapped GuidInfo pieces.

<table>
<tr>
    <td><b>Method</b></td>
    <td><b>Arguments</b></td>
    <td><b>Description</b></td>
</tr>
<tr>
    <td>RegisterGuid()</td>
    <td>GuidComponent</td>
    <td>Adds a newly generated GUID to the manager.</td>
</tr>
<tr>
    <td>UnregisterGuid()</td>
    <td>GuidComponent</td>
    <td>Removes a GUID from the manager.</td>
</tr>
</table>

## GuidInfo

``public class GuidInfo {...}``

## IGuidService

``public interface IGuidService {...}``

## GuidComponent

``public class GuidComponent : MonoBehaviour, ISerializationCallbackReceiver {...}``


## GuidComponentSO

``public class GuidComponentConfig : ScriptableObject {...}``

## GuidManagerReferenceSO

``public class GuidManagerReferenceSO : ScriptableObject {...}``