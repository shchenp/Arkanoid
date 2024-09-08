using System;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public abstract class Viewer : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _value;
        
        protected IDisposable _subscription;

        protected void UpdateView(int context)
        {
            _value.text = context.ToString();
        }
        
        protected void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}