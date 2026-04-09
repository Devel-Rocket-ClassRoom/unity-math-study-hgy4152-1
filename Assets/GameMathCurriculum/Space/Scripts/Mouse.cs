using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Camera cam;
    private Vector3 cubePos;
    private Transform cube;
    private void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
        // 클릭 시작 시 큐브 선택 
        // ray 가 큐브 표면을 쏘기 때문에 누름과 동시에 마우스 위치에서 끌기를 하면 뚝뚝 끊김
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Cube"))
                {
                    cube = hit.transform;

                    // [수정] 바닥과의 오프셋을 정확히 구하기 위해 
                    // 현재 마우스 위치가 '바닥'의 어디를 가리키는지 다시 체크합니다.
                    if (Physics.Raycast(ray, out RaycastHit groundHit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                    {
                        // 큐브의 현재 위치와 마우스가 가리키는 바닥 위치의 차이(XZ만)
                        cubePos = cube.position - groundHit.point;
                        cubePos.y = 0; // 높이 차이는 오프셋에서 제거
                    }
                }
                    
            }
        }

        if (Input.GetMouseButton(0) && cube != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // 바닥 레이어에만 레이를 쏴서 좌표를 얻음
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 nextPos = hit.point + cubePos;

                // Terrain 굴곡 따라 해당 좌표의 높이를 올려주는것
                float terrainHeight = Terrain.activeTerrain.SampleHeight(nextPos);
                float finalHeight = terrainHeight + Terrain.activeTerrain.transform.position.y;

                nextPos.y = finalHeight + cube.localScale.y/2f; // 원래 높이 유지
                cube.position = nextPos;
            }
        }

        // 드래그 종료
        if (Input.GetMouseButtonUp(0)) 
            cube = null; 
    }
}
