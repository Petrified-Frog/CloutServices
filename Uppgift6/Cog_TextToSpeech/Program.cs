using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Cog_TextToSpeech
{
    public class Program
    {
        static async Task Main()
        {
            await SynthesizeAudioAsync();
        }


        public static async Task SynthesizeAudioAsync()
        {
            var config = SpeechConfig.FromSubscription("bec4a3cbdee24d01bb640c6491f15fec", "westeurope");
            config.SpeechSynthesisVoiceName = "en-US-GuyNeural";
            //config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff24Khz16BitMonoPcm);

            //Option 1 - to file
            //using var audioConfig = AudioConfig.FromWavFileOutput("D:/Programming/Lexicon/Molntjänster/Uppgift6/Cog_TextToSpeech/output/file.wav");
            //using var synthesizer = new SpeechSynthesizer(config, audioConfig);
            //await synthesizer.SpeakTextAsync("The Wheel of Time turns, and Ages come and pass, leaving memories that become legend. Legend fades to myth, and even myth is long forgotten when the Age that gave it birth comes again.");
            //option 2 - to speaker
            //using var synthesizer = new SpeechSynthesizer(config);
            //await synthesizer.SpeakTextAsync("The Wheel of Time turns, and Ages come and pass, leaving memories that become legend. Legend fades to myth, and even myth is long forgotten when the Age that gave it birth comes again.");
            //option 3 - to stream            
            //using var synthesizer = new SpeechSynthesizer(config, null);
            //var result = await synthesizer.SpeakTextAsync("Getting the response as an in-memory stream.");
            //using var stream = AudioDataStream.FromResult(result);
            //await stream.SaveToWaveFileAsync("D:/Programming/Lexicon/file.wav"); //NOTE: SaveToWaveFileAsync() cant take åäö

            //option 4 - With XML customization
            using var synthesizer = new SpeechSynthesizer(config, null);

            var ssml = File.ReadAllText("./ssml.xml");
            var result = await synthesizer.SpeakSsmlAsync(ssml);

            using var stream = AudioDataStream.FromResult(result);
            await stream.SaveToWaveFileAsync("D:/Programming/Lexicon/mix.wav");
        }
    }
}
