using FinTrack.Server.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
namespace FinTrack.Server;
public static class CustomModelStateSerialization
{
    private static bool IsTerminal(string name)
    {
        return name.IndexOf(".") == -1;
    }

    private static bool IsArray(string name)
    {
        var indexStart = -1;
        var indexEnd = -1;
        for (var i = 0; i < name.Length; i++)
        {
            if (name[i] == '[')
            {
                indexStart = i;
            }
            if (name[i] == ']')
            {
                indexEnd = i;
                break;
            }
        }
        if ((indexStart >= indexEnd) || indexStart == -1 || indexEnd == -1)
        {
            return false;
        }
        return Regex.IsMatch(name.Substring(indexStart, indexEnd - indexStart), @"(0|[1-9]\d*)");
    }

    private static int GetArrayIndex(string name)
    {
        var indexStart = name.IndexOf('[') + 1;
        var indexEnd = name.IndexOf("]");
        return int.Parse(name.Substring(indexStart, indexEnd - indexStart));
    }

    private static string GetArrayName(string name)
    {
        return name.Substring(0, name.IndexOf("["));
    }

    private static void ParseError(string varName, ModelStateEntry modelState, IError parent)
    {
        if (IsTerminal(varName))
        {
            var errorNode = new StringError(
                modelState.Errors.Select(error => error.ErrorMessage).ToList()
            );
            var child = parent.GetError(varName);
            if (child != null)
            {
                child.AddError(errorNode.ErrorMessages);
            } 
            else
            {
                parent.AddError(varName, errorNode);
            }
            return;
        }
        var nameComponents = varName.Split('.', 2);
        if (IsArray(nameComponents[0]))
        {
            var arrayName = GetArrayName(nameComponents[0]);
            var arrayIndex = GetArrayIndex(nameComponents[0]);
            if (parent.GetError(arrayName) == null)
            {
                parent.AddError(arrayName, new ListError());
            }
            if (parent.GetError(arrayName)!.GetError(arrayIndex) == null)
            {
                parent.GetError(arrayName)!.AddError(arrayIndex, new ObjectError());
            }
            ParseError(nameComponents[1], modelState, parent.GetError(arrayName)!.GetError(arrayIndex)!);
            return;
        }
        if (parent.GetError(nameComponents[0]) == null)
        {
            parent.AddError(nameComponents[0], new ObjectError());
        }
        ParseError(nameComponents[1], modelState, parent.GetError(nameComponents[0])!);
    }

    public static IActionResult OnSerialize(ActionContext actionContext)
    {
        IError root = new RootError();
        foreach (var modelState in actionContext.ModelState)
        {
            ParseError(modelState.Key, modelState.Value, root);
        }
        return new BadRequestObjectResult(root);
    }
}
