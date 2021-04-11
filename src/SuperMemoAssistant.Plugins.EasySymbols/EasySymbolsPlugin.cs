#region License & Metadata

// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// 
// 
// Created On:   7/8/2020 10:44:33 PM
// Modified By:  james

#endregion




namespace SuperMemoAssistant.Plugins.EasySymbols
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.CodeAnalysis;
  using System.Linq;
  using System.Runtime.Remoting;
  using System.Threading.Tasks;
  using System.Windows.Input;
  using Anotar.Serilog;
  using mshtml;
  using SuperMemoAssistant.Extensions;
  using SuperMemoAssistant.Interop.Plugins;
  using SuperMemoAssistant.Services;
  using SuperMemoAssistant.Services.IO.HotKeys;
  using SuperMemoAssistant.Services.IO.Keyboard;
  using SuperMemoAssistant.Services.UI.Configuration;
  using SuperMemoAssistant.Sys.IO.Devices;

  // ReSharper disable once UnusedMember.Global
  // ReSharper disable once ClassNeverInstantiated.Global
  [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
  public class EasySymbolsPlugin : SMAPluginBase<EasySymbolsPlugin>
  {
    #region Constructors

    /// <inheritdoc />
    public EasySymbolsPlugin() { }

    #endregion


    #region Properties Impl - Public

    /// <inheritdoc />
    public override string Name => "EasySymbols";

    /// <inheritdoc />
    public override bool HasSettings => true;
    private EasySymbolsCfg Config { get; set; }
    private OnKeyUpEvent _keyup { get; set; }

    #endregion

    #region Methods Impl

    private async Task LoadConfig()
    {
      Config = await Svc.Configuration.Load<EasySymbolsCfg>().ConfigureAwait(false) ?? new EasySymbolsCfg();
    }

    /// <inheritdoc />
    protected override void PluginInit()
    {

      LoadConfig().Wait();

      Svc.HotKeyManager.RegisterGlobal(
        "InsertSymbol",
        "Insert Symbol",
        HotKeyScope.SMBrowser,
        new HotKey(Key.S, KeyModifiers.CtrlAltShift),
        InsertSymbol
      );
    }

    private Dictionary<string, string> CreateSymbolMap()
    { 
      try 
      { 
        return Config?.NameSymbolMap?.Deserialize<Dictionary<string, string>>();
      }
      catch (Exception ex)
      {
        LogTo.Debug("Exception caught converting the config symbol map into a C# dictionary. Check the formatting of the config map.");
        return new Dictionary<string, string>();
      }
    }

    [LogToErrorOnException]
    private void InsertSymbol()
    {
      try
      {

        var selObj = ContentUtils.GetSelectionObject();
        if (selObj.IsNull())
          return;

        var symbolMap = CreateSymbolMap();
        if (symbolMap.IsNull() || !symbolMap.Any())
          return;
        
        // If the selection object is a highlight symbol name
        // simply replace it with the symbol
        var text = selObj.text;
        if (!text.IsNullOrEmpty() && symbolMap.TryGetValue(text, out var symbol))
        {
          selObj.text = symbol;
          return;
        }

        // Else, create a span to monitor for input

        var htmlDoc = ContentUtils.GetFocusedHTMLDocument();
        if (htmlDoc.IsNull())
          return;

        string id = Guid.NewGuid().ToString();
        selObj.pasteHTML($"<span id='{id}'>" + "%_%" + "</span>");

        var span = htmlDoc.all
          ?.Cast<IHTMLElement>()
          ?.Where(x => x.id == id)
          ?.FirstOrDefault();

        if (span.IsNull())
          return;

        selObj.moveToElementText(span);
        selObj.moveStart("character", 1);
        selObj.moveEnd("character", -1);
        selObj.select();
        
        _keyup = new OnKeyUpEvent(span);
        ((IHTMLElement2)htmlDoc.body).attachEvent("onkeyup", _keyup);
        _keyup.OnKeyUp += Body_OnKeyUp;

        // TODO: Focus lost or something
        // ((IHTMLElement2)span).attachEvent("on")

      }
      catch (RemotingException) { }

    }

    // Could only manage to get the body element key events to work
    [LogToErrorOnException]
    private void Body_OnKeyUp(object sender, IControlHtmlKeyDownEventArgs e)
    {
      try
      {
        var selObj = ContentUtils.GetSelectionObject();
        var src = selObj.parentElement();
        var target = e.spanElement;

        var children = target.children as IHTMLElementCollection;

        // If either the event source == target 
        // or some child of the event source == target...
        if (target.id == src.id
          || (!children.IsNull() && children.Cast<IHTMLElement>().Any(x => x == src)))
        {
          var symbolMap = CreateSymbolMap();

          var text = src?.innerText?.Trim('%');
          if (!text.IsNullOrEmpty() && symbolMap.TryGetValue(text, out var symbol))
          {
            src.innerText = symbol;
            Unmonitor(target);
          }
        }

      }
      catch (RemotingException) { }

    }


    [LogToErrorOnException]
    private void Unmonitor(IHTMLElement span)
    {

      try
      {

        var htmlDoc = ContentUtils.GetFocusedHTMLDocument();
        var body = htmlDoc?.body;
        if (body.IsNull() || span.IsNull())
          return;

        // Remove events
        ((IHTMLElement2)body).detachEvent("onkeyup", _keyup);

      }
      catch (RemotingException) { }

    }

    /// <inheritdoc />
    public override void ShowSettings()
    {
      ConfigurationWindow.ShowAndActivate(HotKeyManager.Instance, Config);
    }

    #endregion

    #region Methods

    #endregion
  }
}
