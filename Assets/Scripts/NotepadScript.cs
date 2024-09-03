//using System.Collections; <- Required for unknown reasons, but it's heavily discouraged to delete unless Visual Studio says otherwise.
//using UnityEditor; <- Required for most Unity-related functions of the Editor.
//using UnityEngine; <- Required for most Unity-related functions.

//[CustomEditor(typeof(scriptname))] // Must need this for this function to work.
//[CanEditMultipleObjects] // Must apply this to avoid the "cannot edit multiple objects dialog in the 
//public class scriptnameeditor : Editor
//{
//public override void OnInspectorGUI()
//{
// Draw the default Inspector
//DrawDefaultInspector();

// Get a reference to the script
//scriptname itemname = (scriptname)target;

// Add checking logic to add for the Inspector, like warnings, errors, etc.
//EditorGUILayout.HelpBox("There must be at least one prefab object in the drop list for the script to work properly!", MessageType.Warning);

//}
//}

//using System.Collections.Generic; <- Required for more complex functions, like serializable items.

//[System.Serializable] <- Required for it to show up in Unity's Inspector and able to draw it.
//public class classname
//{
//    public datatype (int, bool, double, float, etc.) dataname; <- Required to properly display a value in-Editor, albeit it's recommended for it to be the name.
//    public datatype (int, bool, double, float, etc.) datavalue; <- Required to display the name's value in-Editor.
//}

//public class scriptname : MonoBehaviour <- This must go after the class declaration from earlier.
//{
//    public List<classname> animatorParams = new List<classname>(); <- EXTREMELY NEEDED for it to work properly, it MUST be a list, as the class function takes into consideration only Lists, for now.
//}
