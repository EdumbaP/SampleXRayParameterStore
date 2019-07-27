﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SampleXRayParameterStore.Xray;

namespace SampleXRayParameterStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly XRaySegmentFactory _xRaySegmentFactory;

        public ValuesController(XRaySegmentFactory xRaySegmentFactory)
        {
            _xRaySegmentFactory = xRaySegmentFactory;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var correlationId = Guid.NewGuid().ToString();

            using (XRaySegment consumeSegment =
                _xRaySegmentFactory.Create($"GetControllerValue", correlationId))
            {
                return new string[] {"value1", "value2"};
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}