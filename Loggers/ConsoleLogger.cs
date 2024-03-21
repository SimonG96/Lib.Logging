// Author: Gockner, Simon
// Created: 2019-09-26
// Copyright(c) 2019 SimonG. All Rights Reserved.

using Lib.Logging.Interfaces;
using Lib.Logging.Loggers.Interfaces;

namespace Lib.Logging.Loggers;

/// <summary>
/// An <see cref="ILogger"/> that writes log messages to the <see cref="Console"/>
/// </summary>
public class ConsoleLogger : IConsoleLogger
{
    /// <summary>
    /// Write the given <see cref="string"/> to the <see cref="Console"/>
    /// </summary>
    /// <param name="message">The <see cref="ILogMessage{T}"/></param>
    public async Task Write<T>(ILogMessage<T> message) => await Task.Run(() => Console.Write(message.ToString()));

    /// <summary>
    /// <see cref="DisposeAsync"/> the <see cref="ConsoleLogger"/>
    /// </summary>
    public async ValueTask DisposeAsync() => await Task.Run(Console.Clear);
}