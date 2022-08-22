using UnityEngine.Events;

public interface IGenericEvent<T> {

    void Raise(T data);

    void RegisterCallback(UnityAction<T> action);

    void UnregisterCallback(UnityAction<T> action);

}
