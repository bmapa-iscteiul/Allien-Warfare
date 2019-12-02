using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float mouseSensitivity = 3f;

    public CharacterStats cs;
    //[SerializeField]
    //private float thrusterForce = 1000f;

    private PlayerMotor motor;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        motor.Move(velocity);

        //Calcula a rotação como um vetor 
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * mouseSensitivity;

        // Aplicar a rotação
        motor.Rotate(rotation);


        //Calcula a rotação como um vetor 
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 rotationX = new Vector3(xRot, 0f, 0f) * mouseSensitivity;

        // Aplicar a rotação
        motor.RotateCamera(rotationX);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyFist")
        {
            cs = GetComponent<CharacterStats>();
            cs.TakeDamage(10);
            
        }
    }

}
