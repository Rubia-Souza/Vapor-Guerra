
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public abstract class GenericGameEvent<T> : ScriptableObject, IGenericEvent<T> {

    private IList<UnityAction<T>> gameEventCallbacks = new List<UnityAction<T>>();

    public int CountCallback { get => gameEventCallbacks.Count; }

    private void OnEnable() {

        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {

        ClearCallbacks();

    }

    public void ClearCallbacks() {

        gameEventCallbacks.Clear();

    }

    public void Raise(T actionArgument) {
        IList<UnityAction<T>> currentEventCallbacks = gameEventCallbacks.ToList();

        foreach (UnityAction<T> callback in currentEventCallbacks) {

            callback(actionArgument);

        }

    }

    public void RegisterCallback(UnityAction<T> callback) {

        gameEventCallbacks.Add(callback);

    }

    public void UnregisterCallback(UnityAction<T> callback) {

        gameEventCallbacks.Remove(callback);

    }

}
