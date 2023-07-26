using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateNote : MonoBehaviour
{
    Queue<string[]> notes = new Queue<string[]>();
    public bool musicStart;
    public GameObject noteBlock;
    public double playTime=0;
    public bool readyCreate;
    string[] note = null;
    string[] note2 = null;
    public bool musicOff;
    public MusicReader musicReader;
    public GameObject judge;
    public float y;
    public float sink;

    [SerializeField] private SceneLoader sceneLoader;
    UserManager user;
    // Start is called before the first frame update
    void Start()
    {
        y = judge.transform.position.y;
        user = FindObjectOfType<UserManager>();
        sink = user.sink/10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            playTime += Time.deltaTime / user.noteSpeed;
            if (!musicStart && playTime >= 2)
            {
                musicStart = true;
                musicReader.MusicPlay();
            }
            if (!readyCreate)
            {
                if (notes.Count != 0)
                {
                    note = notes.Dequeue();
                    readyCreate = true;
                }
                else
                {
                    musicOff = true;
                }

            }
            else
            {
                if (double.Parse(note[2]) / 1000 <= playTime + sink)
                {
                    if (notes.Count != 0)
                    {
                        note2 = notes.Dequeue();
                    }
                    else
                    {
                        note2 = null;
                    }
                    if( note2 != null&&note[2] == note2[2])
                    {
                        if (note[3] == "5" || note[3] == "1")
                        {
                            if (note[0] == "64")
                                Instantiate(noteBlock, new Vector3(-7.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            else if (note[0] == "192")
                                Instantiate(noteBlock, new Vector3(-5.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            if (note[0] == "320")
                                Instantiate(noteBlock, new Vector3(-3.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            if (note[0] == "448")
                                Instantiate(noteBlock, new Vector3(-1.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                        }
                        else if (note[3] == "128")
                        {
                            if (note[0] == "64")
                            {
                                double time = double.Parse(note[5].Split(':')[0]) - double.Parse(note[2]);
                                LongNote(-7.5f, time / 1000);
                            }
                            else if (note[0] == "192")
                            {
                                double time = double.Parse(note[5].Split(':')[0]) - double.Parse(note[2]);
                                LongNote(-5.5f, time / 1000);
                            }
                            if (note[0] == "320")
                            {
                                double time = double.Parse(note[5].Split(':')[0]) - double.Parse(note[2]);
                                LongNote(-3.5f, time / 1000);
                            }
                            if (note[0] == "448")
                            {
                                double time = double.Parse(note[5].Split(':')[0]) - double.Parse(note[2]);
                                LongNote(-1.5f, time / 1000);
                            }

                        }
                        if (note2[3] == "5" || note2[3] == "1")
                        {
                            if (note2[0] == "64")
                                Instantiate(noteBlock, new Vector3(-7.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            else if (note2[0] == "192")
                                Instantiate(noteBlock, new Vector3(-5.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            if (note2[0] == "320")
                                Instantiate(noteBlock, new Vector3(-3.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            if (note2[0] == "448")
                                Instantiate(noteBlock, new Vector3(-1.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                        }
                        else if (note2[3] == "128")
                        {
                            if (note2[0] == "64")
                            {
                                double time = double.Parse(note2[5].Split(':')[0]) - double.Parse(note2[2]);
                                LongNote(-7.5f, time / 1000);
                            }
                            else if (note2[0] == "192")
                            {
                                double time = double.Parse(note2[5].Split(':')[0]) - double.Parse(note2[2]);
                                LongNote(-5.5f, time / 1000);
                            }
                            if (note2[0] == "320")
                            {
                                double time = double.Parse(note2[5].Split(':')[0]) - double.Parse(note2[2]);
                                LongNote(-3.5f, time / 1000);
                            }
                            if (note[0] == "448")
                            {
                                double time = double.Parse(note2[5].Split(':')[0]) - double.Parse(note2[2]);
                                LongNote(-1.5f, time / 1000);
                            }

                        }
                        readyCreate = false;
                    }
                    else
                    {
                        if (note[3] == "5" || note[3] == "1")
                        {
                            if (note[0] == "64")
                                Instantiate(noteBlock, new Vector3(-7.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            else if (note[0] == "192")
                                Instantiate(noteBlock, new Vector3(-5.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            if (note[0] == "320")
                                Instantiate(noteBlock, new Vector3(-3.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);
                            if (note[0] == "448")
                                Instantiate(noteBlock, new Vector3(-1.5f, y + (8 * user.noteSpeed), 0), gameObject.transform.rotation);


                        }
                        else if (note[3] == "128")
                        {
                            if (note[0] == "64")
                            {
                                double time = double.Parse(note[5].Split(':')[0]) - double.Parse(note[2]);

                                LongNote(-7.5f, time / 1000);
                            }
                            else if (note[0] == "192")
                            {
                                double time = double.Parse(note[5].Split(':')[0]) - double.Parse(note[2]);

                                LongNote(-5.5f, time / 1000);
                            }
                            if (note[0] == "320")
                            {
                                double time = double.Parse(note[5].Split(':')[0]) - double.Parse(note[2]);

                                LongNote(-3.5f, time / 1000);
                            }
                            if (note[0] == "448")
                            {
                                double time = double.Parse(note[5].Split(':')[0]) - double.Parse(note[2]);

                                LongNote(-1.5f, time / 1000);
                            }

                        }
                        if (note2 != null)
                            note = note2;
                        else
                            readyCreate = false;
                    }
                }
            }
            if (musicReader.MusicStop() && musicOff)
            {
                sceneLoader.toResult();
            }
        }
    }
    public void SetNotes(string[] note)
    {
        notes.Enqueue(note);
    }
    public void LongNote(float x, double time)
    {
        
        double longtime = 0;
        GameObject a = Instantiate(noteBlock, new Vector3(x, y+(8 * user.noteSpeed), 0), gameObject.transform.rotation);
        a.GetComponent<MoveNote>().longNote = true;
        a.GetComponent<MoveNote>().longTime = time;
        while (time >= longtime)
        {
            longtime += Time.deltaTime/user.noteSpeed;
            a.transform.position += new Vector3(0, Time.deltaTime/2*4, 0);
            a.transform.localScale += new Vector3(0, Time.deltaTime*4, 0);
        }
    }
}
