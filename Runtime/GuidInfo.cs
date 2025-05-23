using System;
using UnityEngine;

public class GuidInfo
{
    public GameObject GameObject {get; private set;}
    public GuidComponent GuidComponent {get; private set;}

    public GuidInfo(GuidComponent target)
    {
        GuidComponent = target;
        GameObject = target.gameObject;
    }
}
