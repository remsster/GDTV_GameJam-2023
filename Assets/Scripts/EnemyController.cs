using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float dwellTime = 2;

    [SerializeField] private Transform[] patrolPoints;

    private Transform currentPoint;
    private int patrolPointIndex = 0;
    private float timeSpentWaiting = 0;


    // ---------------------------------------------
    // Unity Engine
    // ---------------------------------------------

    private void Start()
    {
        currentPoint = patrolPoints[0];
    }

    private void Update()
    {
        if (currentPoint != null)
        {
            if (ArrivedAtPatrolPoint())
            {
                GetNextPatrolPoint();
                timeSpentWaiting = 0;
            }
        }
        if (timeSpentWaiting > dwellTime)
        {
            Move();
        }
        timeSpentWaiting += Time.deltaTime;
    }

    private void OnEnable()
    {
        Health.DeathEvent += Health_DeathEvent;
    }

    private void OnDisable()
    {
        Health.DeathEvent -= Health_DeathEvent;
    }

    // ---------------------------------------------
    // Events
    // ---------------------------------------------

    private void Health_DeathEvent()
    {
        Destroy(gameObject);
    }

    // ---------------------------------------------
    // Other
    // ---------------------------------------------

    public void TakeDamage()
    {
        Health.TakeDamage(10);
    }

    private void GetNextPatrolPoint()
    {
        patrolPointIndex += 1;
        if (patrolPointIndex == patrolPoints.Length) patrolPointIndex = 0;
        currentPoint = patrolPoints[patrolPointIndex];
    }

    private bool ArrivedAtPatrolPoint()
    {
        return Vector3.Distance(transform.position, currentPoint.position) < 1f;
    }

    private void Move()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        float moveSpeedX = speed * direction.x;
        float moveSpeedY = speed * direction.y;
        // float scale = 1f;
        // if (direction.x < 0) { scale = -scale; }
        
        
        Rigidbody2D.velocity = new Vector2(moveSpeedX,moveSpeedY);
        // stateMachine.transform.localScale = new Vector2(scale, 1);
    }



}
