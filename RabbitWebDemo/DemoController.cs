﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ_Eventbus.Eventbus;
using RabbitMQ_Eventbus.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitWebDemo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IRabbitMQEventbus eventbus;

        public DemoController(IRabbitMQEventbus eventbus)
        {
            this.eventbus = eventbus;
        }

        [HttpGet()]
        public string Default()
        {

            return $"Send a number by going to Demo/send/<number>.";
        }

        [HttpGet("send/{number}")]
        public string Send(int number)
        {
            eventbus.Publish(new RabbitMQMessage(new MessageDestination("numberExchange", "num"), $"{number}"));

            return $"Sent: {number}";
        }
    }
}
