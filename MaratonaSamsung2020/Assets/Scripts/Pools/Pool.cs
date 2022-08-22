using System.Collections.Generic;

public abstract class Pool<T> {

    protected T poolItem;
    private Queue<T> instancesQueue = new Queue<T>();

    public int Size { get; private set; }

    public Pool(T storeItem, int size) {

        poolItem = storeItem;
        Size = size;

        Add(Size);

    }

    private void Add(int count) {

        for (int i = 0; i < count; i++) {

            T newItem = CreateItem();
            instancesQueue.Enqueue(newItem);

        }

    }

    public T GetItem() {

        T deliveryItem = instancesQueue.Dequeue();
        instancesQueue.Enqueue(deliveryItem);

        return deliveryItem;

    }

    protected abstract T CreateItem();

}
