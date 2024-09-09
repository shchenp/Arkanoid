using Interfaces;
using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour, IParent
{
    [SerializeField] private float _speed;
    [SerializeField] private float _bonusScaleMultiplicator;
    [SerializeField] private float _leftBorderX;
    [SerializeField] private float _rightBorderX;
    
    private InputHandler _inputHandler;
    
    [Inject]
    private void Construct()
    {
        _inputHandler = new InputHandler();
    }

    private void Update()
    {
        var nextPosition = transform.position + _speed * Time.deltaTime * _inputHandler.GetInput();
        
        nextPosition.x = Mathf.Clamp(nextPosition.x, _leftBorderX, _rightBorderX);
        transform.position = nextPosition;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GlobalConstants.BONUS_TAG))
        {
            IncreaseScale();
        }
    }

    private void IncreaseScale()
    {
        var newScale = transform.localScale;
        newScale.x *= _bonusScaleMultiplicator;

        transform.localScale = newScale;
    }

    Transform IParent.GetTransform()
    {
        return transform;
    }
}