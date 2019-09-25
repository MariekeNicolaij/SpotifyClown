using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayListManager : MonoBehaviour
{
    public Playlist allSongsPlaylist;

    public List<AudioClip> allSongs;
    public List<Playlist> listOfPlaylists;


    //Stuff to add song to playlist
    public Playlist playlistToAddSongTo;
    public AudioClip songToAddToPlaylist;

    public Playlist currentPlayList;
    public Playlist nextPlaylist;

    public Dropdown playlistDropdown;

    public GameObject playlistPrefab;

    public GameObject playlistRoot;
    public GameObject songRoot;

    public GameObject deletePlaylistButton;

    public GameObject addToPlaylistSelection;
    public Dropdown addToPlaylistSelectionDropdown;

    public InputField newPlaylistName;

    public void Start()
    {
        //Add all the songs to the all songs playlist
        foreach (AudioClip song in allSongs)
        {
            //allSongsPlaylist.playListData.songs.Add(song);
            allSongsPlaylist.songs.Add(song);
        }

        addToPlaylistSelection.SetActive(false);

        allSongsPlaylist.switchPlaylist();

        //Temporary thing for playlist to add songs to
        playlistToAddSongTo = allSongsPlaylist;

        listOfPlaylists.Add(allSongsPlaylist);
    }

    public void CreatePlaylist()
    {
        PlayListData playList = new PlayListData();


        GameObject obj = Instantiate(playlistPrefab, playlistRoot.transform);

        Playlist playlist = obj.GetComponentInChildren<Playlist>();
        playlist.playListData = playList;
        playlist.buttonText.text = newPlaylistName.text;
        playlist.playlistName = newPlaylistName.text;
        playList.name = newPlaylistName.text;

        listOfPlaylists.Add(playlist);

        //Add playlist to the list of playlists when adding songs
        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
        dropdownOptions.AddRange(playlistDropdown.options);
        playlistDropdown.options.Clear();

        dropdownOptions.Add(new Dropdown.OptionData(newPlaylistName.text));
        playlistDropdown.AddOptions(dropdownOptions);

        SwitchPlaylist(playlist);
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

        if(selectedPlaylist.isGeneralPlaylist)
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
        foreach(Dropdown.OptionData dropdown in dropdownOptions)
        {

            if (dropdown.text == currentPlayList.playlistName)//playList.playlistName)
            {

                optionToRemove = dropdown;
            }
        }
        if(optionToRemove != null)
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

        Debug.Log(addToPlaylistSelectionDropdown.value);
        if(addToPlaylistSelectionDropdown.value != 0)
        {
            playlistToAddSongTo = listOfPlaylists[addToPlaylistSelectionDropdown.value];
        }
    }

    //Cancel the adding of a song to a playlist
    public void CancelSongAddition()
    {
        addToPlaylistSelection.SetActive(false);

    }

    public void AddToPlayList()
    {
        playlistToAddSongTo.songs.Add(songToAddToPlaylist);
        addToPlaylistSelection.SetActive(false);
    }

    public void RemoveFromPlaylist(Playlist playList, AudioClip song, GameObject obj)
    {
        playList.songs.Remove(song);
    }

    public void SelectPlaylistToAddSong()
    {
        addToPlaylistSelection.SetActive(true);

    }

    //TODO:

    //4. Add songs to playlist from the "general" playlist

    //Done:
    //1. Create playlist
    //6. Delete playlist
    //5. Remove songs from specific playlist
    //3. Swap Between playlist
    //2. the "general" play list with all the songs(cannot be deleted)


}
