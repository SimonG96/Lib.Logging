// Author: Gockner, Simon
// Created: 2019-09-26
// Copyright(c) 2019 SimonG. All Rights Reserved.

using System.Reflection;
using Lib.Logging.Exceptions;
using Lib.Logging.Interfaces;
using Lib.Logging.Loggers.Interfaces;
using Lib.Tools;

namespace Lib.Logging;

/// <summary>
/// The main logging class
/// </summary>
public class Log : ILog
{
    /// <summary>
    /// Constructor for <see cref="Log"/>
    /// </summary>
    public Log() => Loggers = new List<ILogger>();

    /// <summary>
    /// <see cref="List{T}"/> of <see cref="ILogger"/>s that are subscribed to this <see cref="Log"/>
    /// </summary>
    protected static List<ILogger>? Loggers { get; private set; }

    /// <summary>
    /// The <see cref="LogLevels"/> of this logging instance
    /// </summary>
    protected static LogLevels LogLevel { get; private set; } = LogLevels.Default; //TODO: Move LogLevel to ILogger? Allows to set logLevel separately for each ILogger

    /// <summary>
    /// Initialize the <see cref="ILog"/>
    /// </summary>
    /// <returns>True if successful, false if not</returns>
    public bool InitializeLog()
    {
        return true;
    }

    /// <summary>
    /// Set the <see cref="LogLevels"/> of the current <see cref="Log"/> instance
    /// </summary>
    /// <param name="logLevel">The <see cref="LogLevels"/></param>
    public void SetLogLevel(LogLevels logLevel)
    {
        LogLevel = logLevel;
        LogLevelChanged?.Invoke(this, LogLevel);
    }

    /// <summary>
    /// Add an <see cref="ILogger"/> to the <see cref="Log"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    public void AddLogger(ILogger logger) => Loggers?.Add(logger);

    /// <summary>
    /// Remove an <see cref="ILogger"/> from the <see cref="Log"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    public void RemoveLogger(ILogger logger) => Loggers?.Remove(logger);

    /// <summary>
    /// Write a given <see cref="Exception"/> to the set <see cref="ILogger"/>s
    /// </summary>
    /// <param name="ex">The <see cref="Exception"/></param>
    /// <typeparam name="T">The <see cref="Type"/> of the caller</typeparam>
    public static async Task Write<T>(Exception ex)
    {
        ILogComponent component = GetDefaultComponentFromType<T>();
        await Write<T>(component, ex);
    }

    /// <summary>
    /// Write a given <see cref="Exception"/> to the set <see cref="ILogger"/>s
    /// </summary>
    /// <param name="component">The <see cref="ILogComponent"/></param>
    /// <param name="ex">The <see cref="Exception"/></param>
    public static async Task Write<T>(ILogComponent component, Exception ex)
    {
        await Write<T>(component, LogLevels.Error, ex.Message);
        await Write<T>(component, LogLevels.Error, "");
        await Write<T>(component, LogLevels.Error, ex.StackTrace);
    }

    /// <summary>
    /// Write a given <see cref="AggregateException"/> to the set <see cref="ILogger"/>s
    /// </summary>
    /// <param name="ex">The <see cref="AggregateException"/></param>
    /// <typeparam name="T">The <see cref="Type"/> of the caller</typeparam>
    public static async Task Write<T>(AggregateException ex)
    {
        ILogComponent component = GetDefaultComponentFromType<T>();
        await Write<T>(component, ex);
    }

    public static async Task Write<T>(ILogComponent component, AggregateException ex)
    {
        await Write<T>(component, LogLevels.Error, ex.Message);
        await Write<T>(component, LogLevels.Error, "");
        await Write<T>(component, LogLevels.Error, ex.StackTrace);
        await Write<T>(component, LogLevels.Error, "");

        foreach (var innerException in ex.InnerExceptions)
        {
            await Write<T>(component, innerException);
            await Write<T>(component, LogLevels.Error, "");
        }
    }

