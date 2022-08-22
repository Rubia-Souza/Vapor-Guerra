using UnityEngine.Events;

public interface IEvent {

    void Raise();

    void RegisterCallback(UnityAction action);

    void UnregisterCallback(UnityAction action);

}
