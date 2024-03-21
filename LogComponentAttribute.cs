// Author: Gockner, Simon
// Created: 2019-09-26
// Copyright(c) 2019 SimonG. All Rights Reserved.

using Lib.Logging.Interfaces;

namespace Lib.Logging;

/// <summary>
/// The <see cref="ILogComponent"/> attribute
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class LogComponentAttribute : Attribute, ILogComponent
{
    /// <summary>
    /// <see cref="LogComponentAttribute"/> constructor
    /// </summary>
    /// <param name="component">The <see cref="Component"/></param>
    public LogComponentAttribute(string component) => Component = component;

    /// <summary>
    /// The <see cref="Component"/>
    /// </summary>
    public string Component { get; }

    /// <summary>
    /// Returns the <see cref="Component"/> of this <see cref="ILogComponent"/>
    /// </summary>
    /// <returns>The <see cref="Component"/></returns>
    public override string ToString() => Component;
}