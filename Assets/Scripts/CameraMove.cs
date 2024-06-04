using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 pos = player.position;

        pos.z = -10;

        transform.localPosition = pos;
    }
}
