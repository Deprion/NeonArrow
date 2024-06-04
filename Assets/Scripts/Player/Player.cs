using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed { get { return 2 + extraSpeed; } }
    private float extraSpeed = 0;

    [SerializeField] private ParticleSystem StopFX;

    private Vector3 move = new Vector3(0, 1, 0);

    private readonly Vector3 origin = new Vector3(0, -1, 0);

    private readonly Vector3 upMove = new Vector3(0, 1, 0);
    private readonly Vector3 rightMove = new Vector3(1, 0, 0);

    public static SimpleEvent MoveClick = new SimpleEvent();

    private readonly Quaternion rightRot = Quaternion.Euler(0, 0, -90);
    private Quaternion goalRot = Quaternion.identity;
    private float time = 0;

    public bool isPlaying { private set; get; } = false;

    private void Awake()
    {
        MoveClick.AddListener(ChangeMove);
    }

    private void ChangeMove()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            return;
        }

        if (move.y == 1)
        {
            move = rightMove;
            goalRot = rightRot;
        }
        else
        {
            move = upMove;
            goalRot = Quaternion.identity;
        }

        time = 0;
        extraSpeed += 0.01f;
    }

    public void Restart()
    {
        transform.localPosition = origin;
        transform.localRotation = Quaternion.identity;
        move = upMove;
        goalRot = Quaternion.identity;
        extraSpeed = 0;

        isPlaying = false;
    }

    public void Stop()
    {
        isPlaying = false;
        StopFX.Play();
    }

    private void FixedUpdate()
    {
        if (!isPlaying)
        { 
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;

            return;
        }

        rb.velocity = speed * move;

        if (transform.localRotation != goalRot)
        {
            time += Time.fixedDeltaTime;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, goalRot, time);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Border"))
            Events.Lose.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoadEnd"))
        { 
            Events.RoadEnd.Invoke();
            collision.gameObject.SetActive(false);
        }
    }
}
