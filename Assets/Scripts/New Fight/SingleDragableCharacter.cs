using System;
using EPOOutline;
using UnityEngine;

public class SingleDragableCharacter : MonoBehaviour
{
    public LayerMask PlaceLayer;
    public int GirlLevel;
    public bool IsRanged;

    private Animator _animator;
    private NewSinglePlace _sittingPlace;
    private NewSinglePlace _selectedPlace;
    private Vector3 _characterPosition;
    private Vector3 _calculatedPosition;
    private Vector3 _startInput;
    private Vector3 _startPositionVector;
    private bool _characterPickedUp;
    private bool _newPlaceSelected;
    private bool _dragStartCalculated;
    private bool _canUpgrade;
    private Plane _boardPlane;
    private Vector3 _startBoardPoint;
    private Outlinable _outlinable;

    private void Start()
    {
        _outlinable = GetComponent<Outlinable>();
        _animator = GetComponentInChildren<Animator>();
        _sittingPlace = GetFloorDown();
        _characterPickedUp = true;
        PlaceCharacter();
    }

    private NewSinglePlace GetFloorDown()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.2f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, PlaceLayer))
        {
            if (hit.transform.CompareTag(TagManager.GetTag(TagType.SinglePlace)))
                return hit.transform.GetComponent<NewSinglePlace>();
        }

        return null;
    }

    public void PickUpCharacter()
    {
        _dragStartCalculated = false;
        _characterPickedUp = true;
        _characterPosition = transform.position;
        _characterPosition.y = 4.4f;
        transform.position = _characterPosition;
        _animator.SetBool("IsFloating", true);
        _outlinable.enabled = true;
    }

    public void DragCharacter(Vector3 inputPosition)
    {
        if (_characterPickedUp)
        {
            if (!_dragStartCalculated)
            {
                _dragStartCalculated = true;
                _startInput = inputPosition;
                _startPositionVector = transform.position;

                _boardPlane = new Plane(Vector3.up, _startPositionVector);

                var startRay = Camera.main.ScreenPointToRay(_startInput);
                _boardPlane.Raycast(startRay, out float enterStart);
                _startBoardPoint = startRay.GetPoint(enterStart);
            }

            var inputRay = Camera.main.ScreenPointToRay(inputPosition);
            if (_boardPlane.Raycast(inputRay, out float enter))
            {
                var overlapBoardPoint = inputRay.GetPoint(enter);

                _calculatedPosition.x = _startPositionVector.x + overlapBoardPoint.x - _startBoardPoint.x;
                _calculatedPosition.z = _startPositionVector.z + overlapBoardPoint.z - _startBoardPoint.z;
            }

            // _calculatedPosition.z = _startInput.x - inputPosition.x + _startPositionVector.z;
            // _calculatedPosition.x = inputPosition.y - _startInput.y + _startPositionVector.x;
            _calculatedPosition.y = transform.position.y;
            transform.position = _calculatedPosition;
        }

        NewSinglePlace tempSelectedPlace = GetFloorDown();
        if (tempSelectedPlace != null && _sittingPlace != tempSelectedPlace)
        {
            if (!tempSelectedPlace.IsTaken)
            {
                _newPlaceSelected = true;
                if (_selectedPlace != null)
                    _selectedPlace.SelectPlace(false);
                _selectedPlace = tempSelectedPlace;
                _selectedPlace.SelectPlace(true);
            }
            else if (tempSelectedPlace.CanMerge(this))
            {
                _canUpgrade = true;
                _newPlaceSelected = true;
                if (_selectedPlace != null)
                    _selectedPlace.SelectPlace(false);
                _selectedPlace = tempSelectedPlace;
                _selectedPlace.SelectPlace(true);
            }
        }
        else
        {
            if (_selectedPlace != null)
                _selectedPlace.SelectPlace(false);
            _newPlaceSelected = false;
            _canUpgrade = false;
        }
    }

    public void PlaceCharacter()
    {
        if (!_characterPickedUp)
            return;

        if (_canUpgrade)
        {
            _sittingPlace.FreePlace();
            _selectedPlace.UpgradeCharacter(this);
            return;
        }

        _animator.SetBool("IsFloating", false);

        _characterPickedUp = false;
        _outlinable.enabled = false;
        if (_newPlaceSelected)
        {
            transform.position = _selectedPlace.GetPlaceSitPosition();
            _selectedPlace.TakePlace(this);
            _selectedPlace.SelectPlace(false);
            _sittingPlace.FreePlace();
            _sittingPlace = _selectedPlace;
        }
        else
        {
            transform.position = _sittingPlace.GetPlaceSitPosition();
            _sittingPlace.TakePlace(this);
            _sittingPlace.SelectPlace(false);
        }
    }
}