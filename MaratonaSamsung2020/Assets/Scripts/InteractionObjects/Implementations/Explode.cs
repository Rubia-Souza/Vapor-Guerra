using System.Collections;
using UnityEngine;

public class Explode : InteractableObject {

    private Coroutine createExplosionsCoroutine;

    private int countActualExplosion = 0;

    public SoundData[] explosionSounds;
    public SoundGeneratedEvent soundGeneratedEvent;

    public GameObject[] explosions;
    public Transform[] explosionsPositions;

    public bool desableGameObject;

    protected override void Action() {

        StartCreatingExplosions();

    }

    private void StartCreatingExplosions() {

        createExplosionsCoroutine = StartCoroutine(CreateExplosion());

    }

    private IEnumerator CreateExplosion() {

        while (countActualExplosion < explosions.Length && countActualExplosion < explosionsPositions.Length) {

            Vector3 explosionPosition = explosionsPositions[countActualExplosion].position;

            if (explosionSounds[countActualExplosion] != null) {

                SoundData soundData = explosionSounds[countActualExplosion];
                soundData.Position = explosionPosition;

                soundGeneratedEvent.Raise(soundData);

            }

            GameObject explosion = Instantiate(explosions[countActualExplosion]);
            explosion.transform.position = explosionPosition;

            countActualExplosion++;

            yield return new WaitForSeconds(1f);

        }

        StopCreatingExplosions();

    }

    private void StopCreatingExplosions() {

        if (createExplosionsCoroutine != null) {

            StopCoroutine(createExplosionsCoroutine);
            createExplosionsCoroutine = null;

            countActualExplosion = 0;
            gameObject.SetActive(!desableGameObject);

        }

    }

}
