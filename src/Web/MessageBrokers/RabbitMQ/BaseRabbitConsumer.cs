﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Web.MessageBrokers.Exceptions;

namespace Web.MessageBrokers.RabbitMQ
{
    public abstract class BaseRabbitConsumer<TMessage> : IConsumer<TMessage>
        where TMessage : class, new()
    {
        protected ILogger Logger { get; }

        protected BaseRabbitConsumer(ILogger logger)
        {
            Logger = logger;
        }

#pragma warning disable UseAsyncSuffix // Use Async suffix
        public async Task Consume(ConsumeContext<TMessage> context)
#pragma warning restore UseAsyncSuffix // Use Async suffix
        {
            try
            {
                Logger.LogInformation($"Consuming message {typeof(TMessage).Name}");
                await ConsumeAsync(context);
            }
            catch (Exception exception)
            {
                throw new CannotConsumeMessageException<TMessage>(exception);
            }
        }

        protected abstract Task ConsumeAsync(ConsumeContext<TMessage> context);
    }
}