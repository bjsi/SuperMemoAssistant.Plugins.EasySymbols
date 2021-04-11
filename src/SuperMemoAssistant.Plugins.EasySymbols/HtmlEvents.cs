using mshtml;
using System;
using System.Runtime.InteropServices;

namespace SuperMemoAssistant.Plugins.EasySymbols
{
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  public class OnKeyUpEvent
  {
    public event EventHandler<IControlHtmlKeyDownEventArgs> OnKeyUp;
    public IHTMLElement spanElement { get; set; }
    public OnKeyUpEvent(IHTMLElement span)
    {
      this.spanElement = span; 
    }

    [DispId(0)]
    public void handler(IHTMLEventObj e)
    {
      if (!OnKeyUp.IsNull())
        OnKeyUp(this, new IControlHtmlKeyDownEventArgs(e, spanElement));
    }
  }

  public class IControlHtmlKeyDownEventArgs
  {
    public IHTMLEventObj EventObj { get; set; }
    public IHTMLElement spanElement { get; set; }
    public IControlHtmlKeyDownEventArgs(IHTMLEventObj EventObj, IHTMLElement span)
    {
      this.EventObj = EventObj;
      this.spanElement = span;
    }
  }


  // TODO: Some sort of focus out / deactivate event
}
