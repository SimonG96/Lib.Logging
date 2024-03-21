// Author: Gockner, Simon
// Created: 2019-11-24
// Copyright(c) 2019 SimonG. All Rights Reserved.

using Lib.Logging.Loggers.Interfaces;

namespace Lib.Logging.Loggers.Factories;

public interface IConsoleLoggerFactory
{
    IConsoleLogger Create();
}