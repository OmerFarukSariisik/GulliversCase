using Challenges._7._Abstract_DOTween.Scripts;
using DG.DOTweenEditor;
using UnityEditor;
using UnityEngine;

namespace Challenges._7._Abstract_DOTween.Editor
{
    [CustomEditor(typeof(DoTweenAnimation),true)]
    public class DoTweenAnimationEditor : UnityEditor.Editor
    {
        private SerializedProperty _padProperty;
        private SerializedProperty _objProperty;
        private SerializedProperty _objShadeProperty;
        private bool foldOut = true;
        private void OnEnable()
        {
            _padProperty = serializedObject.FindProperty("floorPad");
            _objProperty = serializedObject.FindProperty("centerObject");
            _objShadeProperty = serializedObject.FindProperty("centerObjectShade");
        }

        public override void OnInspectorGUI()
        {
            if (_padProperty.objectReferenceValue == null || _objProperty.objectReferenceValue == null ||
                _objShadeProperty.objectReferenceValue == null)
            {
                foldOut = EditorGUILayout.BeginFoldoutHeaderGroup(foldOut, "References");
                if(_padProperty.objectReferenceValue==null)
                    EditorGUILayout.PropertyField(_padProperty);
                if(_objProperty.objectReferenceValue==null)
                    EditorGUILayout.PropertyField(_objProperty);
                if(_objShadeProperty.objectReferenceValue==null)
                    EditorGUILayout.PropertyField(_objShadeProperty);
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            
            
            var abstractPickUp = (AbstractPickUpScript)serializedObject.targetObject;
            if (abstractPickUp.inPreview)
            {
                if (GUILayout.Button("Stop"))
                {
                    abstractPickUp.inPreview = false;
                    DOTweenEditorPreview.Stop(true);
                    abstractPickUp.AfterEnd();
                }
            }
            else
            {
                if (GUILayout.Button("Preview"))
                {
                    abstractPickUp.inPreview = true;
                    abstractPickUp.BeforeStart();
                    var tween = abstractPickUp.StartPreview();
                    DOTweenEditorPreview.PrepareTweenForPreview(tween);
                    DOTweenEditorPreview.Start();
                }
            }

            serializedObject.ApplyModifiedProperties();
            
            EditorGUILayout.Separator();
            base.OnInspectorGUI();
        }
    }
}
