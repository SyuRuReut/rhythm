using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    UserManager user;
    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();
    }

    // Update is called once per frame
    void Update()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(user.player[0].sound)*20);
    }
}
