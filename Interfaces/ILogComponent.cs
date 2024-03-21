// Author: Gockner, Simon
// Created: 2019-09-26
// Copyright(c) 2019 SimonG. All Rights Reserved.

namespace Lib.Logging.Interfaces;

/// <summary>
/// The <see cref="ILogComponent"/>
/// </summary>
public interface ILogComponent
{
    /// <summary>
    /// The <see cref="Component"/>
    /// </summary>
    string Component { get; }
}