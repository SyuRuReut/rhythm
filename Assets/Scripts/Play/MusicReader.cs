using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicReader : MonoBehaviour
{
    public string fileName;
    public CreateNote createNote;
    public AudioClip[] musics;
    public string title;
    public int musicNum;
    public AudioSource audioSource;
    public string[] fileNames;

    PlayManager play;
    UserManager user;
    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();
        play = GetComponent<PlayManager>();
        //TextAsset str = Resources.Load<TextAsset>("/Music/osu/" + fileNames[user.readMusic()] + ".osu");
        //StreamReader str = new StreamReader("Assets/Resouces/Music/osu/" + fileNames[user.readMusic()]+".osu", System.Text.Encoding.Default);
        using(FileStream f = new FileStream(Application.streamingAssetsPath+"/"+ fileNames[user.readMusic()] + ".osu", FileMode.Open, FileAccess.Read))
        {
            using(StreamReader reader = new StreamReader(f, System.Text.Encoding.UTF8))
            {
                string line = string.Empty;

                while((line = reader.ReadLine()) != null)
                {
                    Pashing(line);
                }
                reader.Close();
            }f.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (play.pause)
        {
            audioSource.Pause();
        }
    }

    void Pashing(string line)
    {
        
        if (line == null || line == "")
            return;

        if (line.StartsWith("["))
            return;
        else if (line.Length > 12 && line.Substring(0, 12).Equals("TitleUnicode"))
        {
            title = line.Substring(13, line.Length - 13);
            MusicSet();
        }
        else if (line.Length > 13 && line.Substring(0, 13).Equals("ArtistUnicode"))
        {

        }
        else if (line.Length > 7 && line.Substring(0, 7).Equals("Version"))
        {

        }
        else if (line.Length > 3 && line.Substring(0, 3).Equals("BPM"))
        {

        }
        else
        {
            string[] row = line.Split(',');
            createNote.SetNotes(row);
        }    

    }

    void MusicSet()
    {
        musicNum = user.musicNum;
    }

    public void MusicPlay()
    {
        audioSource.clip = musics[musicNum];
        audioSource.Play();
    }

    public bool MusicStop()
    {
        return !audioSource.isPlaying;
    }
}
