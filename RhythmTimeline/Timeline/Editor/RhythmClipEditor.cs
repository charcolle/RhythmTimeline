using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor;
using UnityEditor.Timeline;

namespace charcolle.TimelineAssets
{
    [CustomTimelineEditor( typeof( RhythmClip ) )]
    public class RhythmClipEditor : ClipEditor
    {

        public override void DrawBackground( TimelineClip clip, ClipBackgroundRegion region )
        {
            var RhythmClip = clip.asset as RhythmClip;
            var duration = region.endTime - region.startTime;
            var rect = GetPreviewRect( clip.duration, region );

            BackgroundDrawer( rect );
            BarDrawer( rect, RhythmClip.template, clip.duration );
            BeatDrawer( rect, RhythmClip.template, clip.duration );
        }

        private void BackgroundDrawer( Rect rect )
        {
            rect.yMax -= rect.height * 0.1f;
            rect.yMin += rect.height * 0.1f;
            EditorGUI.DrawRect( rect, Color.grey );
        }

        private void BarDrawer( Rect rect,RhythmBehaviour clip, double duration )
        {
            var pixelPerSecond = rect.width / duration;
            var secondPerBeat = 60F / (clip.Bpm * (clip.Beat / 4));
            var pixelPerBeat = pixelPerSecond * secondPerBeat;

            var beatNum = (clip.Bpm * duration * (clip.Beat / 4)) / 60F;

            var icon = EditorGUIUtility.Load( "icons/blendKeySelected.png" ) as Texture2D;
            var guiColor = GUI.color;
            GUI.color = Color.clear;
            for (int i = 0; i < beatNum; i++)
            {
                if (i % clip.Bar != 0)
                    continue;

                var iconRect = new Rect( rect.x + (float)(i * pixelPerBeat) - 3.75f, rect.y + (rect.height * 0.5f) - 4f, 10f, 10f );
                EditorGUI.DrawTextureTransparent( iconRect, icon );
            }
            GUI.color = guiColor;
        }

        private void BeatDrawer( Rect rect, RhythmBehaviour clip, double duration )
        {
            var pixelPerSecond = rect.width / duration;
            var secondPerBeat = 60F / (clip.Bpm * (clip.Beat/4));
            var pixelPerBeat = pixelPerSecond * secondPerBeat;
            
            var beatNum = (clip.Bpm * duration * (clip.Beat / 4)) / 60F;
            var guiColor = GUI.color;
            GUI.color = Color.clear;
            var icon = EditorGUIUtility.Load( "icons/blendSampler.png" ) as Texture2D;
            for( int i = 0; i < beatNum; i++ )
            {
                if (i % clip.Bar == 0)
                    continue;

                var iconRect = new Rect( rect.x + (float)(i * pixelPerBeat) - 3f, rect.y + (rect.height * 0.5f) - 3f, 6f, 6f );
                EditorGUI.DrawTextureTransparent( iconRect, icon );
            }
            GUI.color = guiColor;
        }
        private Rect GetPreviewRect( double duration, ClipBackgroundRegion region )
        {
            var previewRect = region.position;

            var visibleTime = region.endTime - region.startTime;
            var pixelPerSecond = region.position.width / visibleTime;

            var inVisibleEndLength = pixelPerSecond * (duration - region.endTime);

            previewRect.xMin -= previewRect.x;
            previewRect.width += previewRect.x + (float)inVisibleEndLength;
            return previewRect;
        }

    }

}