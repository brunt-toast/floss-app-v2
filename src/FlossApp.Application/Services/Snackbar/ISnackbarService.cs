using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Services.Snackbar;

public interface ISnackbarService
{
    void ShowSnackbar(string message, SnackbarSeverity? severity = null, string? key = null);
}
