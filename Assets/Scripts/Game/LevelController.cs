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
    Attack,
}

public static class LevelController
{
    public static LevelControllerBehavior ControllerScript;

    public static LevelPhase Phase = LevelPhase.None;
    public static List<List<TileController>> Tiles = new List<List<TileController>>();

    public static Coordinates PathCurrentPlayerLocation;
    public static List<Coordinates> PathRoute = new List<Coordinates>();
    public static List<Coordinates> PathableTiles = new List<Coordinates>();

    public static void EnterLevel()
    {
        Phase = LevelPhase.Pathmaking;
        foreach (Transform row in GameObject.FindGameObjectWithTag("level").transform)
        {
            TileController[] tilesInRow = row.GetComponentsInChildren<TileController>();
            Tiles.Add(tilesInRow.ToList());
        }
    }

    public static TileController GetTile(Coordinates c)
    {
        return Tiles[c.Row][c.Column];
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
        PathableTiles = new List<Coordinates>()
        {
            PathCurrentPlayerLocation + (1, 0),
            PathCurrentPlayerLocation + (-1, 0),
            PathCurrentPlayerLocation + (0, 1),
            PathCurrentPlayerLocation + (0, -1)
        };

        List<Coordinates> pathableTester = new List<Coordinates>()
        {
            PathCurrentPlayerLocation + (1, 0),
            PathCurrentPlayerLocation + (-1, 0),
            PathCurrentPlayerLocation + (0, 1),
            PathCurrentPlayerLocation + (0, -1)
        };

        foreach (Coordinates tile in pathableTester)
        {
            bool isGood = true;
            List<Coordinates> selectedAdjacent = new List<Coordinates>
            {
                tile + (1, 0),
                tile + (-1, 0),
                tile + (0, 1),
                tile + (0, -1),
            };

            selectedAdjacent.Remove(PathCurrentPlayerLocation);

            foreach (Coordinates adjacent in selectedAdjacent)
            {
                if (adjacent.Row >= 0 && adjacent.Column >= 0 && adjacent.Row < Tiles.Count && adjacent.Column < Tiles[0].Count)
                {
                    if (GetTile(adjacent).IsPath)
                    {
                        isGood = false;
                        break;
                    }
                }
            }

            if (!isGood)
            {
                PathableTiles.Remove(tile);
            }
        }
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