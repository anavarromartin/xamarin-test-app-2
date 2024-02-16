using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamarinDemo2
{
    public class OpenAiWorkoutGeneratorService : IWorkoutGeneratorService
    {
        private IOpenAiApi OpenAiApi;

        private string systemPrompt = @"You are a workout generator. You will receive an input with the following parameters in json format: 
- ""workoutAreas"": This is an array of areas to focus the workout on.

You will respond in a JSON array format. You need to provide a complete set of exercises that focus on the input areas that should last for a workout duration of 60 minutes approximately.

Each exercise is a JSON object with the following fields:
- ""exercise"": This is a string with the name of the exercise, for example ""bench press"".
- ""reps"": this is a string containing a rep range, for example ""10-12"".
- ""description"": This is a string with a brief description of how to perform the exercise correctly.

Below is an example of what an answer might look like. Do not deviate from that JSON format. Notice that every exercise is an element of a JSON array. You must also return a JSON array with exercise objects in it:
```
{""exercises"": [
  {
    ""exercise"": ""Squats"",
    ""reps"": ""10-12"",
    ""description"": ""Stand with your feet shoulder-width apart, lower your body as if sitting back into a chair, and then push through your heels to return to the starting position.""
  },
  {
    ""exercise"": ""Deadlifts"",
    ""reps"": ""8-10"",
    ""description"": ""Stand with your feet hip-width apart, bend at the hips and knees to lower the barbell to the floor, and then stand up straight by extending your hips and knees.""
  },
  {
    ""exercise"": ""Lunges"",
    ""reps"": ""12-15"",
    ""description"": ""Stand with your feet hip-width apart, take a step forward with one leg and lower your body until your front knee is bent at a 90-degree angle, then push through your front heel to return to the starting position.""
  },
  {
    ""exercise"": ""Leg Press"",
    ""reps"": ""10-12"",
    ""description"": ""Sit on the leg press machine with your feet shoulder-width apart, push the platform away from your body by extending your knees, and then slowly lower it back down.""
  },
  {
    ""exercise"": ""Calf Raises"",
    ""reps"": ""15-20"",
    ""description"": ""Stand with your feet hip-width apart, raise your heels off the ground by extending your ankles, and then lower them back down.""
  }
]}
```";

        public OpenAiWorkoutGeneratorService(IOpenAiApi openAiApi)
        {
            OpenAiApi = openAiApi;
        }

        public async Task<WorkoutResponse> GenerateWorkout(List<string> focusAreas)
        {
            var request = new CompletionRequest
            {
                model = "gpt-3.5-turbo-0125",
                response_format = new ResponseFormat
                {
                    type = "json_object"
                },
                messages = new List<Message>
                {
                    new()
                    {
                        role = "system",
                        content = systemPrompt
                    },
                    new()
                    {
                        role = "user",
                        content = focusAreas.ToJsonArray()
                    }
                }
            };

            var response = await OpenAiApi.GetCompletion(request);
            var jsonString = response.choices[0].message.content.Trim();
            var workout = JsonConvert.DeserializeObject<WorkoutResponse>(jsonString);
            
            return workout;
        }
    }

    static class ListExtension
    {
        public static string ToJsonArray(this List<string> list)
        {
            return "[" + string.Join(", ", list.Select(item => "\"" + item + "\"")) + "]";
        }
    }
}

