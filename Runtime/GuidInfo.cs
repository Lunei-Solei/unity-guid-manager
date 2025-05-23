using System;
using UnityEngine;

public class GuidInfo
{
    public GameObject GameObject {get; private set;}
    public GuidComponent GuidComponent {get; private set;}
    public GuidManager.GuidType GuidInfoType {get; private set;}

    public GuidInfo(GuidComponent target)
    {
        GuidInfoType = GuidManager.GuidType.Component;
        GuidComponent = target;
        GameObject = target.gameObject;
    }
}
