using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] Interact openFromInteraction;

    private Canvas UIShip;
    Objetivos objetivos;

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

    void Start()
    {

        UIShip = GetComponentInChildren<Canvas>();
        UIShip.gameObject.SetActive(false);
        objetivos = GameObject.Find("UIObjectives").GetComponent<Objetivos>();
    }

    void ShowDetails()
    {
        if(VerifyEscapeObjective() && !GameManager.Instance.PlayerWin)
        {
            UIShip.gameObject.SetActive(true);
        }
     
    }

    void Interact()
    {
        if (VerifyEscapeObjective())
        {
            UIShip.gameObject.SetActive(false);
            GameManager.Instance.WinPlayer();

        }

    }

    void HideDetails()
    {
        UIShip.gameObject.SetActive(false);
    }

    private bool VerifyEscapeObjective(){
        if (objetivos.currentObjective == (int) Objetivos.ObjectivesEnum.ESCAPE)
        {
            return true;
        }else{
            return false;
        }
    }


}
