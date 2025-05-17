using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCamera;   // Assign your FPP camera (child of player)
    public Camera topDownCamera;       // Assign your top-down camera in inspector

    private bool isTopDown = false;

    void Start()
    {
        firstPersonCamera.enabled = true;
        topDownCamera.enabled = false;

        LockCursor(); // Lock cursor at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isTopDown = !isTopDown;

            firstPersonCamera.enabled = !isTopDown;
            topDownCamera.enabled = isTopDown;

            if (isTopDown)
                UnlockCursor();
            else
                LockCursor();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public Transform player; // assign player in inspector
    public Vector3 offset = new Vector3(0, 10, 0);

    void LateUpdate()
    {
        if (topDownCamera.enabled)
        {
            topDownCamera.transform.position = player.position + offset;
            topDownCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
