using Amazon.XRay.Recorder.Core;
using System;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Amazon.XRay.Recorder.Core.Sampling;

namespace SampleXRayParameterStore.Xray
{
    public class XRaySegment : IDisposable
    {
        private readonly IAWSXRayRecorder _xRayRecorder;

        private XRaySegment(IAWSXRayRecorder xRayRecorder)
        {
            _xRayRecorder = xRayRecorder;
        }

        public static XRaySegment Create(IAWSXRayRecorder xRayRecorder, string serviceName)
        {
            xRayRecorder.BeginSegment(serviceName);
            xRayRecorder.AddAnnotation("Environment", "Test");
            xRayRecorder.AddAnnotation("Version", "1.0");

            return new XRaySegment(xRayRecorder);
        }

        public static XRaySegment Create(IAWSXRayRecorder xRayRecorder, string serviceName, string traceHeader, SampleDecision sampleDecision = SampleDecision.Unknown)
        {
            GetSamplingDetails(xRayRecorder, serviceName, traceHeader, out string traceId, out SamplingResponse samplingResponse, out string parentId);

            xRayRecorder.BeginSegment(
                serviceName, traceId, parentId, sampleDecision == SampleDecision.Unknown ? samplingResponse : new SamplingResponse(sampleDecision));
            xRayRecorder.AddAnnotation("Environment", "Test");
            xRayRecorder.AddAnnotation("Version", "1.0");

            return new XRaySegment(xRayRecorder);
        }

        public void AddException(Exception ex)
        {
            _xRayRecorder.AddException(ex);
        }

        public void Dispose()
        {
            _xRayRecorder.EndSegment();
        }

        private static void GetSamplingDetails(
            IAWSXRayRecorder xRayRecorder, string serviceName, string traceHeader, out string traceId, out SamplingResponse samplingResponse,
            out string parentId)
        {
            if (TraceHeader.TryParse(traceHeader, out TraceHeader header))
            {
                traceId = header.RootTraceId;
                samplingResponse = new SamplingResponse { SampleDecision = header.Sampled };
                parentId = header.ParentId;
            }
            else
            {
                traceId = TraceId.NewId();
                samplingResponse = xRayRecorder.SamplingStrategy.ShouldTrace(new SamplingInput { ServiceName = serviceName });
                parentId = null;
            }
        }
    }

}
