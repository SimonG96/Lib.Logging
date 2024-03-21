// Author: Gockner, Simon
// Created: 2019-09-26
// Copyright(c) 2019 SimonG. All Rights Reserved.

namespace Lib.Logging.Interfaces;

/// <summary>
/// The available <see cref="LogLevels"/>
/// </summary>
public enum LogLevels
{
    /// <summary>
    /// No logging 
    /// </summary>
    None,

    /// <summary>
    /// Error logging
    /// </summary>
    Error,

    /// <summary>
    /// Default logging
    /// </summary>
    Default,

    /// <summary>
    /// Advanced logging
    /// </summary>
    Advanced,

    /// <summary>
    /// Debug logging
    /// </summary>
    Debug
}