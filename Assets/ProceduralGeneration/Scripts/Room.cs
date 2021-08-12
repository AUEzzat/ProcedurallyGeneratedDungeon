using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : Walkable
{
    public void AddCommonTile(Vector3 tilePos)
    {
        contactTiles.Add(tilePos);
    }

    public void AddCommonTiles(List<Vector3> tilesPos)
    {
        contactTiles.AddRange(tilesPos);
    }
}
