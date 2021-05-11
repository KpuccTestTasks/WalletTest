using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletApp : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _loadButtons;
    [SerializeField] private GameObject _saveButtons;
    [SerializeField] private GameObject _currencyButtons;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private Button _inputNameButton;
    [SerializeField] private TMP_InputField _currencyAmountInput;
    [SerializeField] private GameObject _currencyActionButtons;
    [SerializeField] private TextMeshProUGUI _currencyInfoText;

    private Wallet _currentWallet;
    private CurrencyType _currentCurrency;

    private void Start()
    {
        _currentCurrency = CurrencyType.Coin;

        UpdateView();
    }

    public void ApplyName()
    {
        if (string.IsNullOrEmpty(_nameInput.text))
        {
            return;
        }

        _name = _nameInput.text;

        UpdateView();
    }

    private void UpdateView()
    {
        _currencyActionButtons.SetActive(false);
        _loadButtons.SetActive(false);
        _saveButtons.SetActive(false);
        _currencyButtons.SetActive(false);
        _nameInput.gameObject.SetActive(false);
        _inputNameButton.gameObject.SetActive(false);
        _currencyAmountInput.gameObject.SetActive(false);

        if (string.IsNullOrEmpty(_name))
        {
            _nameInput.gameObject.SetActive(true);
            _inputNameButton.gameObject.SetActive(true);
            return;
        }

        if (_currentWallet == null)
        {
            _loadButtons.SetActive(true);
            return;
        }

        _saveButtons.SetActive(true);
        _currencyButtons.SetActive(true);
        _currencyActionButtons.SetActive(true);
        _currencyAmountInput.gameObject.SetActive(true);

        UpdateCurrencyInfoText();
    }

    public void AddCurrency()
    {
        if (int.TryParse(_currencyAmountInput.text, out int amount))
        {
            _currentWallet.AddCurrency(amount, _currentCurrency);
        }

        _currencyAmountInput.text = string.Empty;
        UpdateCurrencyInfoText();
    }

    public void SpendCurrency()
    {
        if (int.TryParse(_currencyAmountInput.text, out int amount))
        {
            _currentWallet.SpendCurrency(amount, _currentCurrency);
        }

        _currencyAmountInput.text = string.Empty;
        UpdateCurrencyInfoText();
    }

    public void ResetCurrency()
    {
        _currentWallet.ResetCurrency(_currentCurrency);

        UpdateCurrencyInfoText();
    }

    public void SetCurrency(int index)
    {
        _currentCurrency = (CurrencyType)index;

        UpdateCurrencyInfoText();
    }

    private void UpdateCurrencyInfoText()
    {
        _currencyInfoText.text = $"You have {_currentWallet.GetCurrencyAmount(_currentCurrency)} {_currentCurrency}";
    }

    #region Save and Load

    public void CreateNewWallet()
    {
        _currentWallet = new Wallet();

        UpdateView();
    }

    public void LoadFromPrefs()
    {
        var loadSystem = new PlayerPrefsSaveLoadSystem();

        _currentWallet = loadSystem.Load<Wallet>(_name);

        UpdateView();
    }

    public void LoadFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, $"{_name}.json");

        var loadSystem = new JsonSaveLoadSystem();

        _currentWallet = loadSystem.Load<Wallet>(path);

        UpdateView();
    }

    public void LoadFromBinary()
    {
        string path = Path.Combine(Application.persistentDataPath, $"{_name}.dat");

        var loadSystem = new BinarySaveLoadSystem();

        _currentWallet = loadSystem.Load<Wallet>(path);

        UpdateView();
    }

    public void SaveAsJsonInPrefs()
    {
        var loadSystem = new PlayerPrefsSaveLoadSystem();

        loadSystem.Save(_name, _currentWallet);
    }

    public void SaveAsJson()
    {
        string path = Path.Combine(Application.persistentDataPath, $"{_name}.json");

        var loadSystem = new JsonSaveLoadSystem();

        loadSystem.Save(path, _currentWallet);
    }

    public void SaveAsBinary()
    {
        string path = Path.Combine(Application.persistentDataPath, $"{_name}.dat");

        var loadSystem = new BinarySaveLoadSystem();

        loadSystem.Save(path, _currentWallet);
    }
    #endregion
}
