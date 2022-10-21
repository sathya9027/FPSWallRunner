using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TamilUI;

namespace TamilUIEditor
{
    [CustomEditor(typeof(TamilText), true)]
    public class TamilTextEditor : Editor
    {
        SerializedProperty text;
        SerializedProperty font;
        SerializedProperty fontSize;
        SerializedProperty lineSpacing;


        protected virtual void OnEnable()
        {
            text = serializedObject.FindProperty("m_Text");
            font = serializedObject.FindProperty("m_TSCII_TamilFont");
            fontSize = serializedObject.FindProperty("fontSize");
            lineSpacing = serializedObject.FindProperty("lineSpacing");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawValues();
            serializedObject.ApplyModifiedProperties();

            TamilText tamilText = (TamilText)target;
            if (GUILayout.Button("Render Text"))
            {
                Text text = tamilText.GetComponent<Text>();
                Undo.RecordObject(text, "text changed");
                tamilText.RenderText();
                EditorUtility.SetDirty(text);
            }
        }

        protected virtual void DrawValues()
        {
            EditorGUILayout.PropertyField(text, new GUIContent("Text Box", "Enter Tamil Text,These text will be replaced in text UI Component.Leaving it empty can stop overriding text."));
            EditorGUILayout.PropertyField(font, new GUIContent("TSCII font", "Only TSCII fonts are supported! Don't use unicode fonts."));
            EditorGUILayout.PropertyField(fontSize, new GUIContent("Font Size", "We can override font size.Leaving it 0 can stop overriding."));
            EditorGUILayout.PropertyField(lineSpacing, new GUIContent("Line Spacing", "Override line spacing in Text UI"));
        }
    }
}