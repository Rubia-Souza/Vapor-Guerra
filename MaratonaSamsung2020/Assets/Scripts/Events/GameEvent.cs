using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public abstract class GameEvent : ScriptableObject, IEvent {

    private IList<UnityAction> gameEventCallbacks = new List<UnityAction>();

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

    public void Raise() {
        IList<UnityAction> currentEventCallbacks = gameEventCallbacks.ToList();

        foreach (UnityAction callback in currentEventCallbacks) {

            callback();

        }

    }

    public void RegisterCallback(UnityAction callback) {

        gameEventCallbacks.Add(callback);

    }

    public void UnregisterCallback(UnityAction callback) {

        gameEventCallbacks.Remove(callback);

    }

}
