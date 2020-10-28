#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;

namespace GameWorkstore.GoogleSignIn
{
    public class PackageUpdater
    {
        [MenuItem("Help/PackageUpdate/GameWorkstore.GoogleSignIn")]
        public static void TrackPackages()
        {
            Client.Add("git://github.com/GameWorkstore/googleplaysignin.git");
        }
    }
}
#endif