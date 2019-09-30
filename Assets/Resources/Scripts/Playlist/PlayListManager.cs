using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayListManager : MonoBehaviour
{
    public SpotifyPlayer spotifyPlayer;

    public GameObject playerCanvas;
    public GameObject playlistStuff;

    public Playlist allSongsPlaylist;

    public List<AudioClip> allSongs;
    List<RegularSong> allRegularSongs;
    public List<Playlist> listOfPlaylists;


    //Stuff to add song to playlist
    public Playlist playlistToAddSongTo;
    public AudioClip songToAddToPlaylist;
    public RegularSong regularSongToAddToPlaylist;

    public Playlist currentPlayList;
    public Playlist nextPlaylist;

    public Dropdown playlistDropdown;

    public GameObject playlistPrefab;

    public GameObject playlistRoot;
    public GameObject songRoot;

    public GameObject deletePlaylistButton;

    public GameObject playListScreen;
    public GameObject addToPlaylistSelection;
    public Dropdown addToPlaylistSelectionDropdown;

    public InputField newPlaylistName;

    public void Awake()
    {
        // Get all regular songs
        GameObject[] songPrefabs = Resources.LoadAll<GameObject>("Prefabs/Songs");
        allRegularSongs = new List<RegularSong>();

        foreach (GameObject sp in songPrefabs)
            allRegularSongs.Add(sp.GetComponent<RegularSong>());

        foreach (RegularSong song in allRegularSongs)
        {
            //allSongsPlaylist.playListData.songs.Add(song);
            allSongsPlaylist.playlistSongs.Add(song);
        }

        /*
        //Add all the songs to the all songs playlist
        foreach (AudioClip song in allSongs)
        {
            //allSongsPlaylist.playListData.songs.Add(song);
            allSongsPlaylist.songs.Add(song);
        }*/

        addToPlaylistSelection.SetActive(false);

        allSongsPlaylist.switchPlaylist();

        //Temporary thing for playlist to add songs to
        playlistToAddSongTo = allSongsPlaylist;

        listOfPlaylists.Add(allSongsPlaylist);

        spotifyPlayer.currentPlaylist = currentPlayList;

        playlistStuff.SetActive(false);
    }

    public void CreatePlaylist()
    {
        if (newPlaylistName.text == string.Empty)
            return;
        PlayListData playListData = new PlayListData();


        GameObject obj = Instantiate(playlistPrefab, playlistRoot.transform);

        Playlist playlist = obj.GetComponentInChildren<Playlist>();

        playlist.playListData = playListData;
        playlist.buttonText.text = newPlaylistName.text;
        playlist.playlistName = newPlaylistName.text;
        playListData.name = newPlaylistName.text;

        listOfPlaylists.Add(playlist);

        //Add playlist to the list of playlists when adding songs
        
        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
        dropdownOptions.AddRange(playlistDropdown.options);
        playlistDropdown.options.Clear();

        dropdownOptions.Add(new Dropdown.OptionData(newPlaylistName.text));
        playlistDropdown.AddOptions(dropdownOptions);

        //SwitchPlaylist(playlist); niet switchen want dat gaat de gebruiker echt verward maken
    }

    public void SwitchPlaylist(Playlist selectedPlaylist)
    {
        if (currentPlayList != null)
        {
            //currentPlayList.deletePlaylistButton.gameObject.SetActive(false);

            foreach (GameObject obj in currentPlayList.songGO)
            {
                Destroy(obj);
            }
        }

        currentPlayList = selectedPlaylist;

        if (selectedPlaylist.isGeneralPlaylist)
        {
            deletePlaylistButton.SetActive(false);
        }

        else
        {
            deletePlaylistButton.SetActive(true);
        }
    }


    public void DeletePlaylist()//Playlist playList)
    {
        listOfPlaylists.Remove(currentPlayList);//playList);

        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
        dropdownOptions.AddRange(playlistDropdown.options);

        Dropdown.OptionData optionToRemove = null;
        foreach (Dropdown.OptionData dropdown in dropdownOptions)
        {

            if (dropdown.text == currentPlayList.playlistName)//playList.playlistName)
            {

                optionToRemove = dropdown;
            }
        }
        if (optionToRemove != null)
        {
            dropdownOptions.Remove(optionToRemove);
        }


        playlistDropdown.options.Clear();


        playlistDropdown.AddOptions(dropdownOptions);


        Destroy(currentPlayList.gameObject);

        //Move to the all songs playlist
        allSongsPlaylist.switchPlaylist();

        //SwitchPlaylist(allSongsPlaylist);
    }

    //when drop down changes, change the playlist to add song to. This dropdown should have a list of all of the playlists
    public void SelectedPlaylistToAddSongTo()
    {

        if (addToPlaylistSelectionDropdown.value != 0)
        {
            playlistToAddSongTo = listOfPlaylists[addToPlaylistSelectionDropdown.value];
        }
    }

    //Cancel the adding of a song to a playlist
    public void CancelSongAddition()
    {
        addToPlaylistSelection.SetActive(false);
        playListScreen.SetActive(true);
    }

    public void AddToPlayList()
    {
        if (addToPlaylistSelectionDropdown.value != 0)
        {
            //playlistToAddSongTo.songs.Add(songToAddToPlaylist);
            playlistToAddSongTo.playlistSongs.Add(regularSongToAddToPlaylist);
            addToPlaylistSelection.SetActive(false);
            playListScreen.SetActive(true);
        }
    }

    public void RemoveFromPlaylist(Playlist playList, RegularSong song, GameObject obj)//AudioClip song, GameObject obj)
    {
        playList.playlistSongs.Remove(song);
        //playList.songs.Remove(song);
    }

    public void SelectPlaylistToAddSong()
    {
        playListScreen.SetActive(false);
        addToPlaylistSelection.SetActive(true);

    }

    public void OpenPlaylistStuff()
    {
        playerCanvas.SetActive(false);
        playlistStuff.SetActive(true);
    }

    public void ClosePlaylistStuff()
    {
        playerCanvas.SetActive(true);
        playlistStuff.SetActive(false);

        //Do stuff to put the last selected playlist into the music player
        spotifyPlayer.currentPlaylist = currentPlayList;
        spotifyPlayer.index = 0;
        spotifyPlayer.SetUIStuff();
        spotifyPlayer.PlaySong();

    }
}
