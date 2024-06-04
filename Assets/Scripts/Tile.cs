using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform road, leftBorder, rightBorder, roadEnd;

    public int Size { get; private set; }

    private bool isTurnedRight = false;

    private static readonly Quaternion rightRot = Quaternion.Euler(0, 0, -90);

    public void Init(Vector3 pos, int size, bool turnRight)
    {
        if (turnRight)
            transform.rotation = rightRot;
        else
            transform.rotation = Quaternion.identity;

        isTurnedRight = turnRight;

        Init(pos, size);
    }

    public void Init(Vector3 pos, int size)
    { 
        transform.localPosition = pos;

        Init(size);
    }

    public void Init(int size)
    {
        Size = size;

        road.localScale = new Vector3(1, size, 1);

        gameObject.SetActive(true);
        roadEnd.gameObject.SetActive(true);

        SetBorders();
    }

    private static readonly Vector3 leftTurned = new Vector3(-1, -1, 0);
    private static readonly Vector3 leftNormal = new Vector3(-1, 0, 0);

    private static readonly Vector3 rightTurned = new Vector3(1, 0, 0);
    private static readonly Vector3 rightNormal = new Vector3(1, -1, 0);

    private void SetBorders()
    {
        leftBorder.localScale = road.localScale;
        rightBorder.localScale = road.localScale;

        if (isTurnedRight)
        {
            leftBorder.localPosition = leftTurned;
            rightBorder.localPosition = rightTurned;
        }
        else
        {
            leftBorder.localPosition = leftNormal;
            rightBorder.localPosition = rightNormal;
        }
    }
}
