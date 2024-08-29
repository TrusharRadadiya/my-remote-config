# Custom unity package for remote configuration.
1. Before jumping into the unity editor follow the [unity remote config documentation](https://docs.unity.com/ugs/manual/remote-config/manual/WhatsRemoteConfig) to set up the unity remote config dashboard.
2. Now in the unity editor attach the "RemoteConfigController" script to the game object.
3. Now, there are two lists "UGS Environments" and "Remote Config Environments", add the name of the environments of Unity gaming services in the first list, and in the second list add the name and id of the remote config environment.
4. After that select the preferred environments for the UGS and remote config.
5. Now, to get the settings from the remote server there is event where you can assign the method to get the remote config data. (Note: to see how to access the remote properties you can refer to the above documentation)
