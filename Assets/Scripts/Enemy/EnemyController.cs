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

    public bool ranged=false;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private bool canShoot = false, canAttack = false;
    private float strikeTimer = 0f;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        canShoot = false;
        canAttack = false;
        strikeTimer = 0f;

    }

    void FixedUpdate()
    {
        //Pozicija
        Vector3 positionToFollow = mainCamera.transform.position;//Moze da se menja po potrebi
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
        if ((canShoot || canAttack) && animator != null && strikeTimer >= strikeDelay)
        {
            animator.SetTrigger("attack");
            strikeTimer = 0f;
        }
    }

    public void Attack()
    {
        if (ranged)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        }
        else
        {
            Debug.Log("Udari!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!ranged && collision.gameObject.CompareTag("Player"))
        {
            canAttack = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!ranged && collision.gameObject.CompareTag("Player"))
        {
            canAttack = false;
        }
    }
}
