using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSPeed = 7f;
    [SerializeField] GameInput gameInput;
    bool isWalking;


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetmovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSPeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}
