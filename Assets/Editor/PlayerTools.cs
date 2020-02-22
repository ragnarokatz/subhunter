#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

namespace Editor
{
    public partial class EditorTools
    {
        #if UNITY_EDITOR
        [MenuItem("Tools/Clear Highscore")]
        public static void ClearHighscore()
        {
            ObscuredPrefs.SetInt("high_score", 0);
        }
        #endif
    }
}