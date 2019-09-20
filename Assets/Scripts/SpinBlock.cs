using Assets.DataStructure;
using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Provides the functionality to move a gameobject by the total counts of given <c>SpinBlock</c>.
/// </summary>
public class SpinBlock : MonoBehaviour
{
    private Vector3 _startPosition,
                    _firstPosition,
                    _endPosition;

    private Transform _firstSlot;

    private int _slotCount,
                _direction,
                _groupIndex,
                _blockIndex = -1;

    private float _iconHeight,
                  _totalLength,
                  _scaleFactor,
                  _speedFactor,
                  _minVelocity,
                  _minBreakVelocity,
                  _maxVelocity,
                  _velocity,
                  _acceleration,
                  _delayTime = 0;

    private Vector3 directionVector = Vector3.down;

    private Material[] _materials;
    private Renderer _renderer;
    private BlockMaterial _material;

    private SpinSettings _spinSettings;

    private bool _isRunning,
                 _preEndState = false;

    private void Awake()
    {
        _firstSlot = transform.parent.GetChild(0);
        _slotCount = transform.parent.childCount;
        _iconHeight = transform.localScale.y;
        _totalLength = _slotCount * _iconHeight;
        _spinSettings = base.transform.parent.gameObject.GetComponent<SpinSettings>();
        _scaleFactor = _iconHeight;

        _firstPosition = transform.position;
        _startPosition = transform.parent.GetChild(0).position;
        _endPosition = new Vector3(_startPosition.x, _startPosition.y - _totalLength, _startPosition.y);

        _blockIndex = transform.GetSiblingIndex();
        _groupIndex = transform.parent.GetSiblingIndex();

        if (_spinSettings.Speed == 0)
        {
            _spinSettings.Speed = 1;
        }

        _speedFactor = 1 + _spinSettings.Speed;

        _maxVelocity = _scaleFactor * 0.05f * (_speedFactor);
        _minVelocity = _scaleFactor * 0.05f * (_speedFactor);
        _minBreakVelocity = _scaleFactor * 0.008f * (_speedFactor);

        _velocity = _scaleFactor * 0 * (_speedFactor);
        _acceleration = _scaleFactor * 0.003f * (_speedFactor);

        // TODO: Implement spin direction functionality.
        _direction = -1;

        _renderer = transform.GetComponent<Renderer>();
        _materials = transform.GetComponent<MeshRenderer>().materials;
        transform.GetComponent<MeshRenderer>().materials = _materials;

        if (_material == null)
        {
            _material = new BlockMaterial("1", 1);
        }

        ChangeTexture(_material.MaterialName);

        _delayTime = _spinSettings.DelayTime;
    }

    private bool MoveFrame()
    {
        if (!_preEndState)
        {
            if (Mathf.Abs(_spinSettings.MovedRange) < _totalLength * _spinSettings.Rounds * 0.95f - (0.95f * _spinSettings.Speed / 10))
            {
                IncreaseVelocity();
            }
            else
            {
                DecreaseVelocity();
            }
            transform.position = transform.position + directionVector * _velocity;
        }

        if (_firstSlot.transform == transform)
        {
            _spinSettings.IncreaseMovedRange(_velocity);
        }

        if (((Mathf.Abs(_totalLength * _spinSettings.Rounds * 1.07f) - _spinSettings.MovedRange <= 0.40f) && _spinSettings.Counter >= _spinSettings.Rounds) && !_preEndState)
        {
            _preEndState = true;
            SoundManager.manager.PlayStopSound();
        }

        if (_preEndState)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _acceleration * 10, transform.position.z);
            var deltaStartPosition = Mathf.Abs(_firstPosition.y - transform.position.y);
            if ((deltaStartPosition <= 0.09f))
            {
                transform.position = _firstPosition;
                return true;
            }
        }

        // Increases round by 1.
        if (_firstSlot.position.y <= _endPosition.y)
        {
            _spinSettings.IncreaseRoundCounter(1);
        }

        if (transform.position.y <= _endPosition.y && !_preEndState)
        {
            var deltaEndPosition = Mathf.Abs(transform.position.y) - Mathf.Abs(_endPosition.y);
            transform.position = new Vector3(_startPosition.x, _startPosition.y - deltaEndPosition, _startPosition.z);
            var random = UnityEngine.Random.Range(1, 10);
            _material = new BlockMaterial(random.ToString(), random);
            SetTexture(_material);
        }

        return false;
    }

    private void ChangeTexture(string materialName)
    {
        Texture2D texture = Resources.Load(materialName, typeof(Texture2D)) as Texture2D;
        _renderer.material.mainTexture = texture;
        // Makes the gameobject visible within the window shader.
        _renderer.material.shader = Shader.Find("Custom/ObjectToPit");
    }

    private void IncreaseVelocity()
    {
        if (_velocity <= _maxVelocity)
        {
            _velocity += _acceleration;
        }
    }

    private void DecreaseVelocity()
    {
        if (_velocity > _minVelocity)
        {
            _velocity -= _acceleration;
        }
    }

    /// <summary>
    /// Event handler which will be executed when a spin has finished.
    /// </summary>
    public event EventHandler<BlockDataEventArgs> SpinFinished;
    protected virtual void OnSpinFinished(BlockDataEventArgs args)
    {
        EventHandler<BlockDataEventArgs> handler = SpinFinished;
        if (handler != null)
        {
            handler(this, args);
        }
    }
           
    // The index of the group in which a <c>SpinBlock</c> is.
    public int GroupIndex
    {
        get
        {
            return _groupIndex;
        }
    }

    /// <summary>
    /// The index of the given <c>SpinBlock</c>.
    /// </summary>
    public int BlockIndex
    {
        get
        {
            return _blockIndex;
        }
    }

    /// <summary>
    /// Gives the state if a block is still moving.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
    }

    /// <summary>
    /// The texture information of the block.
    /// </summary>
    public BlockMaterial Material
    {
        get
        {
            return _material;
        }
    }

    /// <summary>
    /// The execution method wich starts the block movement.
    /// </summary>
    //public void Run()
    //{
    //    if (IsRunning)
    //    {
    //        return;
    //    }

    //    StartCoroutine(StartMovement());
    //}

    /// <summary>
    /// Starts the block to move.
    /// </summary>
    public IEnumerator StartMovement()
    {
        _isRunning = true;
        yield return new WaitForSeconds(_delayTime);
        ResetData();

        var result = false;

        while (!result)
        {
            result = MoveFrame();
            yield return null;
        }
                
        OnSpinFinished(new BlockDataEventArgs(Material, GroupIndex, BlockIndex));
        _isRunning = false;
    }
    
    /// <summary>
    /// Resets the gameobject data.
    /// </summary>
    public void ResetData()
    {
        _velocity = _scaleFactor * 0 * (_speedFactor);
        _acceleration = _scaleFactor * 0.002f * (_speedFactor);

        if (_firstSlot.transform == transform)
        {
            _spinSettings.Reset();
        }

        directionVector = Vector3.down;

        switch (_direction)
        {
            case 1:
                directionVector = Vector3.up;
                break;
            default:
                directionVector = Vector3.down;
                break;
        }
        _preEndState = false;
    }

    /// <summary>
    /// Sets the gameobject to a new material.
    /// </summary>
    /// <param name="material">The material with the specific texture information</param>
    public void SetTexture(BlockMaterial material)
    {
        _material = material;
        ChangeTexture(_material.MaterialName);
    }
}