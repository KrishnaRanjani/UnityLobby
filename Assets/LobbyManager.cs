using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Photon.PunBehaviour{

    // Start is called before the first frame update

    public GameObject[] EntryButtons;

    string GameVersion = "1";

    static string PlayerNamePreKey = "PlayerName";

    public TMP_InputField NameInputField;

    string RoomName = "UBC";

    void Start()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings(GameVersion);
        
        }

        string DefaultName = "";
        //InputField NameInputField = GetComponent<InputField>();
        if (NameInputField != null)
        {
            if (PlayerPrefs.HasKey(PlayerNamePreKey))
            {
                DefaultName = PlayerPrefs.GetString(PlayerNamePreKey);
                NameInputField.text = DefaultName;
            }
        
        }

        PhotonNetwork.playerName = DefaultName;
    }

    public override void OnConnectedToMaster()
    {
        print("connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("connected to lobby"); 
    }

    public override void OnJoinedRoom()
    {
        print("Joined Room");

    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        print(newPlayer.NickName + "Has Joined");

        if (PhotonNetwork.isMasterClient && PhotonNetwork.room.PlayerCount >= 2)
        {

            PhotonNetwork.LoadLevel(1);

        }
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom( RoomName, new RoomOptions() { MaxPlayers = 4 }, null);
        print("creating and joining Room");

    }

    public void GetRoomList()
    {
        RoomInfo[] RoomList = PhotonNetwork.GetRoomList();

       

        if (RoomList.Length > 0) 
        {
            for (int Index = 0;
                Index < RoomList.Length;
                ++Index)
            {

               
                RoomInfo Room = RoomList[Index];
                GameObject Entry = EntryButtons[Index];

                TextMeshProUGUI EntryText = Entry.GetComponentInChildren<TextMeshProUGUI>();

                EntryText.text = Room.Name;

                
            }
        
        }
    
    }

    public void JoinRoom(TextMeshProUGUI EntryText)
    {
        PhotonNetwork.JoinRoom(EntryText.text);
    }

    public void SetPlayerName(string value) 
    {
        PhotonNetwork.playerName = value + " ";
        PlayerPrefs.SetString(PlayerNamePreKey, value);

    }


    public void SetRoomName(string value)
    {
        RoomName = value;
    }
}
