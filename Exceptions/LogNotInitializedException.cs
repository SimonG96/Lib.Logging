// Author: Simon Gockner
// Created: 2020-09-12
// Copyright(c) 2020 SimonG. All Rights Reserved.

namespace Lib.Logging.Exceptions;

public class LogNotInitializedException : Exception
{
    public LogNotInitializedException()
        : base("Log is not initialized yet.")
    {
            
    }
}