using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject _winPanel;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Finish")) // this script should now be on the Player!
        {
            _winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
