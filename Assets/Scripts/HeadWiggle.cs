using DG.Tweening;
using UnityEngine;

public class HeadWiggle : MonoBehaviour
{
    public float RotationDuration = 1f;
    public float RotationStrength = 20f;
    
    private void Start()
    {
        transform.DOShakeRotation(RotationDuration, RotationStrength).SetLoops(-1, LoopType.Yoyo);
    }
}