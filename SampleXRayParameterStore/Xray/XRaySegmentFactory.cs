using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Sampling;

namespace SampleXRayParameterStore.Xray
{
    public class XRaySegmentFactory
    {
        private readonly IAWSXRayRecorder _xRayRecorder;

        public XRaySegmentFactory(IAWSXRayRecorder xRayRecorder)
        {
            _xRayRecorder = xRayRecorder;
        }

        public XRaySegment Create(string serviceName)
        {
            return XRaySegment.Create(_xRayRecorder, serviceName);
        }

        public XRaySegment Create(string serviceName, string traceHeader, SampleDecision sampleDecision = SampleDecision.Unknown)
        {
            return XRaySegment.Create(_xRayRecorder, serviceName, traceHeader, sampleDecision);
        }
    }
}
