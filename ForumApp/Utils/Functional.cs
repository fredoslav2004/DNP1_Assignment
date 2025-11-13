using System;

namespace Utils;

public static class Functional
{
    public class AsyncResource<T>
    {
        public T? Resource { get; set; }
        public bool IsLoading { get; set; } = false;
        public Exception? Error { get; set; }

        public void SetValue(T resource)
        {
            Resource = resource;
            IsLoading = false;
            Error = null;
        }

        public void SetError(Exception ex)
        {
            Resource = default;
            IsLoading = false;
            Error = ex;
        }

        public void SetInvalid(string message)
        {
            Resource = default;
            IsLoading = false;
            Error = new Exception(message);
        }

        public static implicit operator T(AsyncResource<T> asyncResource)
        {
            return asyncResource.Resource!;
        }

        public bool HasValue()
        {
            return Resource != null && Error == null && !IsLoading;
        }

        public T GetValue()
        {
            if (HasValue())
            {
                return Resource!;
            }
            else
            {
                throw new InvalidOperationException("Resource does not have a valid value.");
            }
        }
        
        public bool HasError()
        {
            return Error != null;
        }
    }

    public class Maybe<T>
    {
        readonly T? Value;
        public Maybe(T? value)
        {
            Value = value;
        }
        public bool HasValue()
        {
            return Value != null;
        }
        public static Maybe<T> None => new(default);
        public static implicit operator Maybe<T>(T? value) => new(value);
        public static Maybe<T> Some(T value) => new(value);
        public T GetValueOrThrow()
        {
            if (Value == null)
            {
                throw new InvalidOperationException("No value present");
            }
            return Value;
        }
        public T GetValue() => GetValueOrThrow();
    }

    public static R BranchOnCondition<T, R>(this T value, Func<T, bool> condition, Func<T, R> ifTrue, Func<T, R> ifFalse)
    {
        if (condition(value))
        {
            return ifTrue(value);
        }
        else
        {
            return ifFalse(value);
        }
    }
}
