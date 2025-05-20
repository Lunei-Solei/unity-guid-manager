using System;

namespace GuidComponent
{
    public interface IGuidComponent
    {
        protected const string PackageSettingsAssetPath =
            "Packages/com.luneisolei.guidmanager/Runtime/GuidManagerSettings.asset";

        public Guid SystemGuid {get;}
        public Guid GetGuid();
    }
}
