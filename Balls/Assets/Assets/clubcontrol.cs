using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using UnityEngine.UI;

public class clubcontrol : MonoBehaviour {
	private UdpClient receivingUdpClient;    
	private IPEndPoint remoteIpEndPoint;
	private Thread thread;
	private float[] receivedTransform;
	public Text textinfo;

	// port number
	private int receivePort = 4545;

	// Use this for initialization
	void Start () {
		receivedTransform = new float[3];
		try {
			receivingUdpClient = new UdpClient(receivePort);
		} catch (Exception e) {
			Debug.Log (e.ToString());
		}

		remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

		// start the thread for receiving signals
		thread = new Thread(new ThreadStart(ReceiveDataBytes));
		thread.Start();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation.SetEulerAngles (new Vector3 (45, -receivedTransform [2], receivedTransform [1]));
		//transform.Rotate (0, 0, 0);
		//transform.Rotate (-receivedTransform [0], -receivedTransform [2], receivedTransform [1]);
		transform.eulerAngles = new Vector3(40 - receivedTransform[2], 180 - receivedTransform[1], 0.0f);
		if (textinfo != null) {
			StringBuilder sb = new StringBuilder ();
			sb.Append ("(");
			sb.Append (receivedTransform [2]);
			sb.Append (", ");
			sb.Append (-receivedTransform [1]);
			sb.Append (", ");
			sb.Append (receivedTransform [0]);
			sb.Append (")");
			textinfo.text = sb.ToString ();
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Rotate (new Vector3 (1, 0, 0));
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Rotate (new Vector3 (-1, 0, 0));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (new Vector3 (0, -1, 0));
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (new Vector3 (0, 1, 0));
		}
	}

	void ReceiveDataBytes() {
		while (true) {
			//Debug.Log ("Threading inside while");
			// NOTE!: This blocks execution until a new message is received
			byte[] buffer = receivingUdpClient.Receive(ref remoteIpEndPoint);

			int currPos = 0;
			int size = 0;
			size = (int)buffer[currPos++] << 8;
			size = (int)buffer[currPos++] | size;
			Debug.Log (size);
			float[] parameters = new float[(size - 2) / 4];

			StringBuilder stringInfo = new StringBuilder();
			stringInfo.Append ("Received message: (");

			for (int i = 0; i < size / 4; i++) {
				int currElem = 0;
				currElem = (int)buffer[currPos++] << 24;
				currElem = (int)buffer[currPos++] << 16 | currElem;
				currElem = (int)buffer [currPos++] << 8 | currElem;
				currElem = (int)buffer [currPos++] | currElem;

				float floatElem = (float)currElem / 1000000;

				parameters [i] = floatElem;
				stringInfo.Append (floatElem);
				stringInfo.Append (", ");
			}
			stringInfo.Remove (stringInfo.Length - 3, 2);
			stringInfo.Append (")");

			for (int i = 0; i < 3; i++) {
				receivedTransform [i] = parameters [i];
			}
			Debug.Log (stringInfo.ToString ());

			//Debug.Log ("The message was sent from " + remoteIpEndPoint.Address.ToString() + " on port number " + remoteIpEndPoint.Port.ToString());

			Thread.Sleep(8);
		}
	}

	Matrix4x4 getRotation(float[] rotationVector) {
		Matrix4x4 ret = new Matrix4x4 ();
		float q0;
		float q1 = -rotationVector[0];
		float q2 = -rotationVector[2];
		float q3 = rotationVector[1];

		if (rotationVector.Length >= 4) {
			q0 = rotationVector[3];
		} else {
			q0 = 1 - q1*q1 - q2*q2 - q3*q3;
			q0 = (q0 > 0) ? (float)Mathf.Sqrt(q0) : 0;
		}

		float sq_q1 = 2 * q1 * q1;
		float sq_q2 = 2 * q2 * q2;
		float sq_q3 = 2 * q3 * q3;
		float q1_q2 = 2 * q1 * q2;
		float q3_q0 = 2 * q3 * q0;
		float q1_q3 = 2 * q1 * q3;
		float q2_q0 = 2 * q2 * q0;
		float q2_q3 = 2 * q2 * q3;
		float q1_q0 = 2 * q1 * q0;

		ret.m00 = 1 - sq_q2 - sq_q3;
		ret.m01 = q1_q2 - q3_q0;
		ret.m02 = q1_q3 + q2_q0;
		ret.m03 = 0.0f;

		ret.m10 = q1_q2 + q3_q0;
		ret.m11 = 1 - sq_q1 - sq_q3;
		ret.m12 = q2_q3 - q1_q0;
		ret.m13 = 0.0f;

		ret.m20 = q1_q3 - q2_q0;
		ret.m21 = q2_q3 + q1_q0;
		ret.m22 = 1 - sq_q1 - sq_q2;
		ret.m23 = 0.0f;

		ret.m30 = 0.0f;
		ret.m31 = 0.0f;
		ret.m32 = 0.0f;
		ret.m33 = 1.0f;

		return ret;
	}

	void CloseClient() {
		thread.Abort();
		receivingUdpClient.Close();
	}

	void OnApplicationQuit() {
		CloseClient();
	}
}