    /// <summary>
    /// Write a given <see cref="string"/> with the <see cref="LogLevels.Default"/> to the set <see cref="ILogger"/>s
    /// </summary>
    /// <param name="line">The given <see cref="string"/></param>
    /// <typeparam name="T">The <see cref="Type"/> of the caller</typeparam>
    public static async Task Write<T>(string line)
    {
        ILogComponent component = GetDefaultComponentFromType<T>();
        await Write<T>(component, LogLevels.Default, line);
    }

    /// <summary>
    /// Write a given <see cref="string"/> with the <see cref="ILogComponent"/> of the calling <see cref="Assembly"/> and the <see cref="LogLevels"/> to the set <see cref="ILogger"/>s
    /// </summary>
    /// <param name="logLevel">The <see cref="LogLevels"/></param>
    /// <param name="line">The given <see cref="string"/></param>
    /// <typeparam name="T">The <see cref="Type"/> of the caller</typeparam>
    public static async Task Write<T>(LogLevels logLevel, string line)
    {
        ILogComponent component = GetDefaultComponentFromType<T>();
        await Write<T>(component, logLevel, line);
    }

    /// <summary>
    /// Write a given <see cref="string"/> with the <see cref="ILogComponent"/> and the <see cref="LogLevels"/> to the set <see cref="ILogger"/>s
    /// </summary>
    /// <param name="component">The <see cref="ILogComponent"/></param>
    /// <param name="logLevel">The <see cref="LogLevels"/></param>
    /// <param name="line">The given <see cref="string"/></param>
    /// <exception cref="LogNotInitializedException">Log is not initialized yet</exception>
    public static async Task Write<T>(ILogComponent component, LogLevels logLevel, string? line)
    {
        if (logLevel > LogLevel) //logLevel of the message can't be higher than the set LogLevel
            return;

        ILogMessage<T> message = new LogMessage<T>
        {
            LogLevel = logLevel,
            Timestamp = DateTime.Now,
            Component = component,
            Message = line
        };

        if (Loggers == null)
            throw new LogNotInitializedException();
                
        foreach (ILogger logger in Loggers) 
            await logger.Write(message);
    }

    /// <summary>
    /// Get the default <see cref="ILogComponent"/> for the given <see cref="Assembly"/>
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/></typeparam>
    /// <returns>The <see cref="ILogComponent"/> for the type</returns>
    protected static ILogComponent GetDefaultComponentFromType<T>()
    {
        Assembly? assembly = Assembly.GetAssembly(typeof(T));
        LogComponentAttribute? attribute = assembly?.GetCustomAttribute<LogComponentAttribute>();
        return attribute ?? new LogComponentAttribute("UNKNOWN");
    }
        
    /// <summary>
    /// Write a header to all loggers
    /// </summary>
    /// <returns>A <see cref="Task"/> to wait on</returns>
    public async Task WriteLogHeader<T>()
    {
        await Write<T>($"DefenseMind {Versions.GetVersion()}");
        await Write<T>($"User: {Environment.UserName}");
        await Write<T>($"Date: {DateTime.Today:dd.MM.yyyy}");
        await Write<T>($"OS: {Environment.OSVersion}");
        await Write<T>($"CLR-Version: {Environment.Version}");

        await Write<T>("");
        
        await Write<T>("FileLoggers:");
        if (Loggers != null)
            await Loggers.OfType<IFileLogger>().ForEach(async f => await f.WriteInformation(Write<T>));
        
        await Write<T>("");
    }

    /// <summary>
    /// <see cref="DisposeAsync"/> the <see cref="Log"/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (Loggers != null)
        {
            foreach (ILogger logger in Loggers) 
                await logger.DisposeAsync();

            Loggers.Clear();
        }

        LogLevel = LogLevels.None;
    }

    /// <summary>
    /// Event that is fired when <see cref="SetLogLevel"/> is called
    /// <para><see cref="EventArgs"/> is the newly set <see cref="LogLevels"/></para>
    /// </summary>
    public static event EventHandler<LogLevels>? LogLevelChanged;
}