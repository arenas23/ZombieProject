using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public Interact interaction;
    Canvas lockerUI;
    Animator lockerAnimator;

    void Start()
    {
        lockerUI = GetComponentInChildren<Canvas>();
        lockerAnimator = GetComponent<Animator>();
        lockerUI.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        if (interaction)
        {
            interaction.GetInteractEvent.HasInteracted += Interact;
            interaction.GetInteractEvent.CanView += ShowDetails;
            interaction.GetInteractEvent.HideView += HideDetails;
        }
    }

    private void OnDisable()
    {
        if (interaction)
        {
            interaction.GetInteractEvent.HasInteracted -= Interact;
            interaction.GetInteractEvent.CanView -= ShowDetails;
            interaction.GetInteractEvent.HideView -= HideDetails;
        }
    }

    void ShowDetails()
    {
        lockerUI.gameObject.SetActive(true);
    }

    void Interact()
    {
        lockerAnimator.Play("OpenLocker");
        HideDetails();
        gameObject.layer = 1;
    }

    void HideDetails()
    {
        lockerUI.gameObject.SetActive(false);
    }


}
