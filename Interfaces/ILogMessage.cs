// Author: Gockner, Simon
// Created: 2019-09-27
// Copyright(c) 2019 SimonG. All Rights Reserved.

namespace Lib.Logging.Interfaces;

/// <summary>
/// An interface that is used to handle log messages
/// </summary>
public interface ILogMessage<T>
{
    /// <summary>
    /// The <see cref="LogLevels"/> of the <see cref="ILogMessage{T}"/>
    /// </summary>
    LogLevels LogLevel { get; set; }

    /// <summary>
    /// The <see cref="Timestamp"/> of the <see cref="ILogMessage{T}"/>
    /// </summary>
    DateTime Timestamp { get; set; }

    /// <summary>
    /// The <see cref="ILogComponent"/> of the <see cref="ILogMessage{T}"/>
    /// </summary>
    ILogComponent? Component { get; set; }

    /// <summary>
    /// The <see cref="Message"/> of the <see cref="ILogMessage{T}"/>
    /// </summary>
    string? Message { get; set; }
}