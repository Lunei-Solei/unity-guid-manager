using System;

public interface IGuidInfo
{
    public Guid Guid {get;}
    public void UpdateGuid(Guid guid);
}
