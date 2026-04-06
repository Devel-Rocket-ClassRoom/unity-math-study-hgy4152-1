// =============================================================================
// Assignment_PlanetOrbit.cs
// -----------------------------------------------------------------------------
// Matrix4x4.TRS와 MultiplyPoint3x4로 행성-위성 궤도를 구현하는 시스템
// =============================================================================

using UnityEngine;
using TMPro;
public class Assignment_PlanetOrbit : MonoBehaviour
{

    [Header("=== 행성 설정 ===")]
    [Tooltip("행성이 공전할 중심점")]
    [SerializeField] private Vector3 orbitCenter = Vector3.zero;

    [Tooltip("중심점을 기준으로 한 행성의 공전 반경")]
    [Range(3f, 15f)]
    [SerializeField] private float planetOrbitRadius = 5f;

    [Tooltip("행성의 공전 속도 (도/초)")]
    [Range(10f, 180f)]
    [SerializeField] private float planetOrbitSpeed = 30f;

    [Header("=== 위성 설정 ===")]
    [Tooltip("행성을 공전하는 위성 오브젝트")]
    [SerializeField] private Transform satellite;

    [Tooltip("행성을 기준으로 한 위성의 공전 반경")]
    [Range(1f, 5f)]
    [SerializeField] private float satelliteOrbitRadius = 2f;

    [Tooltip("위성의 공전 속도 (도/초)")]
    [Range(30f, 360f)]
    [SerializeField] private float satelliteOrbitSpeed = 90f;

    [Header("=== UI 연결 ===")]
    [Tooltip("궤도 정보를 표시할 TMP_Text")]
    [SerializeField] private TMP_Text uiText;

    [Header("=== 디버그 정보 (읽기 전용) ===")]
    [Tooltip("행성의 월드 좌표")]
    [SerializeField] private Vector3 planetWorldPos = Vector3.zero;

    [Tooltip("위성의 로컬 오프셋 (행성 기준, 행렬 계산 결과)")]
    [SerializeField] private Vector3 satelliteLocalPos = Vector3.zero;

    [Tooltip("위성의 월드 좌표")]
    [SerializeField] private Vector3 satelliteWorldPos = Vector3.zero;

    private void Update()
    {
        // TODO

        // 행성 공전
        /*
        
        float angle = Mathf.PI * 2 * planetOrbitRadius * (planetOrbitSpeed / 360f) * Time.time;
        Vector3 planetVector = new Vector3(planetOrbitRadius * Mathf.Cos(angle), 0f, planetOrbitRadius * Mathf.Sin(angle));

        Vector3 pos = orbitCenter;
        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
        Vector3 scl = new Vector3(1f, 1f, 1f);

        Matrix4x4 trsMatrix = Matrix4x4.TRS(pos, rot, scl);
        
        planetWorldPos =  trsMatrix.MultiplyPoint(planetVector) ;

        transform.position = planetWorldPos;


        // 위성 공전

        float angleSatellite = Mathf.PI * 2 * satelliteOrbitRadius * (satelliteOrbitSpeed / 360f) * Time.time;
        Vector3 satelliteVector = new Vector3(satelliteOrbitRadius * Mathf.Cos(angleSatellite), 0f, satelliteOrbitRadius * Mathf.Sin(angleSatellite));

        Vector3 posSatellite = planetWorldPos;
        Quaternion rotSatellite = Quaternion.Euler(0f, 0f, 0f);
        Vector3 sclSatellite = new Vector3(1f, 1f, 1f);

        Matrix4x4 trsMatrixSatellite = Matrix4x4.TRS(posSatellite, rotSatellite, sclSatellite);

        satelliteLocalPos = trsMatrixSatellite.MultiplyPoint(satelliteVector);

        satellite.position = satelliteLocalPos;
        */

        // 삼각함수 없이

        float planetAngle = Time.time * planetOrbitSpeed;

        Matrix4x4 planetMatrix = Matrix4x4.TRS(orbitCenter, Quaternion.Euler(0f, planetAngle, 0f), Vector3.one);

        transform.position = planetMatrix.MultiplyPoint(new Vector3(planetOrbitRadius, 0f, 0f)); // 원 위의 한 점


        float satelliteAngle = Time.time * satelliteOrbitSpeed;

        Matrix4x4 satelliteMatrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0f, satelliteAngle, 0f), Vector3.one);

        satellite.position = satelliteMatrix.MultiplyPoint(new Vector3(satelliteOrbitRadius, 0f, 0f)); // 원 위의 한 점

        UpdateUI();
    }

    private void OnDrawGizmos()
    {
        if (!enabled) return;

        VectorGizmoHelper.DrawCircleXZ(orbitCenter, planetOrbitRadius, new Color(1f, 1f, 0f, 0.3f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(orbitCenter, transform.position);

        if (satellite != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, satellite.position);

            VectorGizmoHelper.DrawCircleXZ(transform.position, satelliteOrbitRadius,
                new Color(0f, 1f, 1f, 0.3f));
        }

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(orbitCenter, 0.3f);
    }

    private void UpdateUI()
    {
        if (uiText == null) return;

        uiText.text =
            $"[과제] 행성-위성 궤도\n" +
            $"\n<color=yellow>행성 월드 좌표:</color>\n" +
            $"  ({planetWorldPos.x:F2}, {planetWorldPos.y:F2}, {planetWorldPos.z:F2})\n" +
            $"\n<color=cyan>위성 로컬 오프셋 (행성 기준):</color>\n" +
            $"  ({satelliteLocalPos.x:F2}, {satelliteLocalPos.y:F2}, {satelliteLocalPos.z:F2})\n" +
            $"\n<color=cyan>위성 월드 좌표:</color>\n" +
            $"  ({satelliteWorldPos.x:F2}, {satelliteWorldPos.y:F2}, {satelliteWorldPos.z:F2})";
    }
}
