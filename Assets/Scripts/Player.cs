using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    private Rigidbody2D myRigidBody;
    private Camera mainCamera;
    [SerializeField] private AudioSource fireSound;

    private Vector2 mousePosition;
    private Vector2 offset;
    private Vector3 screenPoint;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float bulletDamage = 50f;
    // shots per second
    [SerializeField] private float fireRate = 3f;
    private bool isShooting = false;
    private bool canShoot = true;

    void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
        fireSound = GetComponent<AudioSource>();
    }

    private void Start() {
        Instance = this;
        mainCamera = Camera.main;
    }

    void Update() {
        if (GameInput.Instance.WasShootActionPerformed()) {
            isShooting = true;
        }
    }

    private void FixedUpdate() {
        MovePlayer();
        RotatePlayer();

        if (isShooting && canShoot) {
            StartCoroutine(Fire());
        }
    }

    private void MovePlayer() {
        // Initialize movement vector
        // This will store the combined direction based on player input
        Vector2 moveDirection = Vector2.zero;

        // Combine GLOBAL directions based on input
        // Global right = (1, 0)
        // Global up    = (0, 1)
        if (GameInput.Instance.IsRightActionPressed()) moveDirection += Vector2.right; // Move right (global X+)
        if (GameInput.Instance.IsLeftActionPressed()) moveDirection += Vector2.left;   // Move left (global X-)
        if (GameInput.Instance.IsUpActionPressed()) moveDirection += Vector2.up;       // Move up (global Y+)
        if (GameInput.Instance.IsDownActionPressed()) moveDirection += Vector2.down;   // Move down (global Y-)

        // Check if the player is giving any input
        if (moveDirection != Vector2.zero) {
            // Normalize the direction to prevent diagonal movement from being faster
            // Multiply by moveSpeed to get the final velocity
            myRigidBody.linearVelocity = moveDirection.normalized * moveSpeed;
        }
        else {
            // No input: stop the player movement
            myRigidBody.linearVelocity = Vector2.zero;
        }
    }

    private void RotatePlayer() {
        // Get the mouse position in screen space
        // Screen space: 0,0 = bottom left | 0.5,0.5 = center | 1,1 = top right
        mousePosition = Mouse.current.position.ReadValue();

        // Convert player's world position to screen space
        screenPoint = mainCamera.WorldToScreenPoint(transform.position);

        // Calculate direction (mouse - player) and normalize
        offset = (mousePosition - new Vector2(screenPoint.x, screenPoint.y)).normalized;

        // Compute angle from the direction vector and apply rotation
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    IEnumerator Fire() {
        fireSound.Play();
        isShooting = false;
        canShoot = false;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = offset * bulletSpeed;

        // Destroy bullet after 3 seconds
        Destroy(bullet, 1.5f);

        // Start the delay
        yield return new WaitForSeconds(1 / fireRate);

        canShoot = true;
    }

    public float GetBulletDamamge() {
        return bulletDamage;
    }

}