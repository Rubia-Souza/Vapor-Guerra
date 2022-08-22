using UnityEngine;

public class CollisionSoundMaker : MonoBehaviour {

    private const string PlayerTag = ProjectTags.PlayerTag;
    private const float DetectionLimit = 7f;

    public SoundData soundData;
    public SoundGeneratedEvent soundGeneratedEvent;

    private void OnCollisionEnter2D(Collision2D collision) {

        float yVelocity = Mathf.Abs(collision.relativeVelocity.y);

        if (collision.collider.CompareTag(PlayerTag) &&  yVelocity >= DetectionLimit) {

            ContactPoint2D[] collisionPoints = collision.contacts;
            Vector2 firstImpactPosition = collisionPoints[0].point;

            soundData.Position = firstImpactPosition;
            soundGeneratedEvent.Raise(soundData);

        }

    }

}
