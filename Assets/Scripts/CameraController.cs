using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MinPosition = -10;
    public float MaxPosition = 10;

    public float SmoothTime = 3;

    private float _currentVelocity;
    
    private void LateUpdate()
    {
        var targetXPosition = Mathf.Clamp(PlayerController.Instance.transform.position.x, MinPosition, MaxPosition);

        var position = transform.position;
        var smoothTarget = Mathf.SmoothDamp(position.x, targetXPosition, ref _currentVelocity, SmoothTime);

        position = new Vector3(smoothTarget, position.y, position.z);
        transform.position = position;
    }
}