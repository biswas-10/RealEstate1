namespace RealEstate.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    protected Result(
        bool isSuccess,
        Error? error)
    {
        IsSuccess = isSuccess;

        if (isSuccess && error is not null)
        {
            throw new ArgumentException(
                "Successful result cannot contain an error.");
        }

        if (!isSuccess && error is null)
        {
            throw new ArgumentException(
                "Failer result must contain an error.");
        }

        Error = error;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }
}

public class Result<T> : Result
    {
        public T? Value { get; }

        public T GetValueorThrow()
        {
            if (IsFailure)
            {
                throw new InvalidOperationException(
                    "Cannot access the value of failed result.");
            }

            return Value!;
        }

        private Result(
            T? value,
            bool isSuccess,
            Error? error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(
                value,
                true,
                null);
        }

        public static new Result<T> Failure(Error error)
        {
            return new Result<T>(
                default,
                false,
                error);
        }
}

