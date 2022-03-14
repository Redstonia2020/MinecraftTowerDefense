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
        Coordinates[] pathableCoordinates = new Coordinates[]
        {
            PathCurrentPlayerLocation + (1, 0),
            PathCurrentPlayerLocation + (-1, 0),
            PathCurrentPlayerLocation + (0, 1),
            PathCurrentPlayerLocation + (0, -1)
        };

        if (pathableCoordinates.Contains(path))
        {
            List<Coordinates> adjacentCoordinates = new List<Coordinates>
            {
                path + (1, 0),
                path + (-1, 0),
                path + (0, 1),
                path + (0, -1),
            };

            adjacentCoordinates.Remove(PathCurrentPlayerLocation);

            foreach (Coordinates c in adjacentCoordinates)
            {
                if (c.Row >= 0 && c.Column >= 0 && c.Row < Tiles.Count && c.Column < Tiles[0].Count)
                {
                    if (GetTile(c).IsPath)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        return false;
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