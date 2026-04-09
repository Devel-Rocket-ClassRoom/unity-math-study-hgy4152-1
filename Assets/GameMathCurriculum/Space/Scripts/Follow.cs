using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.5f;
    public float rotateSpeed = 3f;
    public Vector3 cameraOffset = new Vector3(0f, 5f, -5f);
    public Quaternion rotOffset = Quaternion.Euler(45f, 0f, 0f);

    private float elapsedTime;
    private Camera cam;
    private Vector3 currentVelocity;


    public void Start()
    {
        cam = Camera.main;
    }

    public void LateUpdate()
    {

        // 타겟 회전
        float angle = target.eulerAngles.y;
        Quaternion orbitRotation = Quaternion.AngleAxis(angle, Vector3.up);

        // 떨어져야하는 거리 그대로 회전한 값
        Vector3 rotatedOffset = orbitRotation * cameraOffset;
        Vector3 targetPos = target.position + rotatedOffset;

        // 현재 회전 상태에 추가 회전 더해준 값
        Quaternion targetRot = orbitRotation * rotOffset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);

    }
}
