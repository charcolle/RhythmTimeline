using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace charcolle.TimelineAssets
{
    [TrackColor( 1.0f, 0.0f, 0.0f )]
    [TrackBindingType( typeof( RhythmEngine ) )]
    [TrackClipType( typeof( RhythmClip ) )]
    public class RhythmTrack : TrackAsset
    {

        public override Playable CreateTrackMixer( PlayableGraph graph, GameObject go, int inputCount )
        {
            var clips = GetClips();
            foreach (var clip in clips)
            {
                clip.displayName = " ";
                var RhythmClip = clip.asset as RhythmClip;
                RhythmClip.template.clip = clip;
            }
            return ScriptPlayable<RhythmMixerBehaviour>.Create( graph, inputCount );
        }

    }

}