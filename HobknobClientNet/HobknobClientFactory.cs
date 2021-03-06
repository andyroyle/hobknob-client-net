﻿using System;

namespace HobknobClientNet
{
    public class HobknobClientFactory
    {
        public HobknobClient Create(string etcdHost, int etcdPort, string applicationName, TimeSpan cacheUpdateInterval)
        {
            var etcdKeysPath = string.Format("http://{0}:{1}/v2/keys/", etcdHost, etcdPort);

            var etcdClient = new EtcdClient(new Uri(etcdKeysPath));
            var featureToggleProvider = new FeatureToggleProvider(etcdClient, applicationName);
            var featureToggleCache = new FeatureToggleCache(featureToggleProvider, cacheUpdateInterval);
            var hobknobClient = new HobknobClient(featureToggleCache, applicationName);

            featureToggleCache.Initialize();

            return hobknobClient;
        }
    }
}
