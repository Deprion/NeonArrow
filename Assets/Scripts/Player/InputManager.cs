using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Player.MoveClick.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.MoveClick.Invoke();
        }
    }
}
