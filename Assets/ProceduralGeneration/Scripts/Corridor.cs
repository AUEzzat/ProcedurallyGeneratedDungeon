using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : Walkable
{
    public List<Vector3> GetContactTiles()
    {
        if (size.x < size.z)
        {
            for (int i = 0; i <= size.x; i++)
            {
                contactTiles.Add(transform.position + new Vector3(i - size.x / 2, 0, size.z / 2 + 1));
                contactTiles.Add(transform.position + new Vector3(i - size.x / 2, 0, -size.z / 2 - 1));
            }
        }
        else
        {
            for (int i = 0; i <= size.z; i++)
            {
                contactTiles.Add(transform.position + new Vector3(size.x / 2 + 1, 0, i - size.z / 2));
                contactTiles.Add(transform.position + new Vector3(-size.x / 2 - 1, 0, i - size.z / 2));
            }
        }

        return contactTiles;
    }
}
