﻿using Azure;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Intent;
using Microsoft.CognitiveServices.Speech.Translation;

namespace AI.Integration
{
    /// <summary>
    /// This is a wrapper around the Azure Cognitive Services Speech client.
    /// Samples: https://learn.microsoft.com/en-us/dotnet/api/overview/azure/cognitiveservices/speech?view=azure-dotnet
    /// </summary>
    public class SpeechService
    {
        //private readonly TextTranslationClient _textTranslationClient;
        private readonly SpeechTranslationConfig _speechTranslationConfig;

        public SpeechService(Settings settings)
        {
            // Creates an instance of a speech translation config with specified subscription key and service region.
            // Please replace the service subscription key with your subscription key
            //var v2EndpointInString = String.Format("wss://{0}.stt.speech.microsoft.com/speech/universal/v2", AZURE_AI_SERVICE_REGION);
            //var v2EndpointUrl = new Uri(v2EndpointInString);
            //return SpeechTranslationConfig.FromEndpoint(v2EndpointUrl, AZURE_AI_SERVICE_KEY);

            _speechTranslationConfig = SpeechTranslationConfig.FromSubscription(settings.AZURE_AI_SERVICE_KEY, settings.AZURE_AI_SERVICE_REGION);
        }

        public async Task<TranslationRecognitionResult> TranslateAsync(string wavFilePath)
        {
            // When you use Language ID with speech translation, you must set a v2 endpoint and use
            // SpeechTranslationConfig.FromEndpoint() to create the SpeechTranslationConfig object.
            // This will be fixed in a future version of Speech SDK.

            // Translation target language(s).
            // Replace with language(s) of your choice.
            _speechTranslationConfig.AddTargetLanguage("de");
            _speechTranslationConfig.AddTargetLanguage("fr");
            _speechTranslationConfig.AddTargetLanguage("ar");
            _speechTranslationConfig.SpeechRecognitionLanguage = "en-US";

            // Set the mode of input language detection to either "AtStart" (the default) or "Continuous".
            // Please refer to the documentation of Language ID for more information.
            // https://aka.ms/speech/lid?pivots=programming-language-csharp
            //_speechTranslationConfig.SetProperty(PropertyId.SpeechServiceConnection_LanguageIdMode, "Continuous");

            // Define the set of languages to detect
            var autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(new string[] { "en-US", "zh-CN" });

            // Creates a translation recognizer using file as audio input.
            // Replace with your own audio file name.
            using var audioInput = AudioConfig.FromWavFileInput(wavFilePath);
            using var recognizer = new TranslationRecognizer(_speechTranslationConfig, audioInput);
            return await recognizer.RecognizeOnceAsync().ConfigureAwait(false);
        }

        public async Task<SpeechRecognitionResult> RecognizeSpeechAsync(string wavFilePath)
        {
            // Translation target language(s).
            // Replace with language(s) of your choice.
            _speechTranslationConfig.AddTargetLanguage("de");
            _speechTranslationConfig.AddTargetLanguage("fr");
            _speechTranslationConfig.AddTargetLanguage("ar");
            _speechTranslationConfig.SpeechRecognitionLanguage = "en-US";

            // Define the set of languages to detect
            var autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(new string[] { "en-US", "zh-CN" });

            // Creates a translation recognizer using file as audio input.
            // Replace with your own audio file name.
            using var audioInput = AudioConfig.FromWavFileInput(wavFilePath);
            using var recognizer = new SpeechRecognizer(_speechTranslationConfig, audioInput);
            return await recognizer.RecognizeOnceAsync().ConfigureAwait(false);
        }
    }
}
