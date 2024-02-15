using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Vector3 FightPosition;
	public Quaternion FightRotation;
	public float Speed;


	private Transform _cameraTransform;
	private Vector3 _startPosition;
	private Quaternion _startRotation;
	private float _time;
	private bool _needToMove;
	private void Start()
	{
		_cameraTransform = Camera.main.transform;
		_startPosition = _cameraTransform.position;
		_startRotation = _cameraTransform.rotation;
	}
	private void FixedUpdate()
	{
		if (_needToMove)
		{
			MoveCamera();
		}
	}
	private void MoveCamera()
	{
		_time += Speed;
		if(_time >= 1f)
		{
			_time = 1f;
			_needToMove = false;
		}
		_cameraTransform.position = Vector3.Lerp(_startPosition, FightPosition, _time);
		_cameraTransform.rotation = Quaternion.Lerp(_startRotation, FightRotation, _time);
	}
	public void StartCameraMovement()
	{
		_needToMove = true;
	}
}
