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
    public static List<TileController[]> Tiles = new List<TileController[]>();

    public static Coordinates PathCurrentPlayerLocation;

    public static void EnterLevel()
    {
        Phase = LevelPhase.Pathmaking;
        foreach (Transform row in GameObject.FindGameObjectWithTag("level").transform)
        {
            TileController[] tilesInRow = row.GetComponentsInChildren<TileController>();
            Tiles.Add(tilesInRow);
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
            TileController[] tilerow = Tiles[row];
            for (int column = 0; column < tilerow.Length; column++)
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

        return pathableCoordinates.Contains(path);
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