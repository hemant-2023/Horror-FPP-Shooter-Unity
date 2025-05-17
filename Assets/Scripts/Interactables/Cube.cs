using UnityEngine;

public class Cube : Interactable
{

    [SerializeField]
    private GameObject _door;
    private bool _doorOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to design interaction
    protected override void Interact()
    {
        Debug.Log("Interactaed with "+gameObject.name);
        _doorOpen = !_doorOpen;
        _door.GetComponent<Animator>().SetBool("IsOpen",_doorOpen);
    }
}
