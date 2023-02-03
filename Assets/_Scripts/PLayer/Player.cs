using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter _selectedCounter;
    }

    [SerializeField] float moveSPeed = 7f;
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask counterLayerMask;

    bool isWalking;
    Vector3 lastInteractDir;
    ClearCounter selectedCounter;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteraction();
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

    void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetmovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) lastInteractDir = moveDir;

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
    }

    void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            _selectedCounter = selectedCounter
        });
    }
}
