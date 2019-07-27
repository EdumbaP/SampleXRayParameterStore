using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Sampling.Local;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleXRayParameterStore.Helper;
using SampleXRayParameterStore.Xray;

namespace SampleXRayParameterStore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddXRayTracing(this IServiceCollection services, IConfiguration config)
        {
            var xRaySamplingRulePath = XRaySamplingSettings.SamplingRuleFileMap[XRaySamplingRule.All];

            IAWSXRayRecorder xRayRecorder = new AWSXRayRecorderBuilder()
                .WithSamplingStrategy(new LocalizedSamplingStrategy(xRaySamplingRulePath))
                .Build();

            AWSXRayRecorder.InitializeInstance(config);
            AWSSDKHandler.RegisterXRayForAllServices();

            services
                .AddSingleton(xRayRecorder)
                .AddSingleton<XRaySegmentFactory>()
                .AddSingleton<XRaySubsegmentFactory>();

            return services;
        }
    }
}
