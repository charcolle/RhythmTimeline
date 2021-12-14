using System;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace charcolle.TimelineAssets
{
    [Serializable]
    public class RhythmBehaviour : PlayableBehaviour
    {
        public TimelineClip clip;

        public double Bpm = 120f;
        public int Bar = 4;
        public int Beat = 4;
        public bool IsAudioPlay = false;

    }

}