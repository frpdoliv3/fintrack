﻿using CheckDigits.Net;
using System.ComponentModel.DataAnnotations;

namespace FinTrack.DomainModel
{
    internal sealed class IsIsin : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not string stringValue) { return false; }

            var isinAlgorithm = Algorithms.Isin;
            return isinAlgorithm.Validate(stringValue);
        }
    }
}