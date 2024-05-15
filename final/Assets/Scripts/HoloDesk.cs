using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoloDesk : MonoBehaviour
{
    public Interact openFromInteraction;
    Objetivos objetivos;

    [Header("Images")]
    [SerializeField] private Sprite blueTextBackground;
    [SerializeField] private Sprite redTextBackground;
    [SerializeField] private TextMeshProUGUI deskText;
    [SerializeField] private Image image, imageInteract;

    bool firstTime = true;

    Canvas UIDesk;

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

        UIDesk = GetComponentInChildren<Canvas>();
        UIDesk.gameObject.SetActive(false);
        objetivos = GameObject.Find("UIObjectives").GetComponent<Objetivos>();

    }


    void ShowDetails()
    {
        UIDesk.gameObject.SetActive(true);
        if (firstTime)
        {
            objetivos.ActivateObjective(Objetivos.ObjectivesEnum.TURN_ON_GENERATORS);
            firstTime = false;
        }

        if (GameManager.Instance.activeGenerators < 2 )
        {
            image.sprite = redTextBackground;
            deskText.text = "No Energy";
        }
        else
        {
            image.sprite = blueTextBackground;
            deskText.text = "Divert Course";
        }

        imageInteract.gameObject.SetActive(true);

    }

    void Interact()
    {
        if (GameManager.Instance.activeGenerators == 2)
        {
            objetivos.ActivateObjective(Objetivos.ObjectivesEnum.ESCAPE);

        }
        
    }

    void HideDetails()
    {
        UIDesk.gameObject.SetActive(false);
    }
}
