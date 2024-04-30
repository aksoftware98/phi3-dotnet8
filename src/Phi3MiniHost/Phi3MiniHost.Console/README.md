The packages referenced won't be resotred until you configure a NuGet feed for the ONNXRuntime GenAI packages.

### Setup NuGet feed for ONNXRuntime GenAI for .NET
By default the ONNXRuntime GenAI packages are in preview and not all of them are available on the public NuGet feed. so you need to add the following source to be able to restore the following packages referenced currently in the app:
```xml
<PackageReference Include="Microsoft.ML.OnnxRuntime" Version="1.17.3" />
<PackageReference Include="Microsoft.ML.OnnxRuntimeGenAI" Version="0.1.0-rc4" />
<PackageReference Include="Microsoft.ML.OnnxRuntimeGenAI.Managed" Version="0.1.0-rc4" />
```
You can use either .NET CLI or Visual Studio to add the feed:

- ## .NET CLI:
```
dotnet nuget add source "https://aiinfra.pkgs.visualstudio.com/PublicPackages/_packaging/onnxruntime-genai/nuget/v3/index.json" --name "onnxruntime"
```

- ## Visual Studio: 
	- Tools/NuGet Package Manager/Package Manager Settings/Sources:
	- Click +, in the name call whatever you want **onnxruntime-genai** for example
	- Provide the following value for the source: ``https://aiinfra.pkgs.visualstudio.com/PublicPackages/_packaging/onnxruntime-genai/nuget/v3/index.json``

