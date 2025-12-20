using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    #region Calc Vars

    //Refs
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    //Anims
    private bool facingLeft = true;

    //Physics
    private Vector2 input;
    private Vector2 lastMoveDir;

    #endregion

    #region Config Vars

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5;

    [Header("Rotations")]
    [SerializeField] private int rotateAmt = 90;
    [SerializeField] public int currRotation; //Range 1-3
    [SerializeField] private float rotationSpeed = 360f;
    private bool rotating;
    #endregion

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Movement
        ReadMoveInputs();
        Animate();
        if (input.x < 0 && !facingLeft || input.x > 0 && facingLeft)
        {
            Flip();
        }

        //Interactions & Collisions
        if (Input.GetKeyDown(KeyCode.F) && !carryingObject && nearbyPickup && objNearby != null)
        {
            Pickup();
        }
        else if (Input.GetKeyDown(KeyCode.F) && carryingObject)
        {
            Drop();
        }

        //Rotate Mechanic
        if (Input.GetKeyDown(KeyCode.Q) && !rotating)
        {
            StartCoroutine(PerformRotation(-rotateAmt));
        }
        if (Input.GetKeyDown(KeyCode.E) && !rotating)
        {
            StartCoroutine(PerformRotation(rotateAmt));
        }

        //Lock Mechanic
        if(Input.GetKeyDown(KeyCode.L) && objNearby != null && !objNearby.GetComponent<RotateObject>().locked)
        {
            objNearby.GetComponent<RotateObject>().Lock(true);
        }
        else if (Input.GetKeyDown(KeyCode.L) && objNearby != null && objNearby.GetComponent<RotateObject>().locked)
        {
            objNearby.GetComponent<RotateObject>().Lock(false);
        }
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    private void ReadMoveInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero)
        {
            input.Normalize();
            lastMoveDir = input;
        }

        Vector2 movement = transform.right * input.x + transform.up * input.y;
        rb.linearVelocity = movement * moveSpeed;
    }

    private void PerformMovement()
    {
        Vector2 movement = transform.right * input.x + transform.up * input.y;
        rb.linearVelocity = movement * moveSpeed;
    }

    private void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDir.x);
        anim.SetFloat("LastMoveY", lastMoveDir.y);
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingLeft = !facingLeft;
    }

    private IEnumerator PerformRotation(int dirAmt)
    {
        rotating = true;
        //Calculate Rotation
        currRotation += (dirAmt / 90);
        if (currRotation > 3) currRotation = 0;
        else if (currRotation < 0) currRotation = 3;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, currRotation * 90f);

        while(Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
                );
            yield return null;
        }
        transform.rotation = targetRotation;

        rotating = false;
    }

    private void Pickup()
    {
        objNearby.transform.parent = transform;
        carriedObj = objNearby;

        carriedObj.transform.localPosition = pickupOffset;
        carryingObject = true;
    }

    private void Drop()
    {
        carriedObj.transform.parent = null;
        carriedObj = null;
        carryingObject = false;
    }

    #region Collisions and Interaction
    [Header("Interaction")]
    [SerializeField] private GameObject carriedObj = null;
    [SerializeField] private bool carryingObject = false;

    [SerializeField] private Vector2 pickupOffset;

    [Header("Collisions")]
    [SerializeField] private GameObject objNearby = null;
    [SerializeField] private bool nearbyPickup;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        objNearby = collider.gameObject;
        if(collider.gameObject.tag == "PickupObj")
        {
            nearbyPickup = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "PickupObj")
        {
            nearbyPickup = false;
        }
        objNearby = null;
    }

    #endregion
}
