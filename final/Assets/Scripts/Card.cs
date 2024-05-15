using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Interact openFromInteraction;
    Canvas UICard;
    Objetivos objetivos;

    void Start(){
        UICard = GetComponentInChildren<Canvas>();
        UICard.gameObject.SetActive(false);
        objetivos = GameObject.Find("UIObjectives").GetComponent<Objetivos>();
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
        GameManager.Instance.keyCard += 1;
        objetivos.CompleteObjective(Objetivos.ObjectivesEnum.SEARCH_KEYCARD);
        gameObject.SetActive(false);
    }
}
