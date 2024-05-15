using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    public LayerMask finalMask;

    [SerializeField] private Transform interactionPoint;
    private readonly float interactionPointRadius = 7f;
    [SerializeField] private LayerMask interactableMask; 

    private Collider[] colliders = new Collider[3];
    [SerializeField] private int numCollidersFound;
    Interact interactable;
    Interact lastInteractable;
    int indexCollider;


    void Update()
    {
        VerifyDistanceToItem();

        if (interactable && numCollidersFound > 0) {
            if (Input.GetKeyDown(KeyCode.E)) PlayerInteract();
         
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }

    public void PlayerInteract()
    {
        interactable.CallInteract(this);
    }

    public void VerifyDistanceToItem()
    {
        numCollidersFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders,interactableMask);
        if (numCollidersFound > 0)
        {
            indexCollider = VerifyDistanceShorter();
            interactable = colliders[indexCollider].GetComponent<Interact>();
            Debug.Log(interactable);
            interactable.CallView();
            if(interactable != lastInteractable){
                lastInteractable?.CallHideView();
            }
            lastInteractable = interactable;
        }else {
            lastInteractable?.CallHideView();
        }
    }

    public int VerifyDistanceShorter()
    {
        float distance = 0;
        int index = 0;
        float minusDistance = float.PositiveInfinity;
        for (int i =0; i < numCollidersFound; i++)
        {
            distance = Vector3.Distance(interactionPoint.position, colliders[i].gameObject.transform.position);
            if (distance < minusDistance)
            {
                minusDistance = distance;
                index = i;
            } 
        }
        return index;

    }

}


// RaycastHit hit;
// Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

// if (Physics.Raycast(ray, out hit, 20, finalMask))
// {
//     Debug.Log(hit.transform.name);
//     Interact interactScript = hit.transform.GetComponent<Interact>();
//     
// }

// 
