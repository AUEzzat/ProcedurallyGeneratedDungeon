using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{
    protected Vector3 size;
    protected List<Vector3> contactTiles;
    protected float torchPerMultiple;

    public void Init(Transform parent, Vector3 position, Vector3 size, float torchPerMultiple)
    {
        this.size = size;
        contactTiles = new List<Vector3>();
        this.torchPerMultiple = torchPerMultiple;

        transform.parent = parent;
        transform.position = position;
    }

    public void GenerateWalls()
    {
        for (int i = 0; i <= size.x; i++)
        {
            CreateWall(new Vector3(i - size.x / 2, 0, size.z / 2 + 0.5f), Quaternion.identity, i);
            CreateWall(new Vector3(i - size.x / 2, 0, -size.z / 2 - 0.5f), Quaternion.Euler(0, 180, 0), i);
        }
        for (int i = 0; i <= size.z; i++)
        {
            CreateWall(new Vector3(size.x / 2 + 0.5f, 0, i - size.z / 2), Quaternion.Euler(0, 90, 0), i);
            CreateWall(new Vector3(-size.x / 2 - 0.5f, 0, i - size.z / 2), Quaternion.Euler(0, -90, 0), i);
        }

        CreateColumn(new Vector3(size.x / 2 + 0.5f, 0, size.z / 2 + 0.5f));
        CreateColumn(new Vector3(-size.x / 2 - 0.5f, 0, size.z / 2 + 0.5f));
        CreateColumn(new Vector3(size.x / 2 + 0.5f, 0, -size.z / 2 - 0.5f));
        CreateColumn(new Vector3(-size.x / 2 - 0.5f, 0, -size.z / 2 - 0.5f));
    }

    void CreateColumn(Vector3 relPos)
    {
        GameObject wall = Instantiate(DungeonGenerator.Column, transform);
        wall.transform.position = transform.position + relPos;
    }

    void CreateWall(Vector3 relPos, Quaternion rot, float multiple)
    {
        Vector3 wallPos = transform.position + relPos;
        foreach (Vector3 tilePos in contactTiles)
        {
            if (Vector3.Distance(tilePos, wallPos) < 1f)
                return;
        }

        GameObject wall = Instantiate(DungeonGenerator.Wall, transform);
        wall.transform.position = wallPos;
        wall.transform.rotation = rot * wall.transform.rotation;
        wall = Instantiate(DungeonGenerator.Wall, transform);
        wall.transform.position = wallPos + new Vector3(0, 1, 0);
        wall.transform.rotation = rot * wall.transform.rotation;
        if (multiple % torchPerMultiple == 0)
        {
            GameObject torch = Instantiate(DungeonGenerator.Torch, transform);
            torch.transform.position = wallPos + wall.transform.up * 0.2f + new Vector3(0, 1.2f, 0);
            torch.transform.rotation = rot * Quaternion.Euler(new Vector3(0, 180)); ;
            Light light = new GameObject("LightSource").AddComponent<Light>();
            light.transform.parent = torch.transform;
            light.type = LightType.Point;
            light.transform.position = torch.transform.position;
        }
    }

    public void GenerateFloor()
    {
        for (int i = 0; i <= size.x; i++)
        {
            for (int j = 0; j <= size.z; j++)
            {
                GameObject tile = Instantiate(DungeonGenerator.Tile, transform);
                tile.transform.position = transform.position + new Vector3(i - size.x / 2, 0, j - size.z / 2);
            }
        }
    }
}
