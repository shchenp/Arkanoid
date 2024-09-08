using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public abstract class Viewer : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _value;

        protected void UpdateView(int context)
        {
            _value.text = context.ToString();
        }
    }
}