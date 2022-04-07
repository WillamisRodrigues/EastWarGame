using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Imports da Photon 2
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NertworkController : MonoBehaviourPunCallbacks
{
    public InputField playerInputName;
    public InputField roomInputName;
    string playerNameTemp;
    string roomNameTemp;

    public GameObject loginWindow;
    public GameObject lobbyWindow;
    public GameObject CanvasWindow;
    public GameObject GameWindow;
    enum windowConnection {Login, Lobby, Game, Canvas};
    enum statusConnection {EnteringLobby, EnterInLobby, EnterInLobbyFail}
    
    
    #region Aula 02
    bool isConected = false;

    public GameObject myPlayer;



    private void Start()
    {
        roomInputName.enabled = false;
        HandleChangingWindows(windowConnection.Login, true);
        HandleChangingWindows(windowConnection.Lobby, false);
        HandleChangingWindows(windowConnection.Game, false);
        SetRandomPlayerName();
    }

    public override void OnConnected()
    {
        Debug.Log("Conex�o Feita com Sucesso");
        HandleChangingWindows(windowConnection.Lobby, true);
        SetRandomRoomName();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Estou no Master, Hora de entrar no Lobby!");
        isConected = true;

    }

    IEnumerator ReturnPing(float time)
    {
        yield return new WaitForSeconds(time);

        if(PhotonNetwork.CloudRegion != "")
        {
            Debug.Log("Server: " + PhotonNetwork.CloudRegion);
            Debug.Log("Ping: " + PhotonNetwork.GetPing());
            Debug.Log("-----------------------------");
        }
       
       
        StartCoroutine(ReturnPing(time));
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Estou No Lobby");
        Debug.Log("Temos " + PhotonNetwork.CountOfRooms + " disponiveis");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("returnCode " + returnCode);
        Debug.Log("message " + message);
        Debug.Log("N�o tem nenhuma room dispon�vel");
        //PhotonNetwork.CreateRoom(roomName);
        CreateOrEnterRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("AAAAAAAAAAAAAAEEEEEEEEE estou na room!");
        Debug.Log("Temos "+PhotonNetwork.CurrentRoom.PlayerCount+" Jogadores Disponiveis");
        HandleChangingWindows(windowConnection.Lobby, false);
       
        HandleChangingWindows(windowConnection.Login, false);
        HandleChangingWindows(windowConnection.Lobby, false);
        HandleChangingWindows(windowConnection.Canvas, false);
        HandleChangingWindows(windowConnection.Game, true);
        CreateProperties();
        CreateNewPlayer();
        // SceneManager.LoadScene("Game");

    }

    public void ButtonClick()
    {
        PhotonNetwork.ConnectUsingSettings();
        //StartCoroutine(ReturnPing(1f));
    }

    public void EnterInLobby()
    {
        if (isConected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    #endregion

    #region Aula 03
        
    void SetRandomPlayerName()
    {
        if (playerInputName == null)
        {
            Debug.Log("N�o tem um campo");
            return;
        }
        
        playerNameTemp = "Player_" + Random.Range(100,999);
        playerInputName.text = playerNameTemp;
    }

    void SetRandomRoomName()
    {
        if (roomInputName == null)
        {
            Debug.Log("N�o tem um campo");
            return;
        }

       roomNameTemp = "Room_East_War" + Random.Range(1, 99);
        roomInputName.text = roomNameTemp;
    }

    public void Login()
    {
        Debug.Log("Bot�o Login Clicado");
        PhotonNetwork.NickName = playerInputName.text;
        HandleChangingWindows(windowConnection.Login, false);
        PhotonNetwork.ConnectUsingSettings();
    }

   public  void CreateOrEnterRoom()
    {
        Debug.Log("Criando a Sala");
        string rooName = roomInputName.text;
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(rooName, roomOptions, TypedLobby.Default);


    }

    public void SearchRoom()
    {
        Debug.Log("Busca Partida Rapida");
        PhotonNetwork.JoinLobby();
    }



    void HandleChangingWindows(windowConnection windowName, bool value)
    {
        switch (windowName)
        {

            case windowConnection.Login:
                if (loginWindow != null)
                    loginWindow.SetActive(value);
                    break;
                
            case windowConnection.Lobby:
                if (lobbyWindow != null)
                    lobbyWindow.SetActive(value);
                break;
            
            case windowConnection.Game:
                GameWindow.SetActive(value);
                break;

            case windowConnection.Canvas:
                CanvasWindow.SetActive(value);
                break;

            default:
                break;
        }
    }

    void CreateNewPlayer()
    {
        GameObject temPlayer = PhotonNetwork.Instantiate(myPlayer.name, myPlayer.transform.position, myPlayer.transform.rotation, 0);
        HandleChangingWindows(windowConnection.Canvas, false);
        temPlayer.GetComponent<PlayerController>().myCanvas = GameWindow;
    }
    #endregion
    #region Hashtable

    void CreateProperties()
    {
        Hashtable playerTempData = new Hashtable();

        playerTempData.Add("Name", PhotonNetwork.NickName);
        playerTempData.Add("Score", 0);
        playerTempData.Add("Id", PhotonNetwork.LocalPlayer.UserId);
        playerTempData.Add("Team", "Blue");
        PhotonNetwork.SetPlayerCustomProperties(playerTempData);
    }

    #endregion
}
