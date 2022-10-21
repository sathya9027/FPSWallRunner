using UnityEditor;
using UnityEngine;
using TMPro;
using TamilUI;

namespace TamilUIEditor
{
    [CustomEditor(typeof(TamilTextMeshPro), true)]
    public class TamilTextMeshProEditor : Editor
    {
        SerializedProperty textBox;
        SerializedProperty font;

        private void OnEnable()
        {
            textBox = serializedObject.FindProperty("m_Text");
            font = serializedObject.FindProperty("m_FontAsset");
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            serializedObject.Update();

            EditorGUILayout.PropertyField(textBox, new GUIContent("Text Box", "Type your Tamil text here.It will Update automatical in play mode.leaving it empty to stop overriding text"));
            EditorGUILayout.PropertyField(font, new GUIContent("TSCII font", "Add TSCII Text Mesh Pro Font Asset"));

            TamilTextMeshPro tamilTextMeshPro = (TamilTextMeshPro)target;
            if (GUILayout.Button("Render Text"))
            {
                var text = tamilTextMeshPro.GetComponent<TextMeshProUGUI>();
                Undo.RecordObject(text, "text changed");
                tamilTextMeshPro.UpdateText();
                EditorUtility.SetDirty(text);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}