using UnityEngine;
using UnityEngine.Events;
using UniRx;

// Rhythm algorithm from https://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html

[RequireComponent( typeof( AudioSource ) )]
public class RhythmEngine : MonoBehaviour
{
    [Header("Rhythm Event")]
    public UnityEvent OnBar;
    public UnityEvent OnBeat;
    [Header( "Rhythm Event with Count" )]
    public UnityEvent<int> OnBeatWithCount;

    private double bpm = 140;
    private int bar = 4;
    private int beat = 4;
    private bool isAudioPlay = false;
    private bool isPlaying = false;

    private double currentTime;
    private int accent = 4;
    private float gain = 0.5f;
    private double nextTick = 0.0;
    private float amp = 0.0f;
    private float phase = 0.0f;
    private double sampleRate = 0.0;

    void Start()
    {
        MainThreadDispatcher.Initialize();
        sampleRate = AudioSettings.outputSampleRate;
    }

    public void Stop()
    {
        isPlaying = false;
    }

    public void ProcessFrame( double time, double bpm, int bar, int beat, bool isAudioPlay = true )
    {
        currentTime = time;
        this.bpm = bpm;
        this.bar = bar;
        this.beat = beat;
        this.isAudioPlay = isAudioPlay;

        OnPlay( time );
    }

    public void OnPlay( double startTime )
    {
        if (isPlaying)
            return;

        accent = bar;
        double startTick = startTime;
        nextTick = startTick * sampleRate;
        isPlaying = true;
    }

    void OnAudioFilterRead( float[] data, int channels )
    {
        if (!isPlaying)
            return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / beat;
        double sample = currentTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin( phase );
            int i = 0;
            if(isAudioPlay)
            {
                while (i < channels)
                {
                    data[n * channels + i] += x;
                    i++;
                }
            }

            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                amp = 1.0F;
                if (++accent > bar)
                {
                    accent = 1;
                    amp *= 2.0F;
                    MainThreadDispatcher.Post( _ => OnBar.Invoke(), null );
                }
                MainThreadDispatcher.Post( _ => OnBeat.Invoke(), null );
                MainThreadDispatcher.Post( _ => OnBeatWithCount.Invoke( accent ), null );
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
    }

}