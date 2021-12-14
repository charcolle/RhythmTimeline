using UnityEditor;
using UnityEngine;
using charcolle.TimelineAssets;

[CustomPropertyDrawer( typeof( RhythmBehaviour ) )]
public class RhythmClipPropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
    {
        return EditorGUIUtility.singleLineHeight * 2;
    }

    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
    {
        var bpmProperty = property.FindPropertyRelative("Bpm");
        var barProperty = property.FindPropertyRelative( "Bar" );
        var beatProperty = property.FindPropertyRelative( "Beat" );
        var isAudioPlayProperty = property.FindPropertyRelative( "IsAudioPlay" );

        var singleFieldRect = new Rect( position.x, position.y, position.width, EditorGUIUtility.singleLineHeight );


        EditorGUILayout.LabelField( "BPM" );
        bpmProperty.doubleValue = EditorGUILayout.IntSlider( (int)bpmProperty.doubleValue, 50, 250 );

        EditorGUILayout.Space( 2f );
        GUILayout.Box( "", GUILayout.Height( 2 ), GUILayout.ExpandWidth( true ) );
        EditorGUILayout.Space( 2f );

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField( "Bar", GUILayout.Width( 100f ) );
        barProperty.intValue = EditorGUILayout.IntSlider( barProperty.intValue, 2, 9 );
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField( "Beat", GUILayout.Width( 100f ) );
        beatProperty.intValue = EditorGUILayout.IntPopup( beatProperty.intValue, new string[] { "4", "8" }, new int[] { 4, 8 } );
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space( 5f );

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField( "Is Metronome Audio Play", GUILayout.Width( 160f ) );
        isAudioPlayProperty.boolValue = EditorGUILayout.Toggle( isAudioPlayProperty.boolValue );
        EditorGUILayout.EndHorizontal();
    }

}
