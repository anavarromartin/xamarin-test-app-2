using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace XamarinDemo2
{
    public interface IOpenAiApi
    {
        [Headers("Authorization: Bearer")]
        [Post("/chat/completions")]
        Task<CompletionResponse> GetCompletion([Body] CompletionRequest request);
    }

    public class CompletionRequest
    {
        public string model { get; set; }
        public ResponseFormat response_format { get; set; }
        public List<Message> messages { get; set; }
    }

    public class ResponseFormat
    {
        public string type { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class CompletionResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }
}

