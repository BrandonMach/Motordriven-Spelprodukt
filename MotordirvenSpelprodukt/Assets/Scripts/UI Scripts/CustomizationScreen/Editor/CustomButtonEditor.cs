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

        targetCustomButton.TMP = (TextMeshProUGUI)EditorGUILayout.ObjectField("TMP:", targetCustomButton.TMP, typeof(TextMeshProUGUI), true);
        targetCustomButton.Challenge = (Challenge)EditorGUILayout.ObjectField("Challenge:", targetCustomButton.Challenge, typeof(Challenge), true);

        base.OnInspectorGUI();
    }
}
