#if UNITY_EDITOR
using AtanUtils.UI.Controls;
using UnityEditor;
using UnityEngine;

namespace AtanUtils.UI.Editor
{
    [CustomEditor(typeof(ButtonControl), true)]
    public class ButtonEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledScope(serializedObject.isEditingMultipleObjects && targets.Length == 0))
            {
                if (GUILayout.Button("Update Palette"))
                {
                    foreach (var obj in targets)
                    {
                        var comp = obj as ButtonControl;
                        if (comp == null)
                            continue;

                        Undo.RecordObject(comp, "Update Palette");
                        
                        comp.UpdatePalette();
                        EditorUtility.SetDirty(comp);
                    }
                }
            }
        }
    }
}
#endif