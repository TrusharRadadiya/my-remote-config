using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Core.Environments;
using UnityEngine.Events;

namespace MyRemoteConfig
{
    public class RemoteConfigController : MonoBehaviour
    {
        [SerializeField]  private List<string> _UGSEnvironments = new();
        [SerializeField] private List<RemoteConfigEnvironment> _remoteConfigEnvironments = new();
        
        [SerializeField, HideInInspector] private string _selectedUGSEnvironment;
        [SerializeField, HideInInspector] private string _selectedEnvironmentID;

        [HideInInspector] public UnityEvent<RuntimeConfig> OnDataFetched;
            
        private void Awake()
        {
            RemoteConfigService.Instance.FetchCompleted += ApplyRemoteConfig;
            RemoteConfigService.Instance.SetEnvironmentID(_selectedEnvironmentID);
        }

        private async void Start()
        {
            await InitializeRemoteConfigAsync();
            await RemoteConfigService.Instance.FetchConfigsAsync(new { }, new { });
        }

        private async Task InitializeRemoteConfigAsync()
        {
            InitializationOptions options = new InitializationOptions().SetEnvironmentName(_selectedUGSEnvironment);
            await UnityServices.InitializeAsync(options);

            if (!AuthenticationService.Instance.IsSignedIn) await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        private void ApplyRemoteConfig(ConfigResponse configResponse) => OnDataFetched?.Invoke(RemoteConfigService.Instance.appConfig);
    }

    [Serializable]
    public struct RemoteConfigEnvironment
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public string ID { get; private set; }
    }
}