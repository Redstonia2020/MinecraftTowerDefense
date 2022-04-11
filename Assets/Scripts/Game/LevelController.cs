using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum LevelPhase
{
    None,
    Pathmaking,
    BetweenWaves,
    Wave,
}

public static class LevelController
{
    public static LevelControllerBehavior ControllerScript;

    public static LevelPhase Phase = LevelPhase.None;
    public static List<List<TileController>> Tiles = new List<List<TileController>>();

    public static Coordinates PathCurrentPlayerLocation;
    public static List<Coordinates> PathRoute = new List<Coordinates>();
    public static List<Coordinates> PathableTiles = new List<Coordinates>();

    public static List<TileController> EndOfPathTiles = new List<TileController>();

    public static void EnterLevel()
    {
        Phase = LevelPhase.Pathmaking;
        foreach (Transform row in GameObject.FindGameObjectWithTag("level").transform)
        {
            TileController[] tilesInRow = row.GetComponentsInChildren<TileController>();
            Tiles.Add(tilesInRow.ToList());
        }
    }

    public static void BetweenWaves(float waitSeconds = 10)
    {
        Phase = LevelPhase.BetweenWaves;
        Wait(waitSeconds, StartWave);
    }

    public static void EndPathing()
    {
        Phase = LevelPhase.BetweenWaves;
        foreach (var tile in EndOfPathTiles)
        {
            tile.HideEndOfPathIndicator();
        }

        BetweenWaves(30);
    }

    public static void StartWave()
    {
        Phase = LevelPhase.Wave;
    }

    public static void Wait(float seconds, Action waitFunction)
    {
        ControllerScript.StartCoroutine(ControllerScript.WaitSeconds(seconds, waitFunction));
    }

    public static TileController GetTile(Coordinates c)
    {
        if (c.Row >= 0 && c.Column >= 0 && c.Row < Tiles.Count && c.Column < Tiles[0].Count)
        {
            return Tiles[c.Row][c.Column];
        }

        else
        {
            return null;
        }
    }

    public static Coordinates GetTilePosition(TileController tile)
    {
        for (int row = 0; row < Tiles.Count; row++)
        {
            List<TileController> tilerow = Tiles[row];
            for (int column = 0; column < tilerow.Count; column++)
            {
                if (tilerow[column] == tile)
                {
                    return (row, column);
                }
            }
        }

        return (-1, -1);
    }

    public static bool IsPathable(Coordinates path)
    {
        return PathableTiles.Contains(path);
    }

    public static void UpdatePathables()
    {
        var pathableTesting = new List<Coordinates>()
        {
            PathCurrentPlayerLocation + (1, 0),
            PathCurrentPlayerLocation + (-1, 0),
            PathCurrentPlayerLocation + (0, 1),
            PathCurrentPlayerLocation + (0, -1)
        };

        PathableTiles = new List<Coordinates>();

        //List<Coordinates> pathableTester = new List<Coordinates>()
        //{
        //    PathCurrentPlayerLocation + (1, 0),
        //    PathCurrentPlayerLocation + (-1, 0),
        //    PathCurrentPlayerLocation + (0, 1),
        //    PathCurrentPlayerLocation + (0, -1)
        //};

        foreach (Coordinates c in pathableTesting)
        {
            //bool isGood = true;

            //TileController tile = GetTile(c + (-1, 0));
            //if (tile != null && tile.IsPath)
            //{
            //    isGood = false;
            //}

            //tile = GetTile(c + (0, -1));
            //if (tile != null && tile.IsPath)
            //{
            //    isGood = false;
            //}

            if (GetTile(c) != null && !GetTile(c).Unpathable)
            {
                if (IsGoodSide(c + (-1, 0)) && IsGoodSide(c + (0, -1)) && IsGoodSide(c + (1, 0)) && IsGoodSide(c + (0, 1)))
                {
                    PathableTiles.Add(c);
                }
            }

            //List<Coordinates> selectedAdjacent = new List<Coordinates>
            //{
            //    tile + (1, 0),
            //    tile + (-1, 0),
            //    tile + (0, 1),
            //    tile + (0, -1),
            //};

            //selectedAdjacent.Remove(PathCurrentPlayerLocation);

            //foreach (Coordinates adjacent in selectedAdjacent)
            //{
            //    if (adjacent.Row >= -1 && adjacent.Column >= -1 && adjacent.Row < Tiles.Count && adjacent.Column < Tiles[0].Count)
            //    {
            //        if (GetTile(adjacent).IsPath)
            //        {
            //            isGood = false;
            //            break;
            //        }
            //    }

            //    else
            //    {
            //        isGood = false;
            //    }
            //}

            //if (!isGood)
            //{
            //    PathableTiles.Remove(c);
            //}
        }
    }

    private static bool IsGoodSide(Coordinates c)
    {
        return GetTile(c) == null || GetTile(c) == GetTile(PathCurrentPlayerLocation) || !GetTile(c).IsPath;
    }
}

[Serializable]
public struct Coordinates
{
    public int Row;
    public int Column;

    public static implicit operator Coordinates((int row, int column) t) => new Coordinates { Row = t.row, Column = t.column };
    public static Coordinates operator +(Coordinates c, (int row, int column) t) => (c.Row + t.row, c.Column + t.column);
}