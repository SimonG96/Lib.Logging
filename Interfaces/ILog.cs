// Author: Gockner, Simon
// Created: 2019-09-26
// Copyright(c) 2019 SimonG. All Rights Reserved.

using Lib.Logging.Loggers.Interfaces;

namespace Lib.Logging.Interfaces;

/// <summary>
/// The main logging interface
/// </summary>
public interface ILog : IAsyncDisposable
{
    /// <summary>
    /// Initialize the <see cref="ILog"/>
    /// </summary>
    /// <returns>True if successful, false if not</returns>
    bool InitializeLog();

    /// <summary>
    /// Set the <see cref="LogLevels"/> of the current <see cref="ILog"/> instance
    /// </summary>
    /// <param name="logLevel">The <see cref="LogLevels"/></param>
    void SetLogLevel(LogLevels logLevel);

    /// <summary>
    /// Add an <see cref="ILogger"/> to the <see cref="ILog"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    void AddLogger(ILogger logger);

    /// <summary>
    /// Remove an <see cref="ILogger"/> from the <see cref="ILog"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    void RemoveLogger(ILogger logger);

    /// <summary>
    /// Write a header to all loggers
    /// </summary>
    /// <returns>A <see cref="Task"/> to wait on</returns>
    Task WriteLogHeader<T>(string applicationName);
}