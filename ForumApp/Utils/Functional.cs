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
            if(HasValue())
            {
                return Resource!;
            }
            else
            {
                throw new InvalidOperationException("Resource does not have a valid value.");
            }
        }
    }
}
