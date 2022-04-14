using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControllerBehavior : MonoBehaviour
{
    [SerializeField]
    private Coordinates _pathStartLocation;

    [SerializeField]
    public GameObject TileSelectionScreen;

    void Start()
    {
        LevelController.ControllerScript = this;
        LevelController.PathCurrentPlayerLocation = _pathStartLocation;
        LevelController.EnterLevel();
        LevelController.GetTile(_pathStartLocation).CreatePath();
    }

    void Update()
    {
        Keys();
    }

    private void Keys()
    {
        if (LevelController.Phase == LevelPhase.Pathmaking)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                LevelController.GetTile(LevelController.PathCurrentPlayerLocation + (-1, 0)).CreatePathIfPossible();
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LevelController.GetTile(LevelController.PathCurrentPlayerLocation + (0, -1)).CreatePathIfPossible();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                LevelController.GetTile(LevelController.PathCurrentPlayerLocation + (1, 0)).CreatePathIfPossible();
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                LevelController.GetTile(LevelController.PathCurrentPlayerLocation + (0, 1)).CreatePathIfPossible();
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                SceneChanger.Reload();
            }
        }
    }

    public IEnumerator WaitSeconds(float wait, Action waitFunction)
    {
        yield return new WaitForSecondsRealtime(wait);
        waitFunction();
    }
}
