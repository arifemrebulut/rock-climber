using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public Rigidbody testBall;

    [SerializeField] private Rigidbody pelvisRb;
    [SerializeField] private Rigidbody rightHandRb;
    [SerializeField] private Rigidbody leftHandRb;

    [SerializeField] private float balanceForce;

    private Vector3 mouseDownPosition;
    private Vector3 currentMousePosition;

    private Vector3 direction;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            currentMousePosition = Input.mousePosition;

            Vector3 diffence = currentMousePosition - mouseDownPosition;

            direction = new Vector3(diffence.x, diffence.y, 0f).normalized;
        }
    }

    private void FixedUpdate()
    {
        Balance();
    }

    private void Balance()
    {
        pelvisRb.AddForce(Vector3.up * balanceForce * Time.fixedDeltaTime);
    }
}
