using Interfaces;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI.GameOverScreen
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [Inject]
        private void Construct(IDataContainer dataContainer)
        {
            _text.text = dataContainer.GetScore().ToString();
        }
    }
}