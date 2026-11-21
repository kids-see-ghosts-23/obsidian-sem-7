using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerModelPlacer : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public GameObject model;

    void Awake()
    {
        model.SetActive(false);
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImagesChanged;
    }

    void OnImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage image in args.added)
            Place(image);

        foreach (ARTrackedImage image in args.updated)
            Place(image);
    }

    void Place(ARTrackedImage image)
    {
        if (image.trackingState != TrackingState.Tracking)
            return;

        model.SetActive(true);
        model.transform.position = image.transform.position;
        model.transform.rotation = image.transform.rotation;
    }
}
