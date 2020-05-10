using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ToLC.Network
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button continueBtn = null;

        private const string PlayerPrefNameKey = "PlayerName";

        private void Start() => SetUpInputField();

		#region MainMenu / Initiate Login
		private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefNameKey)) { return; }

            string defaultName = PlayerPrefs.GetString(PlayerPrefNameKey);

            nameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {
            continueBtn.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            string playerName = nameInputField.text;

            PhotonNetwork.NickName = playerName;

            PlayerPrefs.SetString(PlayerPrefNameKey, playerName);
            PlayerPrefs.Save();
        }

		#endregion
	}
}

