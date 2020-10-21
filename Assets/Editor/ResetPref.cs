using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetPref : EditorWindow
{
    [MenuItem("Edit/Reset Playerprefs")]

    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
