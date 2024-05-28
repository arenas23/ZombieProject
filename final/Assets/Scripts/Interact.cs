using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    InteractEvent interact = new();


    public InteractEvent GetInteractEvent
    {
        get
        {
            interact ??= new InteractEvent();
            return interact;
        }
    }

    public void CallInteract()
    {
        interact.CallInteractEvent();
    }

    public void CallView()
    {
        interact.CallViewEvent();
    }

    public void CallHideView()
    {
        interact.HideViewEvent();
    }

}

public class InteractEvent
{
    public delegate void InteractHandler();
    public event InteractHandler HasInteracted;

    public delegate void InteractView();
    public event InteractHandler CanView;

    public delegate void InteractHide();
    public event InteractHandler HideView;

    public void CallInteractEvent() => HasInteracted?.Invoke();

    public void CallViewEvent() => CanView?.Invoke();

    public void HideViewEvent() => HideView?.Invoke();
}