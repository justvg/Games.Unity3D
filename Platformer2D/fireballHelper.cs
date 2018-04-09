using UnityEngine;

//Нужен на сцене, чтобы определять,
//в какую сторону лететь файрболу
public class fireballHelper : MonoBehaviour {
    [SerializeField] PlayerMoving player;

    public static bool IsFacingRight;

    private void LateUpdate() {
        IsFacingRight = player.IsFacingRight;
    }
}
