//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using Tobii.EyeX.Framework;
using UnityEngine;

/// <summary>
/// Component that encapsulates a provider for <see cref="EyeXFixationPoint"/> data.
/// </summary>
[AddComponentMenu("Tobii EyeX/Fixation Data")]
public class FixationDataComponent : MonoBehaviour
{
    public FixationDataMode fixationDataMode = FixationDataMode.Slow;

    private EyeXHost _eyexHost;
    private IEyeXDataProvider<EyeXFixationPoint> _dataProvider;
	private RaycastHit fixationRaycastHit;

    /// <summary>6
    /// Gets the last fixation.
    /// </summary>
    public EyeXFixationPoint LastFixation { get; private set; }

    protected void Awake()
	{

        _eyexHost = EyeXHost.GetInstance();
        _dataProvider = _eyexHost.GetFixationDataProvider(fixationDataMode);
    }

    protected void Update()
    {
        LastFixation = _dataProvider.Last;

		// Get the last fixation point.
		EyeXFixationPoint lastFixation = GetComponent<FixationDataComponent>().LastFixation;
		if(lastFixation.IsValid)
		{
			if(lastFixation.EventType == FixationDataEventType.Begin) {
			
			
			} else if(lastFixation.EventType == FixationDataEventType.Data) {
				// Fixation data is being retrieved.

			} else if(lastFixation.EventType == FixationDataEventType.End) {
				// The fixation ended.

			}

			
			/*Ray fixationRay = Camera.main.ScreenPointToRay(new Vector3(screenCoordinates.x, screenCoordinates.y, 0.0f));
			Debug.DrawRay (fixationRay.origin, fixationRay.direction * distanceToSee, Color.magenta);
			
			if (Physics.Raycast (gazeRay.origin, gazeRay.direction, out gazeRaycastHit, distanceToSee)) {
				Debug.Log ("I gazed: " + gazeRaycastHit.collider.gameObject.name);
				gazeRaycastHit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
				
				// set timer of block to 1 (when timer reaches 0 it will get standard gray color)
				BlockScript blockScript = gazeRaycastHit.collider.gameObject.GetComponent<BlockScript>();
				blockScript.lostGazeTimer = 0.35f;
			}*/


		}
    }

    protected void OnEnable()
    {
        _dataProvider.Start();
    }

    protected void OnDisable()
    {
        _dataProvider.Stop();
    }
}
