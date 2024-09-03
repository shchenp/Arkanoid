using UnityEngine;

public class InputHandler
{
    public Vector3 GetInput()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
    }
}
