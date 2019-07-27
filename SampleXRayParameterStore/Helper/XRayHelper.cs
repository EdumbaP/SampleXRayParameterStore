using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleXRayParameterStore.Helper
{
    public enum XRaySamplingRule
    {
        None,
        Default,
        All
    }

    public class XRaySamplingSettings
    {
        public static readonly Dictionary<XRaySamplingRule, string> SamplingRuleFileMap = new Dictionary<XRaySamplingRule, string>
        {
            { XRaySamplingRule.None, "XRaySampleNoneRule.json" },
            { XRaySamplingRule.Default, "XRaySampleDefaultRule.json" },
            { XRaySamplingRule.All, "XRaySampleAllRule.json" }
        };
    }
}
