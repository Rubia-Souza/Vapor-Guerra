using System.Collections;
using UnityEngine;

public class SoundPropagation : MonoBehaviour {

    private const float limitAproximation = 0.5f;

    private Vector3 originalScale;
    private Coroutine propagateSoundCoroutine;

    [SerializeField] private float propagationTime = 1f;
    public float PropagationTime {

        get => propagationTime;
        set => propagationTime = value;
        
    }

    [SerializeField] private float soundVelocity = 1f;
    public float SoundVelocity {

        get => soundVelocity;
        set => soundVelocity = value;

    }

    [SerializeField] private float maxRadius;
    public float MaxRadius {

        get => maxRadius;
        set => maxRadius = value;

    }

    public float PropagationTimePast { get; private set; } = 0f;
    public float InitialPorpagationTime { get; private set; } = 0f;
    public delegate void OnSoundEndPropagation();
    public event OnSoundEndPropagation HandleSoundEndPropagation;
    
    private void Awake() {

        originalScale = transform.localScale;

    }

    private IEnumerator PropagateSoundCrountine() {
        
        PropagationTimePast = InitialPorpagationTime = 0;

        while (!HasCompletedPropagationTime()) {

            if (!HasReachedScaleLimit()) {

                ExpandLocalScale();

            }

            PropagationTimePast += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();

        }
            
        HandleSoundEndPropagation();

    }

    public bool HasReachedScaleLimit() {

        float actualRadius = transform.localScale.x;
        float distance = maxRadius - actualRadius;

        return (distance < limitAproximation);

    }

    public bool HasCompletedPropagationTime() {

        return (propagationTime <= PropagationTimePast);

    }

    private void ExpandLocalScale() {

        float acutalRadius = transform.localScale.x;
        float expandedRadius = acutalRadius + CalculateRadius(PropagationTimePast);
        float interpolatedRadius = Mathf.Lerp(acutalRadius, expandedRadius, SoundVelocity * Time.fixedDeltaTime);

        transform.localScale = new Vector3(interpolatedRadius, interpolatedRadius, interpolatedRadius);

    }

    private float CalculateRadius(float timePast) {

        float newRadius = SoundVelocity * timePast / 100;
        return newRadius;

    }

    public void StartSoundPropagation() {

        RestartLocalRadius();
        propagateSoundCoroutine = StartCoroutine(PropagateSoundCrountine());

    }

    public void StopSoundPropagation() {

        if (propagateSoundCoroutine != null) {

            StopCoroutine(propagateSoundCoroutine);

            PropagationTimePast = InitialPorpagationTime = 0;
            propagateSoundCoroutine = null;

            RestartLocalRadius();

        }

    }

    private void RestartLocalRadius() {

        transform.localScale = originalScale;

    }

    public void RestartSoundPropagation() {

        StopSoundPropagation();
        StartSoundPropagation();

    }

}
