using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField, Range(2, 4)]
    int corridorWidth = 2;
    [SerializeField, Range(2, 10)]
    int corridorLength = 6;
    [SerializeField, Range(6, 20)]
    int minLength = 10;
    [SerializeField, Range(6, 20)]
    int maxLength = 10;
    [SerializeField]
    int roomCount = 10;
    [SerializeField]
    GameObject tile;
    [SerializeField]
    GameObject wall;
    [SerializeField]
    GameObject column;
    [SerializeField]
    GameObject torch;

    static DungeonGenerator instance;
    public static int CorridorLength;
    public static GameObject Tile => instance.tile;
    public static GameObject Wall => instance.wall;
    public static GameObject Column => instance.column;
    public static GameObject Torch => instance.torch;

    Vector3 roomPos = Vector3.zero;
    Vector3 prevRoomSize = Vector3.zero;
    Room previousRoom;

    private void Start()
    {
        instance = this;
        NextDirection nextDirection = new NextDirection();
        List<Vector3> contactTiles = new List<Vector3>();

        for (int i = 0; i < roomCount; i++)
        {
            Room room = new GameObject("Room " + i).AddComponent<Room>();
            Vector3 nextDirVal = nextDirection.GetValue();
            Vector3 otherDirVal = new Vector3(nextDirVal.z, 0, nextDirVal.x);

            Vector3 roomSize = GetRoomSize();
            roomPos += GetRoomPosition(nextDirVal, otherDirVal, roomSize);
            room.Init(transform, roomPos, roomSize, 3);
            room.GenerateFloor();
            prevRoomSize = roomSize;
            if (i > 0)
            {
                Vector3 corridorSize = corridorLength * nextDirVal + corridorWidth * otherDirVal;
                corridorSize = new Vector3(Mathf.Abs(corridorSize.x), 0, Mathf.Abs(corridorSize.z));
                Vector3 corridorPos = roomPos - corridorLength / 2 * nextDirVal - Vector3.Scale(roomSize / 2 + Vector3.one, nextDirVal);

                Corridor corridor = new GameObject($"Corridor Between Rooms {i - 1} and {i}").AddComponent<Corridor>();
                corridor.Init(transform, corridorPos, corridorSize, 2);
                corridor.GenerateFloor();

                previousRoom.AddCommonTiles(contactTiles);
                contactTiles = corridor.GetContactTiles();
                corridor.GenerateWalls();

                previousRoom.AddCommonTiles(contactTiles);
                previousRoom.GenerateWalls();
            }

            previousRoom = room;
        }
        previousRoom.AddCommonTiles(contactTiles);
        previousRoom.GenerateWalls();
    }

    Vector3 GetRoomSize()
    {
        return new Vector3(Random.Range(minLength, maxLength), 0, Random.Range(minLength, maxLength));
    }

    Vector3 GetRoomPosition(Vector3 nextDirVal, Vector3 otherDirVal, Vector3 roomSize)
    {
        Vector3 shiftDir = Vector3.Scale(otherDirVal, prevRoomSize / 2) - corridorWidth * otherDirVal;
        Vector3 forwardDir = Vector3.Scale(nextDirVal, (prevRoomSize + roomSize) / 2 + Vector3.one * 2) + corridorLength * nextDirVal;
        if (Random.value > 0.5)
            shiftDir *= -1;
        return forwardDir + shiftDir;
    }
}