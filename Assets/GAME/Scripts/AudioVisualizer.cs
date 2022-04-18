using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * REFER TO: http://answers.unity3d.com/questions/157940/getoutputdata-and-getspectrumdata-they-represent-t.html 
 * Many Thanks to aldonaletto · Aug 21, 2011 at 05:39 AM 
 * 
*/
public class AudioVisualizer : MonoBehaviour {

	public static AudioVisualizer _instance;

	[Range(1, 100)] public float heightMultiplier;
	public FFTWindow fftWindow;
	private AudioSource audioS;

	[HideInInspector]
	public float rmsValue;   // sound level - RMS
	//[HideInInspector]
	public float dbValue;    // sound level - dB
	[HideInInspector]
	public float pitchValue; // sound pitch - Hz

	[Range(64, 8192)] public int qSamples = 1024;  // array size
	float refValue = 0.1f; 		  // RMS value for 0 dB
	float threshold = 0.02f;      // minimum amplitude to extract pitch

	public Image micGraphicUI; 


	private float[] samples ; // audio samples
	private float[] spectrum; // audio spectrum
	private float   fSample;

	/*
	 * The intensity of the frequencies found between 0 and 44100 will be
	 * grouped into 1024 elements. So each element will contain a range of about 43.06 Hz.
	 * The average human voice spans from about 60 hz to 9k Hz
	 * we need a way to assign a range to each object that gets animated. that would be the best way to control and modify animations.
	*/

	void Awake(){
		_instance = this;
	}

	void Start(){
		audioS = GetComponent<AudioSource> ();

		samples = new float[qSamples];
		spectrum = new float[qSamples];
		fSample = AudioSettings.outputSampleRate;
	}
		

	void Update() {
		AnalyzeSound();

		//print ("Pitch:" + pitchValue + "  DB:" + dbValue + "  RMS:" + rmsValue);
		//print("DB: " + dbValue);

		if (micGraphicUI)
			micGraphicUI.fillAmount = (dbValue * -1) / 100f; 
	}

	void AnalyzeSound(){
		
		audioS.GetOutputData(samples, 0); // fill array with samples
		int i;
		float sum = 0;
		for (i=0; i < qSamples; i++){
			sum += samples[i]*samples[i]; // sum squared samples
		}

		rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
		dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB

		if (dbValue < -160) dbValue = -160; // clamp it to -160dB min

		// get sound spectrum
		audioS.GetSpectrumData(spectrum, 0, fftWindow);
		float maxV = 0;
		int   maxN = 0;

		for (i=0; i < qSamples; i++){ // find max 
			if (spectrum[i] > maxV && spectrum[i] > threshold){
				maxV = spectrum[i];
				maxN = i; // maxN is the index of max
			}
		}

		float freqN = maxN; // pass the index to a float variable
		if (maxN > 0 && maxN < qSamples-1){ // interpolate index using neighbours
			var dL = spectrum[maxN-1]/spectrum[maxN];
			var dR = spectrum[maxN+1]/spectrum[maxN];
			freqN += 0.5f*(dR*dR - dL*dL);
		}

		pitchValue = freqN*(fSample/2)/qSamples; // convert index to frequency
	}
		

}