using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {

	// variables a renplir :
	public GameObject CameraViewport;
	public Camera[] cameras; //indiquer le nombre d'entré (size)

	//variables :
	private GameObject[] mainCameras;
	public int camerasLengt;
	public int currentCameraIndex;

	//animation camera variable :
	public bool cameraIsAnimated = false;
	public float speed;
	public float limiteTimeRotation;
	public float lastTime;
	public float timeDiff;

	public float gravityIntensity;

	// Use this for initialization
	void Start () {

		// stocker tous les objets camera :
		mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");

		//	stocker toutes les vue :
		Debug.Log ("–––––camera position–––––");

		//	regarder si une des entrées n'est pas vide :
		bool camerasIsCorrectInit = true;
		for(int i = 0; i<mainCameras.Length; i++){

			if (cameras [i] == null) {
				camerasIsCorrectInit = false;
			}
		}

		if(! camerasIsCorrectInit ){
			for(int i = 0; i<mainCameras.Length; i++){
				Debug.Log( "camera "+i );
				Debug.Log( mainCameras[i].GetComponent<Camera> ().transform.position );

				cameras[i] = mainCameras[i].GetComponent<Camera> ();
			}
		}

		// initalisiser camerasLengt, à cause du décallage de 1 :
		camerasLengt = cameras.Length - 1;

		// initialiser position display sur la première camera :
		currentCameraIndex = 0;
		Vector3 initViewportPosition = cameras[currentCameraIndex].transform.position;
		Quaternion initViewportRotation = cameras[currentCameraIndex].transform.rotation;
		moovViewportTo( initViewportPosition, initViewportRotation );

		// init gravity !!
	}
	
	// Update is called once per frame
	void Update () {

		cameraOrientation ();

		// changement de gravité :
		if (CameraViewport.GetComponent<Transform> ().rotation.eulerAngles.x > 10) {
			Physics.gravity = new Vector3 (0, 0, gravityIntensity);
			Debug.Log (CameraViewport.GetComponent<Transform> ().rotation.eulerAngles);
		} else {
			Physics.gravity = new Vector3 (0, - gravityIntensity, 0);
			Debug.Log (CameraViewport.GetComponent<Transform> ().rotation.eulerAngles);
		}
	}

	//	functions declaration :

	//	orientation de la camera :
	void cameraOrientation(){

		//	changer position camera, si elle est pas en animation :
		if (!cameraIsAnimated) {
			// mettre a jour les informations de position du CameraViewport :
			if (Input.GetKeyDown ("space")) {
				cameraIsAnimated = true;
				indexCameraPostion ();
				lastTime = Time.time;
			}
		}

		Vector3 moovCameraTo = cameras[currentCameraIndex].transform.position;
		Quaternion rotateCameraTo = cameras[currentCameraIndex].transform.rotation;

		//	appliquer la nouvelle position pour CameraViewport :
		moovViewportTo( moovCameraTo, rotateCameraTo );
	}

	// incrementation logique de l'index de camera :
	void indexCameraPostion(){
		currentCameraIndex++;
		if( currentCameraIndex > camerasLengt ){
			currentCameraIndex = 0;
		}
	}

	void moovViewportTo( Vector3 position, Quaternion rotation ){

		Vector3 currentPosition = CameraViewport.GetComponent<Transform> ().position;
		Vector3 currentRotation = CameraViewport.GetComponent<Transform> ().rotation.eulerAngles;

		Vector3 rotationDemande = rotation.eulerAngles;
		
		// bouger que si on est pas a la position voulue :
		if (currentPosition != position || currentRotation == rotationDemande ) {

			Vector3 diffPosition = position - currentPosition;
			Vector3 diffRotation;
			diffRotation.x = rotationDemande.x - currentRotation.x;
			if( rotationDemande.y < currentRotation.y ){
				diffRotation.y = rotationDemande.y - currentRotation.y + 360;
			} else {
				diffRotation.y = rotationDemande.y - currentRotation.y;
			}
			diffRotation.z = rotationDemande.z - currentRotation.z;

			Vector3 newPosition;
			newPosition.x = currentPosition.x + ( diffPosition.x * speed );
			newPosition.y = currentPosition.y + ( diffPosition.y * speed );
			newPosition.z = currentPosition.z + ( diffPosition.z * speed );

			Vector3 newRotation;
			newRotation.x = currentRotation.x + ( diffRotation.x * speed );
			newRotation.y = currentRotation.y + ( diffRotation.y * speed );
			newRotation.z = currentRotation.z + ( diffRotation.z * speed );
			
			CameraViewport.GetComponent<Transform> ().position = newPosition;
			CameraViewport.GetComponent<Transform> ().rotation = Quaternion.Euler(newRotation);

			timeDiff = Time.time - lastTime;
			if( timeDiff > limiteTimeRotation ){
				cameraIsAnimated = false;
				lastTime = 0;
			}

		}
		else {
			// fin de l'animation :
			cameraIsAnimated = false;
		}

	}
}
