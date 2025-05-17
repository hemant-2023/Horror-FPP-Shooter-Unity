using UnityEngine;

public class Finish1 : MonoBehaviour
{
    public GameObject _winPanel;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
