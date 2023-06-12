## About Duke.LogWriter 
That small library is designed for creating log files and is usually used in command-line applications. By default, do not require any settings.

## Installation
A simple way is to install it from the Nuget package library and it is ready to use. No additional settings are required.
```sh
dotnet add package Duke.LogWriter
```

## Usage
   ```sh
   using Duke.LogWriter;
   LogWriter.Instance.WriteLog("Hello world", true);
   ```
That code will create in your application work directory a new subfolder with the name `logs` and put the file in the format `DukeLogWriter_yyyy_MM_dd.txt`. The log will look like 
```sh
[05:57:01] Hello world
```

### Write Header
```sh
LogWriter.Instance.WriteHeader("Application start", true);
```
That code will print header text with formatting.
```sh
[06:27:45] ================================================================================
[06:27:45] =============================== Application start ==============================
[06:27:45] ================================================================================
```

### File Mask
The File mask default value is `DukeLogWriter_{date}.txt`. You can change that mask.
```sh
LogWriter.Instance.FileMask = "TestLog_{date}.txt";
```
After that, all files will be in the format `TestLog_yyyy_MM_dd.txt`

### Line Mask
The Line mask by default contains a time stamp + log string `[{time}] {log}`. We can change it and remove the time value.

   ```sh
   LogWriter.Instance.LineMask = "{log}";
   LogWriter.Instance.WriteLog("Hello world", true);
   ```
As a result, there will not be a time stamp on the log line.

### Log Folder
The logger by default creates a subfolder with the name `logs`. You can change the name of the folder or set an empty string (will be used current application folder).
   ```sh
   LogWriter.Instance.LogFolder = "BuildLogs";
   ```

### Show In Console
By default, the logger does not print anything to the output console. It can be changed in two ways. Set the value to true
   ```sh
   LogWriter.Instance.WriteLog("Hello world", true);
   ```
or by changing the default value.
   ```sh
   LogWriter.Instance.ShowInConsoleDefault = true;
   LogWriter.Instance.WriteLog("Hello world");
   ```

