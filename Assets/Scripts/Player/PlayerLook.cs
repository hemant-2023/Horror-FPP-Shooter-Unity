using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour
{
    public Camera _cam;
    private float _xRotation = 0f;

    public float _xSensitivity = 30f;
    public float _ySensitivity = 30f;

    // Shake variables
    private Vector3 _originalCamPosition;
    private Coroutine _shakeRoutine;

    public float _shakeDuration = 0.2f;
    public float _shakeMagnitude = 0.1f;

    void Start()
    {
        _originalCamPosition = _cam.transform.localPosition;
    }

    public void ProcessLook(Vector2 input)
    {
        float _mouseX = input.x;
        float _mouseY = input.y;

        _xRotation -= (_mouseY * Time.deltaTime) * _ySensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        _cam.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up * (_mouseX * Time.deltaTime) * _xSensitivity);
    }

    public void Shake()
    {
        if (_shakeRoutine != null)
            StopCoroutine(_shakeRoutine);

        _shakeRoutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < _shakeDuration)
        {
            Vector3 randomPoint = _originalCamPosition + Random.insideUnitSphere * _shakeMagnitude;
            _cam.transform.localPosition = new Vector3(randomPoint.x, randomPoint.y, _originalCamPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        _cam.transform.localPosition = _originalCamPosition;
    }
}
