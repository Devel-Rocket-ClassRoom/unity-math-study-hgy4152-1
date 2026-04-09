using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Camera cam;
    private Vector3 clickPoint;
    private Vector3 startPoint;
    private bool isDrag;
    private void Start()
    {
        cam = Camera.main;
        
        Vector3 cubePos = transform.position;

        cubePos.y = SetHeight(cubePos);

        transform.position = cubePos;
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
                    isDrag = true;
                    startPoint = transform.position;
                    // 큐브 클릭 포인트 넘어 땅을 찍은 후 그 지점에서 큐브까지의 벡터 오프셋 저장
                    if (Physics.Raycast(ray, out RaycastHit groundHit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                    {
  
                        clickPoint = transform.position - groundHit.point;
                        clickPoint.y = 0; // 높이 차이는 오프셋에서 제거
                    }
                }
                    
            }
        }

        if (Input.GetMouseButton(0) && isDrag)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // 바닥 레이어에만 레이를 쏴서 좌표를 얻음
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 nextPos = hit.point + clickPoint;

                nextPos.y = SetHeight(nextPos);
                transform.position = nextPos;
            }
        }

        // 드래그 종료
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;

            // DropPoint 일 시 떨구고 아닐 시 원래위치로 러프하게 이동

        }
    }


    private float SetHeight(Vector3 pos)
    {
        // Terrain 굴곡면 따라서 y 설정
        float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
        float finalHeight = terrainHeight + Terrain.activeTerrain.transform.position.y;

        float y = finalHeight + transform.localScale.y / 2f;

        return y;

    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
