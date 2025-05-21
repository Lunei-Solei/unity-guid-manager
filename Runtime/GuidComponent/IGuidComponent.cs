using System;

namespace GuidComponent
{
    public interface IGuidComponent
    {
        protected const string PackageSettingsAssetPath =
            "Packages/com.luneisolei.guidmanager/Runtime/GuidManagerSettings.asset";

        public Guid Guid {get;}
        public Guid GetGuid();
    }
}
