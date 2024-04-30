using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Object_spawner))]
public class object_spawner_editor : Editor
{
    SerializedProperty gameObjectArray, numberOfObjects;

    void OnEnable()
    {
        gameObjectArray = serializedObject.FindProperty("objectsToPlace");
        numberOfObjects = serializedObject.FindProperty("numberOfObjects");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var obj = target as Object_spawner;

        EditorGUILayout.PropertyField(gameObjectArray, true);
        EditorGUILayout.PropertyField(numberOfObjects, true);

        if (GUILayout.Button("Randomly Place Object"))
        {
            obj.PlaceObjects();
        }

        if (GUILayout.Button("Delete Placed Object"))
        {
            obj.ClearDestroy();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
