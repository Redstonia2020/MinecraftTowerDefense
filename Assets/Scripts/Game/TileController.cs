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
    private GameObject _selectedIndicator;
    [SerializeField]
    private GameObject _pathIndicatorRenderer;
    [SerializeField]
    private GameObject _endOfPathIndicator;

    [SerializeField]
    private bool _endOfPath;

    [HideInInspector]
    public bool IsPath = false;

    void Start()
    {
        if (_endOfPath)
        {
            _endOfPathIndicator.SetActive(true);
            LevelController.EndOfPathTiles.Add(this);
        }
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

        else if (LevelController.Phase == LevelPhase.BetweenWaves || LevelController.Phase == LevelPhase.Wave)
        {
            InteractPlacement();
        }
    }

    private void InteractPlacement()
    {
        LevelController.ChangeTower(this);
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

        if (!_endOfPath)
        {
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

        else
        {
            LevelController.EndPathing();
        }
    }

    public void SetActiveSelectedIndicator(bool active)
    {
        _selectedIndicator.SetActive(active);
    }

    public void HideEndOfPathIndicator()
    {
        _endOfPathIndicator.SetActive(false);
    }
}
