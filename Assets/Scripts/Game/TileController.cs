using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    public bool Unpathable;
    [SerializeField]
    private GameObject _pathRenderer;
    [SerializeField]
    private GameObject _pathIndicatorRenderer;

    [HideInInspector]
    public bool IsPath = false;

    void Start()
    {
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        var coords = LevelController.GetTilePosition(this);

        if (LevelController.Phase == LevelPhase.Pathmaking)
        {
            CreatePathIfPossible();
        }
    }

    public void CreatePathIfPossible()
    {
        if (LevelController.IsPathable(LevelController.GetTilePosition(this)) && !IsPath && !Unpathable)
        {
            CreatePath();
        }
    }

    public void CreatePath()
    {
        _pathRenderer.SetActive(true);
        IsPath = true;
        LevelController.PathCurrentPlayerLocation = LevelController.GetTilePosition(this);

        if (LevelController.PathableTiles.Count > 0)
        {
            foreach (var path in LevelController.PathableTiles)
            {
                LevelController.GetTile(path)._pathIndicatorRenderer.SetActive(false);
            }
        }

        LevelController.UpdatePathables();

        foreach (var path in LevelController.PathableTiles)
        {
            TileController tile = LevelController.GetTile(path);
            if (tile != null)
            {
                tile._pathIndicatorRenderer.SetActive(true);
            }
        }
    }
}
