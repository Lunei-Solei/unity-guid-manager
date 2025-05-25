using UnityEngine;

public interface IGuidInfo
{
    public GameObject GameObject {get;}
    public GuidComponent GuidComponent {get;}
    public GuidManager.GuidType GuidInfoType {get;}
}