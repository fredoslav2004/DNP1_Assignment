using System;

namespace CLI.UI.Core;

public interface IView
{
    Task RenderAsync();
}
