using UnityEngine;

public class PlatformPatrolHorizontal : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
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
        if (!movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                ChangeInDirection();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                ChangeInDirection();
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
       enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * Mathf.Sign(_direction), initScale.y, initScale.z);

        //Make enemy move in direction
        enemy.position = new Vector3(enemy.position.x + _direction * speed, enemy.position.y, enemy.position.z);
    }
}