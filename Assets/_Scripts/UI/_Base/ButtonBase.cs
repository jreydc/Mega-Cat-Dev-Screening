using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PuzzleBubble.UI
{
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected TMP_Text _buttonLabel;

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _buttonLabel = gameObject.GetComponentInChildren<TMP_Text>();
            _button.onClick.AddListener(
                () => { OnClicked(); }
            );
        }

        protected virtual void OnClicked() => Debug.Log(_buttonLabel.text + " was clicked");
    }
}
