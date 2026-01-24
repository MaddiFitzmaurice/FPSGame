#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoLoadScenesEditor
{
    public const string CoreSceneName = "Core";
    public const string CorePath = "Assets/Scenes/Core.unity";

    public const string GameplaySceneName = "Gameplay";
    public const string GameplayPath = "Assets/Scenes/Core/Gameplay.unity";

    public const string MainMenuSceneName = "MainMenu";
    public const string MainMenuPath = "Assets/Scenes/MainMenu.unity";

    static AutoLoadScenesEditor()
    {
        EditorSceneManager.sceneOpened += LoadCoreScene;
        EditorSceneManager.sceneOpened += LoadGameplayScene;
    }

    // Automatically load Core scene if Main Menu or Levels has been loaded as a single
    static void LoadCoreScene(Scene scene, OpenSceneMode mode)
    {
        // Make sure additive scene loading hasn't happened or Core scene hasn't been loaded in
        if (mode == OpenSceneMode.Single && scene.name != CoreSceneName)
        {
            Scene core = EditorSceneManager.GetSceneByPath(CorePath);

            // Check if Core scene hasn't already been loaded in before
            if (!core.isLoaded)
            {
                core = EditorSceneManager.OpenScene(CorePath, OpenSceneMode.Additive);
            }

            // Move to top of hierarchy
            EditorSceneManager.MoveSceneBefore(core, scene);
        }
    }

    // Automatically load the Gameplay scene if any Level scenes have been loaded as a single
    static void LoadGameplayScene(Scene scene, OpenSceneMode mode)
    {
        // Make sure additive scene loading hasn't happened or Core/Gameplay/MainMenu hasn't just been loaded in
        if (mode == OpenSceneMode.Single && scene.name != GameplaySceneName && scene.name != MainMenuSceneName && scene.name != CoreSceneName)
        {
            Scene gameplay = EditorSceneManager.GetSceneByPath(GameplayPath);

            // Check if Gameplay hasn't already been loaded in before
            if (!gameplay.isLoaded)
            {
                gameplay = EditorSceneManager.OpenScene(GameplayPath, OpenSceneMode.Additive);
            }

            // Move to top of hierarchy
            EditorSceneManager.MoveSceneBefore(gameplay, scene);
        }
    }
}
#endif