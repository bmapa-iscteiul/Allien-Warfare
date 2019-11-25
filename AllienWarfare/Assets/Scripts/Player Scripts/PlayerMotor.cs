
using UnityEngine;


 [RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Rigidbody rb;
    private Vector3 rotation = Vector3.zero;
    private Vector3 vel = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        vel = velocity;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
 
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement() 
    {
        if(vel != Vector3.zero)
        {
            // move para a posição com a velocidade calculada
            rb.MovePosition(rb.position + vel * Time.fixedDeltaTime);
        }
    }


    void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
    public void Rotate(Vector3 rotate)
    {
        rotation = rotate;
    }

    public void RotateCamera(Vector3 rotateCamera)
    {
        cameraRotation = rotateCamera;
    }
}
