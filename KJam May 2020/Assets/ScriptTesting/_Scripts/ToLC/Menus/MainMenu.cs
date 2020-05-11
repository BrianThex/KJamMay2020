using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;


namespace ToLC.Menues
{
    public class MainMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject findMatchPanel = null;
        [SerializeField] private GameObject waitingStatusPanel = null;
        [SerializeField] private TextMeshProUGUI waitingStatusText = null;

        [SerializeField] private GameObject mainMenu = null;
        [SerializeField] private GameObject lobby = null;

        [SerializeField] private GameObject lobbyWaitingText = null;
        [SerializeField] private GameObject lobbyStart = null;

        [SerializeField] private GameObject p1Inventory = null;
        [SerializeField] private GameObject p2Inventory = null;

        [SerializeField] private GameObject tradeMenu = null;

        private bool isConnecting = false;

        public bool isInGame = true;

        private const string GameVersion = "v0.0.0.1";
        private const int MaxPlayersPerRoom = 2;

        private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

        private void Update()
        {
            if (isInGame)
            {

                //if (Input.GetKeyDown(KeyCode.I))
                //{
                //    //photonView.RPC("SetInventoryMenu", RpcTarget.All);

                //    SetInventoryMenu();

                //    //if (p1Inventory.activeInHierarchy)
                //    //{
                //    //    photonView.RPC("CloseGlobalMenu", RpcTarget.All, "P1Inventory");
                //    //}
                //    //else
                //    //{
                //    //    photonView.RPC("OpenGlobalMenu", RpcTarget.All, "P1Inventory");
                //    //}
                //}
            }
        }

        [PunRPC]
        public void SetInventoryMenu()
        {
            if (p1Inventory.activeInHierarchy)
            {
                p1Inventory.SetActive(false);
            }
            else
            {
                p1Inventory.SetActive(true);
            }
        }

        public void FindOpponent()
        {
            isConnecting = true;

            findMatchPanel.SetActive(false);
            waitingStatusPanel.SetActive(true);

            waitingStatusText.text = "Searching...";

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Master");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            waitingStatusPanel.SetActive(false);
            findMatchPanel.SetActive(true);

            Debug.Log($"Dissconnected due to: {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No clients are waiting for an opponent, creating a new room");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Client successfully joined a room");

            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if (playerCount != MaxPlayersPerRoom)
            {
                waitingStatusText.text = "Waiting For Opponent...";
                Debug.Log("Client is waiting for an opponent");
            }
            else
            {
                waitingStatusText.text = "Opponent Found";
                Debug.Log("Matching is ready to begin");
                setLobby();
            }
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;

                waitingStatusText.text = "Opponent Found";
                Debug.Log("Matching is ready to begin");

                setMasterLobby();
            }
        }

        public void LoadCatacombs()
        {
            photonView.RPC("CloseGlobalMenu", RpcTarget.All, "Lobby");
            photonView.RPC("CloseGlobalMenu", RpcTarget.All, "P1Inventory");
            photonView.RPC("CloseGlobalMenu", RpcTarget.All, "P2Inventory");

            PhotonNetwork.LoadLevel("SimpleLevel");

            isInGame = true;
        }

        public void LoadCathedral()
        {
            photonView.RPC("CloseGlobalMenu", RpcTarget.All, "Lobby");
            photonView.RPC("CloseGlobalMenu", RpcTarget.All, "P1Inventory");
            photonView.RPC("CloseGlobalMenu", RpcTarget.All, "P2Inventory");

            PhotonNetwork.LoadLevel("Cathedral_Ward");

            isInGame = true;
        }

        private void setLobby()
        {
            mainMenu.SetActive(false);

            p1Inventory.SetActive(true);
            p2Inventory.SetActive(true);

            lobby.SetActive(true);
            lobbyWaitingText.SetActive(true);
            lobbyStart.SetActive(false);
        }

        private void setMasterLobby()
        {
            mainMenu.SetActive(false);

            p1Inventory.SetActive(true);
            p2Inventory.SetActive(true);

            lobby.SetActive(true);
            lobbyWaitingText.SetActive(false);
            lobbyStart.SetActive(true);
        }

        public void OpenTradeMenu()
        {
            photonView.RPC("CloseGlobalMenu", RpcTarget.Others, "LobbyWaitingStatus");
            photonView.RPC("OpenGlobalMenu", RpcTarget.All, "Trade");
            lobbyStart.SetActive(false);
        }

        public void CloseTradeMenu()
        {
            photonView.RPC("CloseTrade", RpcTarget.MasterClient);
        }

        [PunRPC]
        public void CloseTrade()
        {
            photonView.RPC("OpenGlobalMenu", RpcTarget.Others, "LobbyWaitingStatus");
            photonView.RPC("CloseGlobalMenu", RpcTarget.All, "Trade");
            lobbyStart.SetActive(true);
        }

        [PunRPC]
        public void OpenGlobalMenu(string menu)
        {
            switch (menu)
            {
                case "Trade":
                    tradeMenu.SetActive(true);
                    break;
                case "LobbyWaitingStatus":
                    lobbyWaitingText.SetActive(true);
                    break;
                case "Lobby":
                    lobby.SetActive(true);
                    break;
                case "P1Inventory":
                    p1Inventory.SetActive(true);
                    break;
                case "P2Inventory":
                    p2Inventory.SetActive(true);
                    break;
            }
        }

        [PunRPC]
        public void CloseGlobalMenu(string menu)
        {
            switch (menu)
            {
                case "Trade":
                    tradeMenu.SetActive(false);
                    break;
                case "LobbyWaitingStatus":
                    lobbyWaitingText.SetActive(false);
                    break;
                case "Lobby":
                    lobby.SetActive(false);
                    break;
                case "P1Inventory":
                    p1Inventory.SetActive(false);
                    break;
                case "P2Inventory":
                    p2Inventory.SetActive(false);
                    break;
            }
        }
    }
}

