using System.Collections;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask handRayLayer;
    [SerializeField] private Rigidbody pelvisRb;
    [SerializeField] private Rigidbody rightHandRb;
    [SerializeField] private Rigidbody leftHandRb;

    [SerializeField] private float jumpForce;
    [SerializeField] private float balanceForce;
    [SerializeField] private float gripLerpSpeed;

    private Rigidbody targetRb;

    private Vector3 mouseDownPosition;
    private Vector3 currentMousePosition;
    private Vector3 direction;

    private RaycastHit hit;
    private Camera mainCamera;
    private float maxDistance = 100f;

    private bool jumping;
    private bool gripping;
    private bool onGround;
    private bool isDead;

    private float initialMass = 3.75f;

    private GameObject lastGrip;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        EventManager.GripEvent += Grip;
        EventManager.KillPlayerEvent += KillPlayer;
    }

    private void OnDisable()
    {
        EventManager.GripEvent -= Grip;
        EventManager.KillPlayerEvent -= KillPlayer;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            direction = Vector3.zero;

            mouseDownPosition = Input.mousePosition;

            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, maxDistance, handRayLayer))
            {
                if (hit.transform.CompareTag("RightHand"))
                {
                    targetRb = rightHandRb;
                }
                else if (hit.transform.CompareTag("LeftHand"))
                {
                    targetRb = leftHandRb;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            currentMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (lastGrip != null)
            {
                Destroy(lastGrip);
            }

            Vector3 diffence = currentMousePosition - mouseDownPosition;

            direction = new Vector3(diffence.x, diffence.y, 0f).normalized;

            gripping = false;
            leftHandRb.isKinematic = false;
            rightHandRb.isKinematic = false;
            jumping = true;
        }

        CheckOnGround();
  
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (onGround)
            {
                Balance();
            }

            if (jumping)
            {
                Jump();
            }
        }
    }

    private void Balance()
    {
        pelvisRb.AddForce(Vector3.up * balanceForce * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (targetRb != null && direction != Vector3.zero)
        {
            float initialMass = targetRb.mass;
            targetRb.mass *= 2;
            targetRb.velocity = direction * jumpForce * Time.fixedDeltaTime;
        }
    }

    private void Grip(GameObject grip)
    {
        if (targetRb != null && !isDead)
        {
            lastGrip = grip;

            grip.GetComponent<Collider>().enabled = false;

            jumping = false;
            gripping = true;
            targetRb.isKinematic = true;
            targetRb.MovePosition(grip.transform.position);

            targetRb.mass = initialMass;

            StartCoroutine(AutoDestroyGrip());
        }
    }

    private void KillPlayer()
    {
        isDead = true;
        jumping = false;
        gripping = false;
        targetRb.isKinematic = false;
    }

    private IEnumerator AutoDestroyGrip()
    {
        yield return new WaitForSecondsRealtime(2f);

        targetRb.isKinematic = false;

        Destroy(lastGrip);
    }

    private void CheckOnGround()
    {
        Vector3 origin = new Vector3(pelvisRb.transform.position.x, pelvisRb.transform.position.y + 0.4f, transform.position.z);

        onGround = (Physics.OverlapSphere(origin, radius, groundLayer).Length > 0) ? true : false;       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(pelvisRb.transform.position.x, pelvisRb.transform.position.y + 0.4f, transform.position.z), radius);   
    }
}