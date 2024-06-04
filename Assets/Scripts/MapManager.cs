using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private Transform map;

    private Tile[] tiles = new Tile[20];
    private int index = 0;

    private void Awake()
    {
        Events.RoadEnd.AddListener(GenerateNext);
    }

    private void Start()
    {
        for (int i = 0; i < tiles.Length; i++) 
        {
            var obj = Instantiate(tile, map, false);
            tiles[i] = obj.GetComponent<Tile>();
            obj.SetActive(false);
        }

        GenerateNew();
    }
    private void GenerateNew()
    {
        ResetAll();

        tiles[index].Init(Vector3.zero, 5, false);

        index++;

        for (int i = 0; i < 10; i++)
        {
            GenerateNext(tiles[index - 1].transform.localPosition, tiles[index - 1].Size, index % 2 == 1);
        }
    }

    private void GenerateNext()
    {
        int temp = index == 0 ? tiles.Length - 1 : index - 1;

        GenerateNext(tiles[temp].transform.localPosition, tiles[temp].Size, index % 2 == 1);
    }

    private void GenerateNext(Vector3 prevPos, int prevSize, bool turnRight)
    {
        int size = GetSize();

        Vector3 pos = Vector3.zero;

        if (turnRight)
        {
            pos.y = prevPos.y + prevSize / 2f - 0.5f;
            pos.x = prevPos.x + size / 2f + 0.5f;
        }
        else
        {
            pos.y = prevPos.y + size / 2f + 0.5f;
            pos.x = prevPos.x + prevSize / 2f - 0.5f;
        }

        tiles[index].Init(pos, size, turnRight);

        index = index + 1 >= tiles.Length ? 0 : index + 1;
    }

    private void ResetAll()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].gameObject.SetActive(false);
        }

        index = 0;
    }

    private int GetSize()
    {
        float rnd = Random.value;

        if (rnd < 0.2f)
            return 4;
        else if (rnd < 0.46f)
            return 3;
        else if (rnd < 0.72f)
            return 2;
        else return 1;
    }
}
