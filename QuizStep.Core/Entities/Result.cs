using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Core.Entities
{
    public class Result<T>
    {
        public bool Success { get; private set; }
        public string? Error { get; private set; }
        public T? Value { get; private set; }

        public static Result<T> Ok(T value) => new() { Success = true, Value = value };
        public static Result<T> Fail(string error) => new() { Success = false, Error = error };
    }
}
