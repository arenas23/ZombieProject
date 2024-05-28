using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Interact openFromInteraction;
    Canvas UICard;

    void Start(){
        UICard = GetComponentInChildren<Canvas>();
        UICard.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (openFromInteraction)
        {
            openFromInteraction.GetInteractEvent.HasInteracted += Interact;
            openFromInteraction.GetInteractEvent.CanView += ShowDetails;
            openFromInteraction.GetInteractEvent.HideView += HideDetails;
        }
    }

    private void OnDisable()
    {
        if (openFromInteraction)
        {
            openFromInteraction.GetInteractEvent.HasInteracted -= Interact;
            openFromInteraction.GetInteractEvent.CanView -= ShowDetails;
            openFromInteraction.GetInteractEvent.HideView -= HideDetails;
        }
    }

    void ShowDetails()
    {
        UICard.gameObject.SetActive(true);
    }

    void HideDetails()
    {
        UICard.gameObject.SetActive(false);
    }

    void Interact (){
        gameObject.SetActive(false);
    }
}
