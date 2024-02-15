using UnityEngine;

public class NewCameraRaycastController : MonoBehaviour
{
    public int CharacterLayer;

    [SerializeField] private bool _isActivated;

    private Ray _ray;
    private RaycastHit _hit;
    private Camera _camera;
    private SingleDragableCharacter _selectedCharacter;
    private Vector3 _touchPosition;
    private bool _isCharacterSelected;

    private void Awake()
    {
        _touchPosition = new Vector3();
        _camera = FindObjectOfType<Camera>();
    }

    public void ActivateController(bool active)
    {
        _isActivated = active;
    }

    private void Update()
    {
        if (!_isActivated)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, CharacterLayer) &&
                _hit.transform.CompareTag(TagManager.GetTag(TagType.Character)))
            {
                _selectedCharacter = _hit.transform.GetComponent<SingleDragableCharacter>();
                _selectedCharacter.PickUpCharacter();
                _isCharacterSelected = true;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (_isCharacterSelected)
            {
                _touchPosition.z = 0f;
                _touchPosition.x = Input.mousePosition.x;// / 600f;
                _touchPosition.y = Input.mousePosition.y;// / 600f;
                _selectedCharacter.DragCharacter(_touchPosition);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isCharacterSelected)
            {
                _isCharacterSelected = false;
                _selectedCharacter.PlaceCharacter();
            }
        }
    }
}