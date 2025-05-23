using System;
using Core;
using UnityEngine;

public class ComponentGuidInfo : IGuidInfoBase, IHasGameObject
{
    public Guid Guid {get;}
    public GameObject GameObject {get;}
    public MonoBehaviour Component {get;}

    internal ComponentGuidInfo(GuidComponent target)
    {
        Guid = target.Guid;
        Component = target;
        GameObject = target.gameObject;
    }
}
