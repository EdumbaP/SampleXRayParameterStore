using System;
using System.Runtime.CompilerServices;
using Amazon.XRay.Recorder.Core;

namespace SampleXRayParameterStore.Xray
{
    public class XRaySubsegmentFactory
    {
        private readonly IAWSXRayRecorder _xRayRecorder;

        public XRaySubsegmentFactory(IAWSXRayRecorder xRayRecorder)
        {
            _xRayRecorder = xRayRecorder;
        }

        public XRaySubsegment Create(string className, [CallerMemberName] string subsegment = null)
        {
            return new XRaySubsegment(_xRayRecorder, $"{className}.{subsegment}");
        }

        public void TryAddException(Exception ex)
        {
            if (_xRayRecorder.TraceContext.IsEntityPresent())
            {
                _xRayRecorder.AddException(ex);
            }
        }
    }
}
