using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player pl;

    private void Awake()
    {
        Events.Lose.AddListener(Lose);
    }

    private void Lose()
    {
        pl.Stop();
    }
}
