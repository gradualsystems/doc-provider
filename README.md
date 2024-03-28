# Document Provider

## Overview

Services for document storage and manipulation.

Document:

- Single file - prime functionality
- Multiple files - collection of files
- Archive files - packed files into archived formats (ZIP, ASiC, ...)

REST API for document operations:

- Upload
- Download

Multiple file storage:

- FileSystem - ready
- DB - TBD
- REST API - TBD
- SOAP API - TBD

## Deployment

### Configuration - GS.DP.APi

- appsettings - section SimpleFileProviderConfig

- RootFolder - path to primary file storage in filesystem (change the path)

- RootFolder2  - path to seconsary file storage in filesystem (change the path)

  > Both parameters are the same (in hosting / customer environment could be different)

  ```
    "SimpleFileProviderConfig": {
      "RootFolder": "D:\\DOC\\FSD",
      "RootFolder2": "D:\\DOC\\FSD"
    },
  ```

### Usage

#### Run the Api

In main repo folder run command batch in cmd shell

```
run.bat
```

#### Call from your solution

Install  GS.DP.Client package in your app

- Add package via console

```cmd
dotnet add package GS.DP.Client --version 1.0.2
```

- Add package via VS window
  - Open Package Manager
  - Search for GS.DP.Client and install

Call sample

- Register FileProviderHttpClient in Program.cs

  ```c#
  builder.Services.AddHttpClient("GS.DP.Client", c =>
  {
      c.BaseAddress = new Uri("https://localhost:7120");
  });
  
  builder.Services.AddTransient<IFileProviderHttpClient, FileProviderHttpClient>();
  
  ```

- Add IFileProviderHttpClient to your class

```c#
public class TestService : ITestService
{
    private readonly IFileProviderHttpClient _httpClient;

    public TestService(IFileProviderHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

```

- Download example

```c#
    public async Task<int> DownloadAsync(string id)
    {
        //call download method
        var result = await _httpClient.DownloadFileBytesAsync(id);

        //do anything with result, ex. save to file
        string filename = Path.Combine(Path.GetTempPath(), id);
        await File.WriteAllBytesAsync(filename, result);

        return 1;
    }

```

- Upload example

```c#
    public async Task<string> RunUploadAsync()
    {
        //get some file from folder
        string filename = @"d:\data\files\test1.pdf";
        byte[] bytes = await File.ReadAllBytesAsync(filename);

        //call upload method
        //pars filenema, bytes
        //returns file id
        var result = await _httpClient.UploadFileBytesAsync(Path.GetFileName(filename), bytes);

        return result;
    }
```



