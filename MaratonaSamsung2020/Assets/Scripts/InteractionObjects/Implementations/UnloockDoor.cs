using UnityEngine;
using UnityEngine.Events;

public class UnloockDoor : ConstantInteractionObject {

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite unloockedSprite;

    [HideInInspector] public bool IsUnloocked = false;

    public UnityEvent UnloockDoorAction;

    private void Awake() {

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected override void Action() {
        
        if (!IsUnloocked) {

            base.Action();

        }
        else {

            UnloockedAction();

        }

    }

    protected override void OnInteractionCompleated() {

        if (unloockedSprite != null) {

            spriteRenderer.sprite = unloockedSprite;
            gameObject.name = "UnloockedDoor";

        }

        IsUnloocked = true;

    }

    protected void UnloockedAction() {

        UnloockDoorAction.Invoke();

    }

}
