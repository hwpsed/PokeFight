using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Firebase;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;
    public GameObject playButton;
    public GameObject playDisableButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this; // Singleton
    }

    // Start is called before the first frame update
    async void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // connect to master server
        await PokemonSpawner.GetPokemon("Bulbasaur");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        PhotonNetwork.AutomaticallySyncScene = true;
        playDisableButton.SetActive(false);
        playButton.SetActive(true);
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        int randomRoomId = Random.Range(0, 100000);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        PhotonNetwork.CreateRoom("Room " + randomRoomId, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public void OnPlayButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
        playButton.SetActive(false);
        cancelButton.SetActive(true);
    }

    public void OnCancelButtonClicked()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
        playButton.SetActive(true);
        cancelButton.SetActive(false);
    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        SceneManager.LoadScene(2);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.CurrentRoom.Players.Count == 2)
        {
            StartGame();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
