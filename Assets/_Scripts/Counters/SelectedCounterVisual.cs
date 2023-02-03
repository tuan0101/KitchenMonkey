using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] ClearCounter clearCounter;
    [SerializeField] GameObject visualGameObject;
    private void Start() // run after Player's Awake()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        // alternative for Player_OnSelectedCounterChanged
        //Player.Instance.OnCounterChanged += OnCounterChanged;
    }


    // alternative for Player_OnSelectedCounterChanged
    void OnCounterChanged(ClearCounter selectedCounter)
    {
        if (this.clearCounter == selectedCounter)
        {
            Show();
            Debug.Log("from event action");
        }
        else
        {
            Hide();
        }
    }
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        Debug.Log("who trigger me? : " + sender.ToString());
        if (e._selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    void Show()
    {
        visualGameObject.SetActive(true);
    }

    void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
