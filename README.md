
# Phi-3-mini ONNX with .NET

Phi-3 Mini is a lightweight, state-of-the-art open model that's so powerful and can run locally easily even if you have a normal machine.

By getting Phi-3 to run locally you will have the power of almost GPT 3.5T on your machine where you can build amazing smart tools and your own Copilots that are more private and faster as no internet will be needed and you are in charge of everything. 

Optimized Phi-3 Mini models are published here in  [ONNX](https://onnx.ai/)  format to run with  [ONNX Runtime](https://onnxruntime.ai/)  on CPU and GPU across devices, including server platforms, Windows, Linux, and Mac desktops, and mobile CPUs, with the precision best suited to each of these targets.

![enter image description here](https://github.com/aksoftware98/phi3-dotnet8/blob/main/assets/final-output.png?raw=true)

# Setup
### 1. Clone the repo:
Clone the current repo into your local machine.
Inside the *src* folder, you will find the **Phi3MiniHost** folder that contains the solution. before you can run it there are more steps.

### 2. Setup NuGet feed for ONNXRuntime GenAI for .NET
By default, the ONNXRuntime GenAI packages are in preview and not all of them are available on the public NuGet feed. so you need to add the following source to be able to restore the following packages referenced currently in the app:
```xml
<PackageReference Include="Microsoft.ML.OnnxRuntime" Version="1.17.3" />
<PackageReference Include="Microsoft.ML.OnnxRuntimeGenAI" Version="0.1.0-rc4" />
<PackageReference Include="Microsoft.ML.OnnxRuntimeGenAI.Managed" Version="0.1.0-rc4" />
```

- #### .NET CLI:
```
dotnet nuget add source "https://aiinfra.pkgs.visualstudio.com/PublicPackages/_packaging/onnxruntime-genai/nuget/v3/index.json" --name "onnxruntime"
```

- #### Visual Studio: 
	- Tools/NuGet Package Manager/Package Manager Settings/Sources:
	- Click +, in the name call whatever you want **onnxruntime** for example
	- Provide the following value for the source: ``https://aiinfra.pkgs.visualstudio.com/PublicPackages/_packaging/onnxruntime-genai/nuget/v3/index.json``
	- ![Package Source in VS](https://github.com/aksoftware98/phi3-dotnet8/blob/main/assets/package-sources.png?raw=true)




### 3. Clone the ONNX repo from Hugging Face:
To get started, first you need to clone the repo of the ONNX model of Phi-3-mini either the 4k or the 128k tokens
		. [microsoft/Phi-3-mini-4k-instruct-onnx · Hugging Face](https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-onnx)
		. [microsoft/Phi-3-mini-128k-instruct-onnx · Hugging Face](https://huggingface.co/microsoft/Phi-3-mini-128k-instruct-onnx)
The instructions of the clone can be found in the page itself:
![Clone repository](https://github.com/aksoftware98/phi3-dotnet8/blob/main/assets/clone-model.png?raw=true)
After cloning the repo, this will be the local folder structure on your machine:
```
Phi-3-mini-4k-instruct-onnx/ 
│ 
├── cpu_and_mobile/ 
	│ 
	├── cpu-int4-rtn-32/
	│ │ ├── added_tokens.json 
	│ │ ├── genai_config.json 
	│ │ ├── phi3-mini-4k-instruct-cpu-int4-rtn-block-32.onnx 
	│ │ ├── phi3-mini-4k-instruct-cpu-int4-rtn-block-32.onnx.data
	│ │ ├── special_tokens.json 
	│ │ ├── tokenizer.json 
	│ │ ├── tokenizer.model 
	│ │ └── tokenizer_config.json
	|
	│ 
	├── cpu-int4-rtn-32-acc-level-4/ .. MODEL ..
	|
│ 
├── cude/
	│ 
	├── cuda-fp16/ .. MODEL ..
	|
	│ 
	├── cuda-int4-rtn-block-32/ .. .. MODEL FILES ..
	|
|
├── directml/
	│ 
	├── directml-int4-awq-block-128/ .. MODEL FILES ..
|
├── .gitignore
|
├── config.json
|
├── LICENSE
|
├── README.md
```

The repo contains 3 different ONNX models each optimized to run on a specific hardware acceleration platform and the first one is optimized to run on normal CPUs which is the one we are going to use.
If you run the model with Python, it will be straightforward to follow the instructions and run the model with **NVIDIIA CUDA** or **Windows DirectML** but with the .NET the **ONNXRuntime** packages for GenAI are still in early preview and I didn't manage to get it to run correctly neither with CUDA nor DirectML.

### 4. Copy cpu-int4-rtn-32 content the Model folder:
To not miss with the files of the model. Copy all the content from the folder: ***Phi-3-mini-4k-instruct-onnx/cpu-and-mobile/cpu-int4-rtn-32/***
and paste them inside our solution folder that's called **Model**, it can be found through the path ***src/Phi3MiniHost/Model** 
Mainly you can paste the content anywhere you want but keep in mind to copy the files with their names as they are without any changes.  If you decided to put the model content in a different path related to our project, you need to mention that folder path in our **Program.cs**:
```csharp
... (line 09)

// TODO: Place the path of the model folder here:
// Current directory is : src/Phi3MiniHost/Phi3MiniHost.Console/bin/Debug/net8.0/ 
string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "..", "Model"); // => so this will evaluate to src/Phi3MiniHost/Model/
...
```
### 5. Modify genai_config.json
The current version of the ONNXRuntime GenAI for .NET seems to not support some properties in the **genai_config.json** for the Phi-3. so open that file in your editor of favorite and do the following changes between the lines 28 to 36:

-  Remove the JSON property:
	
	``
	"eos_token_id": [
    32000,
    32001,
    32007
],
	``
- Modify the ``type`` value to ``phi`` instead of ``phi3``

> In the **Phi3MiniHost.Console** project, you will find the modified file, you can copy it to the **Model** folder directly if you want.

### 6. Have fun!
That's it all!!!
You will enjoy Phi-3 to the limits, as it's so versatile, performant, local, and too powerful too. You can start developing your plugins, clients, and so on. 

Share with me what inventions you are working on in the **Issues** above


## Credits
The code above is taken from the following 
[onnxruntime-genai/examples/csharp at main · microsoft/onnxruntime-genai (github.com)](https://github.com/microsoft/onnxruntime-genai/tree/main/examples/csharp) as this model is example is written for Phi-2
