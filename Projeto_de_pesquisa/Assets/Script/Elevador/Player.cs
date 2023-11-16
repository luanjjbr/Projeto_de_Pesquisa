using System.Data.Common;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f; // Velocidade de movimentação
    public float sensitivity = 2.0f; // Sensibilidade do mouse
    private float rotationX = 0.0f;
    public static bool id = false;

    void Start()
    {
        // Oculta e trava o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //#if !UNITY_EDITOR
    //   Cursor.lockState = CursorLockMode.Locked;
    //  Cursor.visible = false;
    //#endif

    void Update()
    {
        if (id)
        {
            VerificarCursorBloqueado();
            id = false;
        }
        // Movimentação
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Rotação do mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * sensitivity; // Invertido para corresponder ao padrão FPS

        rotationX += mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
    void VerificarCursorBloqueado()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            //Debug.Log("O cursor está bloqueado.");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Debug.Log("O cursor não está bloqueado.");
        }
    }
}
