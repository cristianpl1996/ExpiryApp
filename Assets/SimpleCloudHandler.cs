using UnityEngine;
using Vuforia;

public class SimpleCloudHandler : MonoBehaviour {

    private CloudRecoBehaviour mCloudRecoBehaviour; 
    private bool mIsScanning = false;
    private string mTargetMetadata = "";

    public ImageTargetBehaviour ImageTargetTemplate_Alcohol;
	public ImageTargetBehaviour ImageTargetTemplate_Levepromazina;
	public ImageTargetBehaviour ImageTargetTemplate_Albendazol;
	public ImageTargetBehaviour ImageTargetTemplate_Metoclopramida;

    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    public void OnInitialized(TargetFinder targetFinder) {
      Debug.Log ("Cloud Reco initialized");
    }
    public void OnInitError(TargetFinder.InitState initError) {
        Debug.Log ("Cloud Reco init error " + initError.ToString());
    }
    public void OnUpdateError(TargetFinder.UpdateState updateError) {
        Debug.Log ("Cloud Reco update error " + updateError.ToString());
    }

    public void OnStateChanged(bool scanning) {
        mIsScanning = scanning;
        if (scanning)
        {
            // clear all known trackables
            var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
        }
    }

    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult) {
        TargetFinder.CloudRecoSearchResult cloudRecoSearchResult = 
            (TargetFinder.CloudRecoSearchResult)targetSearchResult;
        // do something with the target metadata
        mTargetMetadata = cloudRecoSearchResult.MetaData;

		if ((mTargetMetadata == "alcohol")) {
			if (ImageTargetTemplate_Alcohol) { 
				// enable the new result with the same ImageTargetBehaviour: 
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
				tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate_Alcohol.gameObject);
			}
            Debug.Log(mTargetMetadata);
		} else if ((mTargetMetadata == "levepromazina")) {
			if (ImageTargetTemplate_Levepromazina) { 
				// enable the new result with the same ImageTargetBehaviour: 
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
				tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate_Levepromazina.gameObject);
			}
            Debug.Log(mTargetMetadata);
		} else if ((mTargetMetadata == "albendazol")) {
			if (ImageTargetTemplate_Albendazol) { 
				// enable the new result with the same ImageTargetBehaviour: 
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
				tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate_Albendazol.gameObject);
			}
            Debug.Log(mTargetMetadata);
		} else if ((mTargetMetadata == "metoclopramida")) {
			if (ImageTargetTemplate_Metoclopramida) { 
				// enable the new result with the same ImageTargetBehaviour: 
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
				tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate_Metoclopramida.gameObject);
			}
            Debug.Log(mTargetMetadata);
		} 
        // stop the target finder (i.e. stop scanning the cloud)
        mCloudRecoBehaviour.CloudRecoEnabled = false;
    }

    /*void OnGUI() {
      if (!mIsScanning) {
          if (GUI.Button(new Rect(100,300,200,50), "Restart Scanning")) {
          // Restart TargetFinder
            OnDestroy();
            mCloudRecoBehaviour.CloudRecoEnabled = true;
          }
      }
    }*/

	public void Restart() {
        mCloudRecoBehaviour.CloudRecoEnabled = true;
        OnDestroy(); 
	}
}
