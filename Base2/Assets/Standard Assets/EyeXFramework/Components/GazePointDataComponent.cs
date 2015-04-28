//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using Tobii.EyeX.Framework;
using UnityEngine;

/// <summary>
/// Component that encapsulates a provider for <see cref="EyeXGazePoint"/> data.
/// </summary>
[AddComponentMenu("Tobii EyeX/Gaze Point Data")]
public class GazePointDataComponent : MonoBehaviour
{
    public GazePointDataMode gazePointDataMode = GazePointDataMode.LightlyFiltered;
	public SphereCollider colorCollider;

    private EyeXHost _eyexHost;
    private IEyeXDataProvider<EyeXGazePoint> _dataProvider;
	private RaycastHit gazeRaycastHit;


    /// <summary>
    /// Gets the last gaze point.
    /// </summary>
    public EyeXGazePoint LastGazePoint { get; private set; }

    protected void Awake()
    {
        _eyexHost = EyeXHost.GetInstance();
        _dataProvider = _eyexHost.GetGazePointDataProvider(gazePointDataMode);
    }

    protected void OnEnable()
    {
        _dataProvider.Start();
    }

    protected void OnDisable()
    {
        _dataProvider.Stop();
    }

    protected void Update()
    {
        LastGazePoint = _dataProvider.Last;

		// Get the last fixation point.
		EyeXGazePoint lastGazePoint = GetComponent<GazePointDataComponent>().LastGazePoint;

		if (lastGazePoint.IsValid) {
			Vector2 screenCoordinates = lastGazePoint.Screen;
			Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint (new Vector3 (screenCoordinates.x, screenCoordinates.y, 0));
			Ray gazeRay = Camera.main.ScreenPointToRay (new Vector3 (screenCoordinates.x, screenCoordinates.y, 0));

			if (Physics.Raycast (gazeRay.origin, gazeRay.direction, out gazeRaycastHit, 30)) {
				//Debug.Log ("I fixed: " + fixationRaycastHit.collider.gameObject.name);

				colorCollider.transform.position = gazeRaycastHit.transform.position;
			}
		}
    }
}
