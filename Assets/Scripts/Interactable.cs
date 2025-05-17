using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //add and remove interaction Event Component to this gameobject
    public bool _useEvent;
    //maggage displays to the player on looking at it
    public string _promptMassage;

    //this function will be called by player script
    public void BaseInteract()
    {
        if (_useEvent)
            GetComponent<InteractionEvent>()._onInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        //this is a tamplate function overwriten by subclass
    }
}
