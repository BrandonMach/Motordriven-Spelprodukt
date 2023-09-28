using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using TMPro;

[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{
    // Makes it possible to add extra fields to already existing UI components
    public override void OnInspectorGUI()
    {
        CustomButton targetCustomButton = (CustomButton)target;

        targetCustomButton.Text = (TextMeshProUGUI)EditorGUILayout.ObjectField("Text:", targetCustomButton.Text, typeof(TextMeshProUGUI), true); 

        base.OnInspectorGUI();
    }
}
