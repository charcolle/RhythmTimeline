using UnityEngine.Playables;

namespace charcolle.TimelineAssets
{
    public class RhythmMixerBehaviour : PlayableBehaviour
    {

        private RhythmEngine Rhythm;

        public override void ProcessFrame( Playable playable, FrameData info, object playerData )
        {
            if (Rhythm == null)
            {
                Rhythm = playerData as RhythmEngine;
                var scriptPlayable = (ScriptPlayable<RhythmBehaviour>)playable.GetInput( 0 ); // awful..
                var mp = (RhythmBehaviour)scriptPlayable.GetBehaviour();
            }

            if (Rhythm == null)
                return;

            bool hasInputWeight = false;
            RhythmBehaviour RhythmProperties;
            double currentClipStartTime = 0;
            double currentBPM = 0;
            int currentBar=0;
            int currentBeat=0;
            bool currentIsAudioPlay = false;
            var inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                var inputWeight = playable.GetInputWeight( i );
                if (inputWeight > 0)
                {
                    hasInputWeight = true;
                    var scriptPlayable = (ScriptPlayable<RhythmBehaviour>)playable.GetInput( i );
                    RhythmProperties = scriptPlayable.GetBehaviour();
                    currentClipStartTime = RhythmProperties.clip.start;
                    currentBPM = RhythmProperties.Bpm;
                    currentBar = RhythmProperties.Bar;
                    currentBeat = RhythmProperties.Beat;
                    currentIsAudioPlay = RhythmProperties.IsAudioPlay;
                }
            }

            if (hasInputWeight)
            {
                Rhythm.ProcessFrame( playable.GetTime(), currentBPM, currentBar, currentBeat, currentIsAudioPlay );
            } else
            {
                Rhythm.Stop();
            }
        }
    }

}
