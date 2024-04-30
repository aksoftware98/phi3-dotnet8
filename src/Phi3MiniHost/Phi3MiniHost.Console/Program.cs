using Microsoft.ML.OnnxRuntimeGenAI;

Console.WriteLine("-------------");
Console.WriteLine("Hello, Phi-3 Mini!");
Console.WriteLine("-------------");

// TODO: Place the path of the model folder here:
// Current directory is : src/Phi3MiniHost/Phi3MiniHost.Console/bin/Debug/net8.0/ 
string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "..", "Model"); // => so this will evaluate to src/Phi3MiniHost/Model/
using Model model = new Model(modelPath);
using Tokenizer tokenizer = new Tokenizer(model);

Console.WriteLine("Please enter option number:");
Console.WriteLine("1. Complete Output");
Console.WriteLine("2. Streaming Output");
int.TryParse(Console.ReadLine(), out var option);

while (true)
{
	Console.WriteLine("Prompt:");
	// Example prompts available on Hugging Face page of the model:
	// https://huggingface.co/microsoft/Phi-3-mini-4k-instruct#chat-format
	string prompt = Console.ReadLine() ?? string.Empty;
	var sequences = tokenizer.Encode(prompt);

	using GeneratorParams generatorParams = new GeneratorParams(model);
	generatorParams.SetSearchOption("max_length", 400); // Maximum length of the output
	generatorParams.SetSearchOption("temperature", 0.3); // Temperature controls the randomness of the output: near 0 means deterministic, near 1 means more random
	generatorParams.SetInputSequences(sequences);

	if (option == 1) // Complete Output
	{
		var outputSequences = model.Generate(generatorParams);
		var outputString = tokenizer.Decode(outputSequences[0]);

		Console.WriteLine("Output:");
		Console.WriteLine(outputString);
	}

	else if (option == 2) //Streaming Output
	{
		using var tokenizerStream = tokenizer.CreateStream();
		using var generator = new Generator(model, generatorParams);
		while (!generator.IsDone())
		{
			generator.ComputeLogits();
			generator.GenerateNextToken();
			Console.Write(tokenizerStream.Decode(generator.GetSequence(0)[^1]));
		}
	}
}