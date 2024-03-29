﻿using mshtml;
using SuperMemoAssistant.Extensions;
using SuperMemoAssistant.Services;
using System;
using System.Runtime.Remoting;

namespace SuperMemoAssistant.Plugins.EasySymbols
{
  public static class ContentUtils
  {
    /// <summary>
    /// Get the selection object representing the currently highlighted text in SM.
    /// </summary>
    /// <returns>IHTMLTxtRange object or null</returns>
    public static IHTMLTxtRange GetSelectionObject()
    {
      try
      {
        var ctrlGroup = Svc.SM.UI.ElementWdw.ControlGroup;
        var htmlCtrl = ctrlGroup?.FocusedControl?.AsHtml();
        var htmlDoc = htmlCtrl?.GetDocument();
        var sel = htmlDoc?.selection;

        if (!(sel?.createRange() is IHTMLTxtRange textSel))
          return null;

        return textSel;
      }
      catch (RemotingException) { }
      catch (UnauthorizedAccessException) { }

      return null;

    }

    /// <summary>
    /// Get the IHTMLDocument2 representing the focused html control
    /// </summary>
    /// <returns>IHTMLDocument2 or null</returns>
    public static IHTMLDocument2 GetFocusedHTMLDocument()
    {

      try
      {
        var ctrlGroup = Svc.SM.UI.ElementWdw.ControlGroup;
        var htmlCtrl = ctrlGroup?.FocusedControl?.AsHtml();
        return htmlCtrl?.GetDocument();
      }
      catch (RemotingException) { }
      catch (UnauthorizedAccessException) { }

      return null;
    }
  }
}
