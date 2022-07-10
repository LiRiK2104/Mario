using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _topBorder = 10;
    [SerializeField] private float _bottomBorder = 10;
    [SerializeField] private float _leftBorder = -10;
    [SerializeField] private float _rightBorder = 10;
    
    private Player _player;
    private Vector2 _offset;
    private float _speed = 2;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _offset = transform.position - _player.transform.position;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetPosition =  (Vector2)_player.transform.position + _offset;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
        ClampCameraPosition();
    }
    
    private void ClampCameraPosition()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        float aspect = (float)Screen.width / Screen.height;
        Vector3 halfScreenSize = new Vector3(Camera.main.orthographicSize * aspect, Camera.main.orthographicSize, Camera.main.farClipPlane);

        float x = Mathf.Clamp(cameraPos.x, _leftBorder + halfScreenSize.x, _rightBorder - halfScreenSize.x);
        float y = Mathf.Clamp(cameraPos.y, _bottomBorder + halfScreenSize.y, _topBorder - halfScreenSize.y);
        
        Camera.main.transform.position = new Vector3(x, y, cameraPos.z);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 leftTop = new Vector2(_leftBorder, _topBorder);
        Vector2 rightTop = new Vector2(_rightBorder, _topBorder);
        Vector2 leftBottom = new Vector2(_leftBorder, _bottomBorder);
        Vector2 rightBottom = new Vector2(_rightBorder, _bottomBorder);
        Gizmos.DrawLine(leftTop, rightTop);
        Gizmos.DrawLine(leftTop, leftBottom);
        Gizmos.DrawLine(rightTop, rightBottom);
        Gizmos.DrawLine(leftBottom, rightBottom);
    }

    private void OnValidate()
    {
        float minBordersDistance = 1;

        _topBorder = Mathf.Max(_topBorder, _bottomBorder + minBordersDistance);
        _rightBorder = Mathf.Max(_rightBorder, _leftBorder + minBordersDistance);
    }
}
