// Author: Gockner, Simon
// Created: 2019-09-26
// Copyright(c) 2019 SimonG. All Rights Reserved.

using Lib.Logging.Interfaces;

namespace Lib.Logging.Loggers.Interfaces;

/// <summary>
/// Base class for loggers
/// </summary>
public interface ILogger : IAsyncDisposable
{
    /// <summary>
    /// Write the given <see cref="ILogMessage{T}"/>
    /// </summary>
    /// <param name="message">The <see cref="ILogMessage{T}"/></param>
    Task Write<T>(ILogMessage<T> message);
}