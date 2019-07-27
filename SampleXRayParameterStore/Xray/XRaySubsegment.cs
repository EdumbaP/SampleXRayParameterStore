using System;
using Amazon.XRay.Recorder.Core;

namespace SampleXRayParameterStore.Xray
{
    public class XRaySubsegment : IDisposable
    {
        private readonly IAWSXRayRecorder _xRayRecorder;

        public XRaySubsegment(IAWSXRayRecorder xRayRecorder, string subsegment)
        {
            _xRayRecorder = xRayRecorder;

            _xRayRecorder.BeginSubsegment(subsegment);
            _xRayRecorder.SetNamespace("remote");
        }

        public void AddException(Exception ex)
        {
            _xRayRecorder.AddException(ex);
        }

        public void Dispose()
        {
            _xRayRecorder.EndSubsegment();
        }
    }
}