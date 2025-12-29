using UnityEngine;

public class PlatformPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform UpEdge;
    [SerializeField] private Transform DownEdge;
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [Header("Idle Behaviour")]
    [SerializeField] private float IdleDuration;
    private float idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.y >= UpEdge.position.y)
            {
                ChangeInDirection();
            }
            else
            {
                MoveInDirection(4);
            }
        }
        else
        {
            if (enemy.position.y <= DownEdge.position.y)
            {
                ChangeInDirection();
            }
            else
            {
                MoveInDirection(-4);
            }
        }
    }
    private void ChangeInDirection()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > IdleDuration)
        {
            movingLeft = !movingLeft;
        }
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        //MAke enemy face direction
       enemy.localScale = new Vector3(initScale.x, Mathf.Abs(initScale.y) * Mathf.Sign(_direction), initScale.z);

        //Make enemy move in direction
        enemy.position = new Vector3(enemy.position.x, enemy.position.y + _direction * speed, enemy.position.z);
    }
}