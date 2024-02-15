using UnityEngine;

public class CameraRaycastController : MonoBehaviour
{
	public int CharacterLayer;

	private Ray _ray;
	private RaycastHit _hit;
	private Camera _camera;
	private SingleCharacter _selectedCharater;
	private Vector3 _touchPosition;
	private bool _isInputTaken;
	private bool _isCharacterSelected;
	[SerializeField]private bool _isActivated;
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
		if (_isActivated)
		{
			if (Input.GetMouseButton(0))
			{
				if (!_isInputTaken)
				{
					_isInputTaken = true;
					_ray = _camera.ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(_ray, out _hit, CharacterLayer) && _hit.transform.CompareTag(TagManager.GetTag(TagType.Character)))
					{
						_selectedCharater = _hit.transform.GetComponent<SingleCharacter>();
						_selectedCharater.PickUpCharacter();
						_isCharacterSelected = true;
					}
				}
				else
				{
					if (_isCharacterSelected)
					{

						_touchPosition.z = 0f;
						_touchPosition.x = Input.mousePosition.x / 600f;
						_touchPosition.y = Input.mousePosition.y / 600f;
						_selectedCharater.DragCharacter(_touchPosition);
					}
				}
			}
			if (Input.GetMouseButtonUp(0))
			{
				_isInputTaken = false;
				if (_isCharacterSelected)
				{
					_isCharacterSelected = false;
					_selectedCharater.PlaceCharacter();
				}
			}
		}
	}
}