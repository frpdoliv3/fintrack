using System.Text.Json;

namespace FinTrack.Server.Entity;

internal interface IError
{
    public IError AddError(string varName, IError error);
    public IError AddError(int index, IError error);
    public IError AddError(List<string> errors);
    public IError? GetError(int index);
    public IError? GetError(string varName);

    protected static string ConvertCamelCase(string name)
    {
        return JsonNamingPolicy.CamelCase.ConvertName(name);
    }
}

internal class StringError: IError
{
    public List<string> ErrorMessages { get; init; } = new();

    public StringError(List<string> errors)
    {
        ErrorMessages = errors;
    }

    public IError AddError(string varName, IError error)
    {
        varName = IError.ConvertCamelCase(varName);
        var objectError = new ObjectError
        {
            ErrorMessages = ErrorMessages,
            ChildErrors = new()
        };
        objectError.ChildErrors[varName] = error;
        return objectError;
    }

    public IError AddError(int index, IError error)
    {
        var listError = new ListError
        {
            ErrorMessages = ErrorMessages,
            ChildErrors = new()
        };
        listError.ChildErrors[index] = error;
        return this;
    }

    public IError AddError(List<string> errors)
    {
        foreach (var error in errors)
        {
            ErrorMessages.Add(error);
        }
        return this;
    }

    public IError? GetError(int index)
    {
        throw new InvalidOperationException("StringError has no children");
    }

    public IError? GetError(string varName)
    {
        throw new InvalidOperationException("StringError has no children");
    }
}

internal class RootError : IError 
{
    public Dictionary<string, IError> ChildErrors { get; init; } = new();

    public IError AddError(string varName, IError error)
    {
        varName = IError.ConvertCamelCase(varName);
        ChildErrors[varName] = error;
        return this;
    }

    public IError AddError(int index, IError error)
    {
        throw new InvalidDataException("Impossible to add variable index errors to list");
    }

    public IError AddError(List<string> errors)
    {
        throw new InvalidOperationException("Impossible to add error to root");
    }

    public IError? GetError(int index)
    {
        throw new InvalidOperationException("ObjectError has no index based children");
    }

    public IError? GetError(string varName)
    {
        varName = IError.ConvertCamelCase(varName);
        return ChildErrors.GetValueOrDefault(varName);
    }
}

internal class ObjectError: IError
{
    public List<string> ErrorMessages { get; init; } = new();
    public Dictionary<string, IError> ChildErrors {  get; init; } = new();

    public IError AddError(string varName, IError error)
    {
        varName = IError.ConvertCamelCase(varName);
        ChildErrors[varName] = error;
        return this;
    }

    public IError AddError(int index, IError error)
    {
        throw new InvalidDataException("Impossible to add variable index errors to list");
    }

    public IError AddError(List<string> errors)
    {
        foreach (var error in errors)
        {
            ErrorMessages.Add(error);
        }
        return this;
    }

    public IError? GetError(int index)
    {
        throw new InvalidOperationException("ObjectError has no index based children");
    }

    public IError? GetError(string varName)
    {
        varName = IError.ConvertCamelCase(varName);
        return ChildErrors.GetValueOrDefault(varName);
    }
}

internal class ListError: IError
{
    public List<string> ErrorMessages { get; init; } = new();
    public Dictionary<int, IError> ChildErrors { get; init; } = new();

    public IError AddError(string varName, IError error)
    {
        throw new InvalidDataException("Impossible to add variable name errors to list");
    }

    public IError AddError(int index, IError error)
    {
        ChildErrors[index] = error;
        return this;
    }

    public IError AddError(List<string> errors)
    {
        foreach (var error in errors)
        {
            ErrorMessages.Add(error);
        }
        return this;
    }

    public IError? GetError(int index)
    {
        return ChildErrors.GetValueOrDefault(index);
    }

    public IError? GetError(string varName)
    {
        throw new InvalidOperationException("ListError has no name based children");
    }
}
