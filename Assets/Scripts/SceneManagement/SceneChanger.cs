using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Home,
    StoryModeSelection,
    Plains,
}

public static class SceneChanger
{
    public static void GoToScene(Scenes scene)
    {
        SceneManager.LoadScene(EnumToScene[scene]);
    }

    public static void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static Dictionary<Scenes, string> EnumToScene = new Dictionary<Scenes, string>
    {
        { Scenes.Home, "Scenes/Selection/Home" },
        { Scenes.StoryModeSelection, "Scenes/Selection/StoryModeSelection" },
        { Scenes.Plains, "Scenes/StoryMode/Plains" },
    };
}
