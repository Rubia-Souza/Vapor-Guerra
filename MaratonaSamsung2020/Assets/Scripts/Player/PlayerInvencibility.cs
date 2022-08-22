using System.Collections;
using UnityEngine;

public class PlayerInvencibility : MonoBehaviour {

    private const int PlayerLayerMaskIndex = ProjectLayers.PlayerLayerMaskIndex;
    private const int UndetectedLayerMaskIndex = ProjectLayers.UndetectableLayerMaskIndex;

    private Coroutine invencibilityCoroutine;

    [SerializeField] private FloatEvent playerRecivedDamageEvent;

    [SerializeField] private float invencibilityTime = 3f;
    public float InvencibilityTime { get; }

    private void Start() {

        playerRecivedDamageEvent.RegisterCallback(MakePlayerInvencible);

    }

    private void MakePlayerInvencible(float actualHealth) {

        StartInvencibility();

    }

    private void StartInvencibility() {

        invencibilityCoroutine = StartCoroutine(Invencibility());

    }

    private IEnumerator Invencibility() {

        transform.gameObject.layer = UndetectedLayerMaskIndex;

        yield return new WaitForSeconds(invencibilityTime);

        transform.gameObject.layer = PlayerLayerMaskIndex;

        StopInvencibility();

    }

    private void StopInvencibility() {

        if (invencibilityCoroutine != null) {

            StopCoroutine(invencibilityCoroutine);
            invencibilityCoroutine = null;

        }

    }

}
