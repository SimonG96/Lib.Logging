// Author: Gockner, Simon
// Created: 2019-09-27
// Copyright(c) 2019 SimonG. All Rights Reserved.

using Lib.Logging.Interfaces;

namespace Lib.Logging;

/// <summary>
/// Implementation of <see cref="ILogMessage{T}"/>
/// </summary>
public class LogMessage<T> : ILogMessage<T>
{
    /// <summary>
    /// The <see cref="LogLevels"/> of the <see cref="LogMessage{T}"/>
    /// </summary>
    public LogLevels LogLevel { get; set; }

    /// <summary>
    /// The <see cref="Timestamp"/> of the <see cref="LogMessage{T}"/>
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// The <see cref="ILogComponent"/> of the <see cref="LogMessage{T}"/>
    /// </summary>
    public ILogComponent? Component { get; set; }

    /// <summary>
    /// The <see cref="Message"/> of the <see cref="LogMessage{T}"/>
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Builds the <see cref="LogMessage{T}"/> out of all parts and returns it as a <see cref="string"/>
    /// </summary>
    /// <returns>The <see cref="LogMessage{T}"/> as a <see cref="string"/></returns>
    public override string ToString() => $"{Timestamp:u}: [{Component}] [{typeof(T).Name}] {Message}\n";
}