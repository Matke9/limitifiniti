using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 4f;            
    public float distanceOffset = 5f;         
    public float rotationSpeed = 5f;           
    public float stopThreshold = 0.1f;         
    public float bufferZone = 1f;              

    private Camera mainCamera;                 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
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
        }
        else if (distanceToCamera < distanceOffset - bufferZone)
        {
            Vector3 backwardDirection = (transform.position - positionToFollow).normalized;
            targetPosition = transform.position + backwardDirection * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            targetPosition = transform.position;
        }
        rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, 5*Time.deltaTime));

        //Rotacija
        Vector3 directionToCamera = positionToFollow - transform.position;
        float angle = Mathf.Atan2(directionToCamera.y, directionToCamera.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

}
