using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MusicData : MonoBehaviour {
	public static SCR_MusicData instance;
	
	
	public AudioClip[] music;
	
	
	public float[][] data = new float[10][];
	private int songSelected = 0;
	
	private void Awake () {
		instance = this;
	}
	
	
	private void Start () {
		data[0] = new float[] {
			0.715625f,
			1.286458f,
			1.857292f,
			2.429167f,
			3f,
			3.570833f,
			4.142709f,
			4.713542f,
			5.286458f,
			5.857292f,
			6.429167f,
			7f,
			7.570834f,
			8.139584f,
			8.713542f,
			9.857292f,
			10.42917f,
			11f,
			11.57083f,
			12.14271f,
			12.71354f,
			13.28646f,
			14.42917f,
			15f,
			15.57083f,
			16.14271f,
			16.71354f,
			17f,
			17.28646f,
			17.57708f,
			17.85729f,
			19f,
			19.57188f,
			20.14271f,
			20.71458f,
			21.28646f,
			21.85729f,
			22.42917f,
			23f,
			23.57188f,
			24.14271f,
			24.71354f,
			25.28646f,
			25.85729f,
			26.42917f,
			27f,
			27.57084f,
			28.14271f,
			28.71354f,
			29.28646f,
			29.85729f,
			30.42917f,
			31f,
			31.57084f,
			32.14271f,
			32.71458f,
			33.28646f,
			33.85833f,
			34.42917f,
			35f,
			35.27708f,
			35.57188f,
			35.875f,
			36.14375f,
			37.28646f,
			37.85729f,
			38.42813f,
			39f,
			39.57084f,
			40.14271f,
			40.71354f,
			41.28646f,
			41.85729f,
			42.42813f,
			43f,
			43.57084f,
			44.14271f,
			44.42396f,
			44.71354f,
			45.00938f,
			45.28646f,
			45.85729f,
			46.42813f,
			47f,
			47.57084f,
			48.14271f,
			48.71354f,
			49.28542f,
			49.8573f,
			50.42917f,
			51f,
			51.57188f,
			52.14271f,
			52.71354f,
			53.28646f,
			53.58646f,
			53.8573f,
			54.17084f,
			54.42917f,
			55.57188f,
			56.14271f,
			56.71354f,
			57.28646f,
			57.8573f,
			58.42917f,
			59f,
			59.57084f,
			60.14271f,
			60.71354f,
			61.28646f,
			61.8573f,
			62.42813f,
			63f,
			63.57084f,
			64.14167f,
			64.71355f,
			65.28542f,
			65.85729f,
			66.42813f,
			67f,
			67.57188f,
			68.14272f,
			68.71458f,
			69.28646f,
			69.85834f,
			70.42917f,
			71f,
			71.57084f,
			71.84688f,
			72.14272f,
			72.44063f,
			72.71355f
		};
	}
	
	public void LoadMusic(int song) {
		songSelected = song;
		GetComponent<AudioSource>().clip = music[songSelected];
	}
	
	public float[] GetData() {
		return data[songSelected];
	}
}
