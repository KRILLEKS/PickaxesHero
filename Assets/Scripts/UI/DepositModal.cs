using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepositModal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oreName;
    [SerializeField] private GameObject bankMenuGO;

    [SerializeField] private float[] progressReceivedPerOre =
        new float[Constants.fillerIndexes.Length];

    // local variables
    private Bank _bank;
    private Ore _ore;
    private int _fillerIndex;
    private TMP_InputField _inputField;
    private int _amountToDeposit;
    private float _progressToAdd;
    private Slider _slider;

    private void Awake()
    {
        _bank = FindObjectOfType<Bank>();
        _inputField = gameObject.GetComponentInChildren<TMP_InputField>();
        _slider = GetComponentInChildren<Slider>();
    }

    public void SetUp(Ore ore, int fillerIndex)
    {
        _ore = ore;
        _fillerIndex = fillerIndex;
        oreName.text = _ore.name;
        _slider.maxValue = SingleExtractedOresCounter.ores[_ore.index];
        _inputField.text = "0";
    }

    public void DepositButton()
    {
        if (SingleExtractedOresCounter.ores[_ore.index] >= _amountToDeposit)
        {
            // transaction
            SingleExtractedOresCounter.ores[_ore.index] -= _amountToDeposit;
            
            _progressToAdd =
                progressReceivedPerOre[_fillerIndex] * _amountToDeposit;

            _bank.AddProgress(_progressToAdd);
            
            // close menu
            bankMenuGO.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            // TODO: popup menu "not enough resources"
        }
    }

    public void OnSliderValueChange()
    {
        _inputField.text = _slider.value.ToString();
    }

    public void OnInputFieldValueChange()
    {
        if (_inputField.text.Length == 0)
        {
            _inputField.text = "0";
            _inputField.MoveTextEnd(false);
        }
        else
        {
            _amountToDeposit = int.Parse(_inputField.text);
            if (_amountToDeposit > _slider.maxValue)
                _amountToDeposit = Mathf.FloorToInt(_slider.maxValue);
        }

        _inputField.text = _amountToDeposit.ToString();
        _slider.value = _amountToDeposit;
    }
}