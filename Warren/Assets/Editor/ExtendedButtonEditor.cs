using UnityEditor.UI;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ExtendedButton), editorForChildClasses: true)]
[CanEditMultipleObjects]
public class ExtendedButtonEditor : ButtonEditor
{
    private SerializedProperty onHoverEnter;
    private SerializedProperty onHoverExit;

    protected override void OnEnable()
    {
        base.OnEnable();

        onHoverEnter = serializedObject.FindProperty("OnHoverEnter");
        onHoverExit = serializedObject.FindProperty("OnHoverExit");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Extended Button fields");
        EditorGUILayout.PropertyField(onHoverEnter);
        EditorGUILayout.PropertyField(onHoverExit);

        serializedObject.ApplyModifiedProperties();
    }
}
