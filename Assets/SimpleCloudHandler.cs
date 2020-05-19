using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;
using UnityEngine.Video;

public class SimpleCloudHandler : MonoBehaviour, IObjectRecoEventHandler { 
    private CloudRecoBehaviour mCloudRecoBehaviour; 
    private bool mIsScanning = false;
    private string mTargetMetadata = "";
    public ImageTargetBehaviour ImageTargetTemplate_Alcohol;
	public ImageTargetBehaviour ImageTargetTemplate_Levepromazina;
	public ImageTargetBehaviour ImageTargetTemplate_Albendazol;
	public ImageTargetBehaviour ImageTargetTemplate_Metoclopramida;
// Use this for initialization 
    void Start () { 
// register this event handler at the cloud reco behaviour 
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>(); 
		
        if (mCloudRecoBehaviour) 
        { 
            mCloudRecoBehaviour.RegisterEventHandler(this); 
        } 
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
		} else if ((mTargetMetadata == "levepromazina")) {
			if (ImageTargetTemplate_Levepromazina) { 
				// enable the new result with the same ImageTargetBehaviour: 
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
				tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate_Levepromazina.gameObject);
			}
		} else if ((mTargetMetadata == "albendazol")) {
			if (ImageTargetTemplate_Albendazol) { 
				// enable the new result with the same ImageTargetBehaviour: 
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
				tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate_Albendazol.gameObject);
			}
		} else if ((mTargetMetadata == "metoclopramida")) {
			if (ImageTargetTemplate_Metoclopramida) { 
				// enable the new result with the same ImageTargetBehaviour: 
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
				tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, ImageTargetTemplate_Metoclopramida.gameObject);
			}
		} 
        // stop the target finder (i.e. stop scanning the cloud)
        mCloudRecoBehaviour.CloudRecoEnabled = false;
    }

	public void Click() {
		if (!mIsScanning) {
            mCloudRecoBehaviour.CloudRecoEnabled = true;
		}
	}
}
