using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 4f;
    public float distanceOffset = 5f;
    public float rotationSpeed = 5f;
    public float stopThreshold = 0.1f;
    public float bufferZone = 1f;
    public float strikeDelay = 2f;
    private Camera mainCamera;

    private bool canShoot = false;
    private float strikeTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        canShoot = false;
        strikeTimer = 0f;
    }

    void FixedUpdate()
    {
        //Pozicija
        Vector3 positionToFollow = mainCamera.transform.position;
        float distanceToCamera = Vector2.Distance(transform.position, positionToFollow);
        Vector3 targetPosition;

        if (distanceToCamera > distanceOffset + bufferZone)
        {
            targetPosition = Vector3.MoveTowards(transform.position, positionToFollow, moveSpeed * Time.fixedDeltaTime);
            canShoot = false;
        }
        else if (distanceToCamera < distanceOffset - bufferZone)
        {
            Vector3 backwardDirection = (transform.position - positionToFollow).normalized;
            targetPosition = transform.position + backwardDirection * moveSpeed * Time.fixedDeltaTime;
            canShoot = false;
        }
        else
        {
            targetPosition = transform.position;
            canShoot = true;
        }
        rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, 5*Time.deltaTime));

        //Rotacija
        Vector3 directionToCamera = positionToFollow - transform.position;
        float angle = Mathf.Atan2(directionToCamera.y, directionToCamera.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        strikeTimer += Time.deltaTime;
        if (canShoot && animator != null && strikeTimer >= strikeDelay) 
        {
            animator.SetTrigger("shoot");
            Debug.Log("Can Shoot!");
            strikeTimer = 0f;
        }
    }

}
