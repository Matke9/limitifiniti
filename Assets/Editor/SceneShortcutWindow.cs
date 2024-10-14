using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneShortcutWindow : EditorWindow
{
    [MenuItem("Tools/Scene Shortcuts")]
    public static void ShowWindow()
    {
        GetWindow<SceneShortcutWindow>("Scene Shortcuts");
    }

    private void OnGUI()
    {
        GUILayout.Label("Scene Shortcuts", EditorStyles.boldLabel);

        // Get all the scenes in the project
        var scenes = EditorBuildSettings.scenes;

        foreach (var scene in scenes)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);

            // Display a button for each scene
            if (GUILayout.Button(sceneName))
            {
                // Load the scene when clicked
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scene.path);
                }
            }
        }
    }
}

