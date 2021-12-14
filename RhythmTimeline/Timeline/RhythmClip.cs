using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace charcolle.TimelineAssets
{
    [Serializable]
    [DisplayName( "Rhythm Clip" )]
    public class RhythmClip : PlayableAsset, ITimelineClipAsset
    {
        public RhythmBehaviour template = new RhythmBehaviour();

        public ClipCaps clipCaps => ClipCaps.SpeedMultiplier;

        public override double duration {
            get {
                return (60f / template.Bpm);
            }
        }

        public override Playable CreatePlayable( PlayableGraph graph, GameObject owner )
        {
            var playable = ScriptPlayable<RhythmBehaviour>.Create( graph );
            var RhythmProperty = (RhythmBehaviour)playable.GetBehaviour();

            RhythmProperty.clip = template.clip;
            RhythmProperty.Bpm = template.Bpm;
            RhythmProperty.Bar = template.Bar;
            RhythmProperty.Beat = template.Beat;
            RhythmProperty.IsAudioPlay = template.IsAudioPlay;

            return playable;
        }

    }
}