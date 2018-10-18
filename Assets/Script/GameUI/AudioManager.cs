using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZWGames;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private List<AudioSource> _audioResources = new List<AudioSource>();

    private enum E_SOUND_ID
    {
        E_BUTTON_PRESSED_SOUND = 0,
    }

    // Use this for initialization
    private Recipient _recipient = new Recipient();

    void Start () {
        _recipient.addNotify(NotifyId.NOTIFY_PLAY_BUTTON_PRESSED_SOUND, playButonPressedSound);
	}

    private void playButonPressedSound(INotifyData obj)
    {
        int soundIndex = (int)E_SOUND_ID.E_BUTTON_PRESSED_SOUND;
        if (_audioResources.Count > soundIndex)
        {
            _audioResources[soundIndex].Play();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
