using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Vuforia;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
    public bool isVideoEnabled;
    public string url;
    [Space]
    public GameObject videoGO;

    private Button _interactionBtn;
    private VideoPlayer _videoPlayer;
    private RawImage _rawImage;

    private void Awake() {
        _interactionBtn = videoGO.GetComponent<Button>();
        _videoPlayer = videoGO.GetComponent<VideoPlayer>();
        _rawImage = videoGO.GetComponent<RawImage>();
    }

    protected override void Start()
    {
        base.Start();
        _interactionBtn.onClick.AddListener(OpenUrl);
        StartCoroutine(PrepareVideo());
        var vuforia = VuforiaARController.Instance;
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        vuforia.RegisterOnPauseCallback(OnPaused);
    }

    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }
     
    private void OnPaused(bool paused)
    {
        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
                CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }

    private IEnumerator PrepareVideo()
    {
        _videoPlayer.Prepare();

        while (!_videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(.5f);
        }

        _rawImage.texture = _videoPlayer.texture;

        isVideoEnabled = true;
    }

    private void OpenUrl() {
        Application.OpenURL(url);
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        if(isVideoEnabled) _videoPlayer.Play();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if(isVideoEnabled) _videoPlayer.Pause();
    }
	
	public void Play() {
		if(isVideoEnabled) _videoPlayer.Play();
	}
	
	public void Pause() {
		if(isVideoEnabled) _videoPlayer.Pause();
	}
	
	public void Restart() {
		base.OnTrackingLost();
		if(isVideoEnabled) _videoPlayer.Pause();
	}
}