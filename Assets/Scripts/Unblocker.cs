using UnityEngine;
using UnityEngine.UI;

public class Unblocker : MonoBehaviour
{
    [SerializeField] private Image _imageBerry;
    [SerializeField] private ResourcesManager _resourceManager;
    [SerializeField] private int _berryCost;
    [SerializeField] private Button _berryButton;

    private const string ButtonPressedKey = "ButtonPressed";
    private const string ImageColorKeyPrefix = "ImageColor_";

    private void Start()
    {
        bool buttonPressed = PlayerPrefs.GetInt(ButtonPressedKey + _berryButton.gameObject.name, 0) == 1;
        if (buttonPressed)
        {
            _berryButton.interactable = false;
        }

        // ��������� ��� ������, � ������� �������� ������ ������
        string buttonName = _berryButton.gameObject.name;
        string imageColorKey = ImageColorKeyPrefix + buttonName;

        if (PlayerPrefs.HasKey(imageColorKey))
        {
            string colorString = PlayerPrefs.GetString(imageColorKey);
            Color savedColor;
            if (ColorUtility.TryParseHtmlString("#" + colorString, out savedColor))
            {
                _imageBerry.color = savedColor;
            }
        }
    }

    public void OnClick()
    {
        if (_resourceManager.Coins >= _berryCost)
        {
            _resourceManager.SpendCoins(_berryCost);
            _resourceManager.ChangeCoinCounter();
            _resourceManager.SavePlayerPrefs();
            _berryButton.interactable = false; // ��������� ������� ������
            var tempColor = _imageBerry.color;
            tempColor.a = 1f;
            _imageBerry.color = tempColor;
            PlayerPrefs.SetInt(ButtonPressedKey + _berryButton.gameObject.name, 1);

            // ��������� ���� ��� ������ ������
            string buttonName = _berryButton.gameObject.name;
            string imageColorKey = ImageColorKeyPrefix + buttonName;
            string colorString = ColorUtility.ToHtmlStringRGBA(_imageBerry.color);
            PlayerPrefs.SetString(imageColorKey, colorString);

            PlayerPrefs.Save();
        }
    }
}