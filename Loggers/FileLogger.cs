// Author: Gockner, Simon
// Created: 2019-09-26
// Copyright(c) 2019 SimonG. All Rights Reserved.

using Lib.Logging.Interfaces;
using Lib.Logging.Loggers.Interfaces;

namespace Lib.Logging.Loggers;

/// <summary>
/// An <see cref="ILogger"/> that writes log messages to a set file
/// </summary>
public class FileLogger : IFileLogger
{
    private readonly StreamWriter _fileWriter;
    private readonly Timer _timer;

    private readonly SemaphoreSlim _lockObject = new(1);
    private readonly string _fullFilePath;

    /// <summary>
    /// Constructor for <see cref="FileLogger"/>
    /// </summary>
    /// <param name="filePath">The directory of the LogFile</param>
    /// <param name="fileName">The path of the LogFile</param>
    public FileLogger(string filePath, string fileName)
    {
        Directory.CreateDirectory(filePath);
        _fullFilePath = Path.Combine(filePath, fileName);
        
        _fileWriter = new StreamWriter(_fullFilePath, true);
        _timer = new Timer(TimerCallback, null, 0, 1000);
    }

    /// <summary>
    /// Write the given <see cref="string"/> to the file
    /// </summary>
    /// <param name="message">The <see cref="ILogMessage{T}"/></param>
    public async Task Write<T>(ILogMessage<T> message)
    {
        await _lockObject.WaitAsync();
        try
        {
            await _fileWriter.WriteAsync(message.ToString());
        }
        finally
        {
            _lockObject.Release();
        }
    }

    /// <summary>
    /// Write information about this <see cref="FileLogger"/>
    /// </summary>
    /// <param name="writeFunction">The write function</param>
    public async Task WriteInformation(Func<string, Task> writeFunction) => await writeFunction($"LogFile path: {_fullFilePath}");
    private async void TimerCallback(object? state) => await Flush();

    private async Task Flush()
    {
        await _lockObject.WaitAsync();
        try
        {
            await _fileWriter.FlushAsync();
        }
        finally
        {
            _lockObject.Release();
        }
    }

    /// <summary>
    /// <see cref="DisposeAsync"/> the <see cref="FileLogger"/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _timer.DisposeAsync();

        await Flush();
        await _fileWriter.DisposeAsync();

        _lockObject.Dispose();
    }
}