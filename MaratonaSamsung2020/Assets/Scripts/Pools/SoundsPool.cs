using UnityEngine;

public class SoundsPool : Pool<GameObject> {

    public SoundsPool(GameObject SoundPointPrefab, int size) : base(SoundPointPrefab, size) {

    }

    protected override GameObject CreateItem() {

        GameObject soundPoint = GameObject.Instantiate(poolItem);
        soundPoint.SetActive(false);
        
        return soundPoint;

    }

}
