using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
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
}