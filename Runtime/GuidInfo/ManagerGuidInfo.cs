using System;

public class ManagerGuidInfo : IGuidInfoBase
{
    public Guid Guid {get;}

    public ManagerGuidInfo()
    {
        Guid = GuidManager.Guid;
    }
}
