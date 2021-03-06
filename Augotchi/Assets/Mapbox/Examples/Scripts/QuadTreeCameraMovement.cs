﻿namespace Mapbox.Examples
{
	using Mapbox.Unity.Map;
	using Mapbox.Unity.Utilities;
	using Mapbox.Utils;
	using UnityEngine;
	using System;

	public class QuadTreeCameraMovement : MonoBehaviour
	{
		[SerializeField]
		[Range(0, 20)]
		public float _panSpeed = 0f;

		[SerializeField]
		float _zoomSpeed = 0f;

		[SerializeField]
		public Camera _referenceCamera;

		[SerializeField]
		QuadTreeTileProvider _quadTreeTileProvider;

		[SerializeField]
		AbstractMap _dynamicZoomMap;

		[SerializeField]
		bool _useDegreeMethod;

		private Vector3 _origin;
		private Vector3 _mousePosition;
		private Vector3 _mousePositionPrevious;
		private bool _shouldDrag;

        private bool isInitialized = false;

		void Start()
		{
			if (null == _referenceCamera)
			{
				_referenceCamera = GetComponent<Camera>();
				if (null == _referenceCamera) { Debug.LogErrorFormat("{0}: reference camera not set", this.GetType().Name); }
			}

            
        }


		private void LateUpdate()
		{
			if (null == _dynamicZoomMap) { return; }

            GameControl.isZooming = false;

            if (Input.touchSupported && Input.touchCount > 0)
			{
				HandleTouch();
			}
			else
			{
				HandleMouseAndKeyBoard();
			}

            // VARNING! FULHAX
            if (!isInitialized)
            {
                _quadTreeTileProvider.UpdateMapProperties(_dynamicZoomMap.CenterLatitudeLongitude, GameControl.zoomValue);

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Poof"))
                    go.transform.localScale = transform.localScale * 2;

                isInitialized = true;
            }
		}

		void HandleMouseAndKeyBoard()
		{
			// zoom
			float scrollDelta = 0.0f;
			scrollDelta = Input.GetAxis("Mouse ScrollWheel");
			ZoomMapUsingTouchOrMouse(scrollDelta);

			//pan keyboard
			float xMove = Input.GetAxis("Horizontal");
			float zMove = Input.GetAxis("Vertical");

			PanMapUsingKeyBoard(xMove, zMove);


			//pan mouse
			PanMapUsingTouchOrMouse();
		}

		void HandleTouch()
		{
			float zoomFactor = 0.0f;
			//pinch to zoom. 
			switch (Input.touchCount)
			{
				case 1:
					{
						PanMapUsingTouchOrMouse();
					}
					break;
				case 2:
					{
						// Store both touches.
						Touch touchZero = Input.GetTouch(0);
						Touch touchOne = Input.GetTouch(1);

						// Find the position in the previous frame of each touch.
						Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
						Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

						// Find the magnitude of the vector (the distance) between the touches in each frame.
						float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
						float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

						// Find the difference in the distances between each frame.
						zoomFactor = 0.01f * (touchDeltaMag - prevTouchDeltaMag);

                        if (zoomFactor > 0f && gameObject.transform.localScale.x > 2.25f)
                        {
                            return;
                        }
                        else if (zoomFactor < 0f && gameObject.transform.localScale.x < 0.5f)
                        {
                            return;
                        }
					}
					ZoomMapUsingTouchOrMouse(zoomFactor);
					break;
				default:
					break;
			}
		}

		void ZoomMapUsingTouchOrMouse(float zoomFactor)
		{
            GameControl.isZooming = true;
            float newZoom = Mathf.Max(0.0f, Mathf.Min(_dynamicZoomMap.Zoom + zoomFactor * _zoomSpeed, 21.0f));
            if(isInitialized)
                GameControl.zoomValue = newZoom;

            _quadTreeTileProvider.UpdateMapProperties(_dynamicZoomMap.CenterLatitudeLongitude, newZoom);
		}

		void PanMapUsingKeyBoard(float xMove, float zMove)
		{
			if (Math.Abs(xMove) > 0.0f || Math.Abs(zMove) > 0.0f)
			{
				// Get the number of degrees in a tile at the current zoom level. 
				// Divide it by the tile width in pixels ( 256 in our case) 
				// to get degrees represented by each pixel.
				// Keyboard offset is in pixels, therefore multiply the factor with the offset to move the center.
				float factor = _panSpeed * (Conversions.GetTileScaleInDegrees((float)_dynamicZoomMap.CenterLatitudeLongitude.x, _dynamicZoomMap.AbsoluteZoom));
				_quadTreeTileProvider.UpdateMapProperties(new Vector2d(_dynamicZoomMap.CenterLatitudeLongitude.x + zMove * factor * 2.0f, _dynamicZoomMap.CenterLatitudeLongitude.y + xMove * factor * 4.0f), _dynamicZoomMap.Zoom);
			}
		}

		void PanMapUsingTouchOrMouse()
		{
            if (_useDegreeMethod)
            {
                UseDegreeConversion();
            }
            else
            {
                UseMeterConversion();
            }
        }

		void UseMeterConversion()
		{
			if (Input.GetMouseButton(0))
			{
				var mousePosScreen = Input.mousePosition;
				//assign distance of camera to ground plane to z, otherwise ScreenToWorldPoint() will always return the position of the camera
				//http://answers.unity3d.com/answers/599100/view.html
				mousePosScreen.z = _referenceCamera.transform.localPosition.y;
				_mousePosition = _referenceCamera.ScreenToWorldPoint(mousePosScreen);

				if (_shouldDrag == false)
				{
					_shouldDrag = true;
					_origin = _referenceCamera.ScreenToWorldPoint(mousePosScreen);
				}
			}
			else
			{
				_shouldDrag = false;
			}

			if (_shouldDrag == true)
			{
				var changeFromPreviousPosition = _mousePositionPrevious - _mousePosition;

                if(changeFromPreviousPosition.magnitude > 25)
                {
                    changeFromPreviousPosition = Vector3.zero;
                }

                GameObject go = GameObject.FindGameObjectWithTag("GameController");
                if (go != null)
                {
                    if (go.GetComponent<GameControl>().isTweakingGardenDecor)
                    {
                        return;
                    }
                }

                go = GameObject.FindGameObjectWithTag("InventoryUI");
                if (go != null)
                {
                    if (go.active)
                    {
                        return;
                    }
                }

                if (Mathf.Abs(changeFromPreviousPosition.x) > 0.0f || Mathf.Abs(changeFromPreviousPosition.y) > 0.0f)
				{
					_mousePositionPrevious = _mousePosition;
					var offset = _origin - _mousePosition;

                    /*if (Mathf.Abs(offset.x) > 0.0f || Mathf.Abs(offset.z) > 0.0f)
					{
						if (null != _dynamicZoomMap)
						{
							float factor = Conversions.GetTileScaleInMeters((float)0, _dynamicZoomMap.AbsoluteZoom) * 256.0f / _dynamicZoomMap.UnityTileSize;
							var latlongDelta = Conversions.MetersToLatLon(_dynamicZoomMap.CenterMercator + new Vector2d(offset.x * factor, offset.z * factor));
							_quadTreeTileProvider.UpdateMapProperties(latlongDelta, _dynamicZoomMap.Zoom);
						}
					}
					_origin = _mousePosition;*/

                    float rot = 0;

                    if(Input.mousePosition.y > Screen.height * 0.5)
                    {
                        rot += (changeFromPreviousPosition.x * 0.1f);
                    }
                    else
                    {
                        rot += (-changeFromPreviousPosition.x * 0.1f);
                    }

                    if (Input.mousePosition.x > Screen.width * 0.5)
                    {
                        rot += (-changeFromPreviousPosition.y * 0.1f);
                    }
                    else
                    {
                        rot += (changeFromPreviousPosition.y * 0.1f);
                    }

                    GameObject.FindGameObjectWithTag("CameraTarget").transform.Rotate(0, rot * 10f, 0);
                    GameControl.rotation = GameObject.FindGameObjectWithTag("CameraTarget").transform.rotation;
                }
				else
				{
					_mousePositionPrevious = _mousePosition;
					_origin = _mousePosition;
				}
			}
		}

		void UseDegreeConversion()
		{
			if (Input.GetMouseButton(0))
			{
				var mousePosScreen = Input.mousePosition;
				//assign distance of camera to ground plane to z, otherwise ScreenToWorldPoint() will always return the position of the camera
				//http://answers.unity3d.com/answers/599100/view.html
				mousePosScreen.z = _referenceCamera.transform.localPosition.y;
				_mousePosition = _referenceCamera.ScreenToWorldPoint(mousePosScreen);

				if (_shouldDrag == false)
				{
					_shouldDrag = true;
					_origin = _referenceCamera.ScreenToWorldPoint(mousePosScreen);
				}
			}
			else
			{
				_shouldDrag = false;
			}

			if (_shouldDrag == true)
			{
				var changeFromPreviousPosition = _mousePositionPrevious - _mousePosition;
				if (Mathf.Abs(changeFromPreviousPosition.x) > 0.0f || Mathf.Abs(changeFromPreviousPosition.y) > 0.0f)
				{
					_mousePositionPrevious = _mousePosition;
					var offset = _origin - _mousePosition;

					if (Mathf.Abs(offset.x) > 0.0f || Mathf.Abs(offset.z) > 0.0f)
					{
						if (null != _dynamicZoomMap)
						{
							// Get the number of degrees in a tile at the current zoom level. 
							// Divide it by the tile width in pixels ( 256 in our case) 
							// to get degrees represented by each pixel.
							// Mouse offset is in pixels, therefore multiply the factor with the offset to move the center.
							float factor = _panSpeed * Conversions.GetTileScaleInDegrees((float)_dynamicZoomMap.CenterLatitudeLongitude.x, _dynamicZoomMap.AbsoluteZoom) * 256.0f / _dynamicZoomMap.UnityTileSize;
							_quadTreeTileProvider.UpdateMapProperties(new Vector2d(_dynamicZoomMap.CenterLatitudeLongitude.x + offset.z * factor, _dynamicZoomMap.CenterLatitudeLongitude.y + offset.x * factor), _dynamicZoomMap.Zoom);
						}
					}
					_origin = _mousePosition;
				}
				else
				{
					_mousePositionPrevious = _mousePosition;
					_origin = _mousePosition;
				}
			}
		}
	}
}