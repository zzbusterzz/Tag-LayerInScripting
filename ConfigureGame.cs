using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class ConfigureGame
{
	static ConfigureGame ()
	{
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        AddTag("Tag1", tagManager);
        AddTag("Tag2", tagManager);
    }

    static void AddTag(string tag, SerializedObject tagManager)
    {   
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        
        // First check if it is not already present
        bool found = false;
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(tag)) { found = true; break; }
        }

        // if not found, add it
        if (!found)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
            n.stringValue = tag;
        }
        else
        {
            Debug.Log("Tag is present");
        }

        tagManager.ApplyModifiedProperties();
    }

    static void AddLayer(string layer, SerializedObject tagManager)
    {
        // For Unity 5 we need this too
        SerializedProperty layersProp = tagManager.FindProperty("layers");

#if UNITY_4
        SerializedProperty sp = tagManager.FindProperty("User Layer 10");
        if (sp != null) sp.stringValue = layerName;
#else
        // --- Unity 5 ---
        SerializedProperty sp = layersProp.GetArrayElementAtIndex(10);
        if (sp != null) sp.stringValue = layer;
#endif
        // and to save the changes
        tagManager.ApplyModifiedProperties();
    }
}