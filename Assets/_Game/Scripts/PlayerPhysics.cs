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

    private Rigidbody targetRb;

    private Vector3 mouseDownPosition;
    private Vector3 currentMousePosition;
    private Vector3 direction;

    private RaycastHit hit;
    private Camera mainCamera;
    private float maxDistance = 100f;

    private bool jumping;
    private bool onGround;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;

            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, maxDistance, handRayLayer))
            {
                if (hit.transform.CompareTag("RightHand"))
                {
                    Debug.Log("HIT RIGHT");
                    targetRb = rightHandRb;
                }
                else if (hit.transform.CompareTag("LeftHand"))
                {
                    Debug.Log("HIT LEFT");
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
            Vector3 diffence = currentMousePosition - mouseDownPosition;

            direction = new Vector3(diffence.x, diffence.y, 0f).normalized;

            jumping = true;
        }

        CheckOnGround();
  
    }

    private void FixedUpdate()
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

    private void Balance()
    {
        pelvisRb.AddForce(Vector3.up * balanceForce * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (targetRb != null)
        {
            targetRb.velocity = direction * jumpForce * Time.fixedDeltaTime;
        }
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