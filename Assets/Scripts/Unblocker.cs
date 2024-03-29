using UnityEngine;
using UnityEngine.UI;

public class Unblocker : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ResourcesManager _resourceManager;
    [SerializeField] private int _cost;
    [SerializeField] private Button _button;

    private const string ButtonPressedKey = "ButtonPressed";
    private const string ImageColorKeyPrefix = "ImageColor_";

    private void Start()
    {
        bool buttonPressed = PlayerPrefs.GetInt(ButtonPressedKey + _button.gameObject.name, 0) == 1;
        if (buttonPressed)
        {
            _button.interactable = false;
        }

        // Извлекаем имя кнопки, к которой привязан данный скрипт
        string buttonName = _button.gameObject.name;
        string imageColorKey = ImageColorKeyPrefix + buttonName;

        if (PlayerPrefs.HasKey(imageColorKey))
        {
            string colorString = PlayerPrefs.GetString(imageColorKey);
            Color savedColor;
            if (ColorUtility.TryParseHtmlString("#" + colorString, out savedColor))
            {
                _image.color = savedColor;
            }
        }
    }

    public void OnClick()
    {
        if (_resourceManager._coins >= _cost)
        {
            _resourceManager.SpendCoins(_cost);
            _resourceManager.ChangeCoinCounter();
            _resourceManager.SavePlayerPrefs();
            _button.interactable = false; // Выключаем текущую кнопку
            var tempColor = _image.color;
            tempColor.a = 1f;
            _image.color = tempColor;
            PlayerPrefs.SetInt(ButtonPressedKey + _button.gameObject.name, 1);

            // Сохраняем цвет для данной кнопки
            string buttonName = _button.gameObject.name;
            string imageColorKey = ImageColorKeyPrefix + buttonName;
            string colorString = ColorUtility.ToHtmlStringRGBA(_image.color);
            PlayerPrefs.SetString(imageColorKey, colorString);

            PlayerPrefs.Save();
        }
    }
}