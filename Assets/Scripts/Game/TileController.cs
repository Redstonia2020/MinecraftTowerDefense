using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    private bool _unpathable;
    [SerializeField]
    private GameObject _pathRenderer;

    [HideInInspector]
    public bool IsPath = false;

    void Start()
    {
        LevelController.EnterLevel();
    }

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        var coords = LevelController.GetTilePosition(this);
        Debug.Log($"{coords.Row} {coords.Column}");

        if (LevelController.Phase == LevelPhase.Pathmaking)
        {
            CreatePathIfPossible();
        }
    }

    private void CreatePathIfPossible()
    {
        if (LevelController.IsPathable(LevelController.GetTilePosition(this)) && !IsPath && !_unpathable)
        {
            CreatePath();

        }
    }

    public void CreatePath()
    {
        _pathRenderer.SetActive(true);
        IsPath = true;
        LevelController.PathCurrentPlayerLocation = LevelController.GetTilePosition(this);
    }
}
