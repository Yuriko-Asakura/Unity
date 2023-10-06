using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float Sensivity = 10f;
    public float RotationSmoothing = 20f;
    public float MovementSpeed = 2.0f;
    public float JumpForce = 5.0f;
    public float DistationToGround = 0.1f;
    private float pitch;
    private float yaw;
    private bool IsGrounded;
    private Rigidbody _Rigidbody;
    public GameObject HandMeshes;
    public float SprintSpeed = 10.0f;
    private GameManager _GameManager;



    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _GameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        if (Input.GetKey(KeyCode.Space)&& IsGrounded) Jump() ;
        _Rigidbody.MovePosition(CalculateMovement());
        if (Input.GetKey(KeyCode.LeftShift) && !_GameManager.IsStaminaRestoring)
        {
            _GameManager.SpendStamina();
            _Rigidbody.MovePosition(CalculateSprint());
        }
        else _Rigidbody.MovePosition(CalculateMovement());

        SetRotation();
    }
    public void SetRotation()
    {
        //Считываем движения мыши по X
        yaw += Input.GetAxis("Mouse X") * Sensivity;
        //Считываем движения мыши по Y
        pitch -= Input.GetAxis("Mouse Y") * Sensivity;

        //Делаем ограничение вращения по оси Y
        pitch = Mathf.Clamp(pitch, -60, 90);

        //Куда должен повернуться игровк
        Quaternion SmoothRotation = Quaternion.Euler(pitch, yaw, 0);

        //Линейная интерполяция (вращение со сглаживанием)
        HandMeshes.transform.rotation = Quaternion.Slerp(HandMeshes.transform.rotation, SmoothRotation, RotationSmoothing * Time.fixedDeltaTime);

        //То же самое для всего игрового объекта, но только по оси Y
        SmoothRotation = Quaternion.Euler(0, yaw, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, SmoothRotation, RotationSmoothing * Time.fixedDeltaTime);
    }
        private Vector3 CalculateMovement()
    {
        float HorizontalDirection = Input.GetAxis("Horizontal");
        float VerticalDirection = Input.GetAxis("Vertical");
        Vector3 Move = transform.right * HorizontalDirection + transform.forward * VerticalDirection;
        return _Rigidbody.transform.position + Move * Time.fixedDeltaTime * MovementSpeed;
    }
    private Vector3 CalculateSprint()
    {
        float HorizontalDirection = Input.GetAxis("Horizontal");
        float VerticalDirection = Input.GetAxis("Vertical");

        Vector3 Move = transform.right * HorizontalDirection + transform.forward * VerticalDirection;

        return _Rigidbody.transform.position + Move * Time.fixedDeltaTime * /* Меняется только это -> */ SprintSpeed;
    }

    private void Jump()
    {
        _Rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
    private void GroundCheck()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, DistationToGround);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * DistationToGround));
    }
}

