using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerBehavior : MonoBehaviour
{
    [SerializeField]
    private Coordinates _pathStartLocation;

    void Start()
    {
        LevelController.ControllerScript = this;
        LevelController.EnterLevel();
        LevelController.PathCurrentPlayerLocation = _pathStartLocation;
        LevelController.GetTile(_pathStartLocation).CreatePath();
    }

    void Update()
    {
        
    }
}
