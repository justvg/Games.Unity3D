using UnityEngine;


public class Camera2DFollow : MonoBehaviour {
    public Transform target;

    public GameObject leftBorder;
    public GameObject rightBorder;

    private float m_OffsetZ;
    private Vector3 m_CurrentVelocity;

    private void Start() {
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    private void Update() {
        if (target.transform.position.x > leftBorder.transform.position.x && target.transform.position.x < rightBorder.transform.position.x) {
            Vector3 aheadTargetPos = target.position + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, 0.1f);
            transform.position = newPos;
        }
    }
}

