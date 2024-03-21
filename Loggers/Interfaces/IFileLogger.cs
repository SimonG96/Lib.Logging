// Author: Gockner, Simon
// Created: 2019-11-24
// Copyright(c) 2019 SimonG. All Rights Reserved.

namespace Lib.Logging.Loggers.Interfaces;

public interface IFileLogger : ILogger
{
    /// <summary>
    /// Write information about this <see cref="IFileLogger"/>
    /// </summary>
    /// <param name="writeFunction">The write function</param>
    Task WriteInformation(Func<string, Task> writeFunction);
}