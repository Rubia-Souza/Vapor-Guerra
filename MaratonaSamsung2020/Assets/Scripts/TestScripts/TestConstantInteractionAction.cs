using UnityEngine;

public class TestConstantInteractionAction : ConstantInteractionObject {

    protected override void OnInteractionCompleated() {

        Debug.Log("Você fica 10 segundos procurando comida nessas caixas, mas não encontra nada");

    }

}
