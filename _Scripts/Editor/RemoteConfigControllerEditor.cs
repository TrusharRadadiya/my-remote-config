using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace MyRemoteConfig.Editor
{
    [CustomEditor(typeof(RemoteConfigController))]
    public class RemoteConfigControllerEditor : UnityEditor.Editor
    {
        private SerializedProperty _UGSEnvProp;
        private SerializedProperty _remoteConfigEnvProp;
        
        private SerializedProperty _selectedUGSEnvProp;
        private SerializedProperty _selectedRemoteConfigEnvProp;
        
        private int _selectedUGSEnvIndex;
        private int _selectedRemoteConfigEnvIndex;

        private readonly List<string> _UGSEnvironments = new();
        private readonly List<RemoteConfigEnvironment> _remoteConfigEnvironments = new();
        
        private void OnEnable()
        {
            _UGSEnvProp = serializedObject.FindProperty("_UGSEnvironments");
            _remoteConfigEnvProp = serializedObject.FindProperty("_remoteConfigEnvironments");

            _selectedUGSEnvProp = serializedObject.FindProperty("_selectedUGSEnvironment");
            _selectedRemoteConfigEnvProp = serializedObject.FindProperty("_selectedEnvironmentID");
        }

        public override void OnInspectorGUI()
        {
            _remoteConfigEnvironments.Clear();
            for (int i = 0; i < _remoteConfigEnvProp.arraySize; i++)
            {
                RemoteConfigEnvironment env = (RemoteConfigEnvironment)_remoteConfigEnvProp.GetArrayElementAtIndex(i).boxedValue;
                _remoteConfigEnvironments.Add(env);
            }
            
            _UGSEnvironments.Clear();
            for (int i = 0; i < _UGSEnvProp.arraySize; i++)
            {
                string env = _UGSEnvProp.GetArrayElementAtIndex(i).stringValue;
                _UGSEnvironments.Add(env);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Selected UGS Environment");
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            _selectedUGSEnvIndex = EditorGUILayout.Popup(_selectedUGSEnvIndex, _UGSEnvironments.ToArray());
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Selected Remote Config Environment");
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            string[] remoteConfigEnvOptions = _remoteConfigEnvironments.Select(x => x.Name).ToArray();
            _selectedRemoteConfigEnvIndex = EditorGUILayout.Popup(_selectedRemoteConfigEnvIndex, remoteConfigEnvOptions);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            if (_UGSEnvironments.Count > 0) _selectedUGSEnvProp.stringValue = _UGSEnvironments[_selectedUGSEnvIndex];
            if (_remoteConfigEnvironments.Count > 0) _selectedRemoteConfigEnvProp.stringValue = _remoteConfigEnvironments[_selectedRemoteConfigEnvIndex].ID;
            
            base.OnInspectorGUI();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}