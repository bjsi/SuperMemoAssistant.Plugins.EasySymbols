﻿using Forge.Forms;
using Forge.Forms.Annotations;
using Newtonsoft.Json;
using SuperMemoAssistant.Services.UI.Configuration;
using SuperMemoAssistant.Sys.ComponentModel;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace SuperMemoAssistant.Plugins.EasySymbols
{
  [Form(Mode = DefaultFields.None)]
  [Title("Dictionary Settings",
         IsVisible = "{Env DialogHostContext}")]
  [DialogAction("cancel",
        "Cancel",
        IsCancel = true)]
  [DialogAction("save",
        "Save",
        IsDefault = true,
        Validates = true)]
  public class EasySymbolsCfg : CfgBase<EasySymbolsCfg>, INotifyPropertyChangedEx
  {

    [Title("Easy Symbols Plugin")]
    [Heading("By Jamesb | Experimental Learning")]

    [Heading("Features")]
    [Text(@"- Easily insert symbols like Greek letters into SuperMemo HTML components.
- Easily add / remove available symbols.")]

    [Heading("Support")]
    [Text("If you would like to support my projects, check out my Patreon or buy me a coffee.")]

    [Action("patreon", "Patreon", Placement = Placement.Before, LinePosition = Position.Left)]
    [Action("coffee", "Coffee", Placement = Placement.Before, LinePosition = Position.Left)]

    [Heading("Links")]
    [Action("github", "GitHub", Placement = Placement.Before, LinePosition = Position.Left)]
    [Action("feedback", "Feedback Site", Placement = Placement.Before, LinePosition = Position.Left)]
    [Action("blog", "Blog", Placement = Placement.Before, LinePosition = Position.Left)]
    [Action("youtube", "YouTube", Placement = Placement.Before, LinePosition = Position.Left)]
    [Action("twitter", "Twitter", Placement = Placement.Before, LinePosition = Position.Left)]

    [Heading("Settings")]

    [Field(Name = "Name-Symbol Pairs (JSON)")]
    [MultiLine]
    public string NameSymbolMap { get; set; } = @"{
  'alpha': 'α',
  'beta': 'ß',
  'gamma': 'Γ',
  'delta': 'δ',
  'epsilon': 'ε',
  'theta': 'Θ',
  'pi': 'π',
  'mu': 'µ',
  'usigma': 'Σ',
  'lsigma': 'σ',
  'tau': 	'τ',
  'uphi': 'Φ',
  'lphi': 'φ',
  'omega': 'Ω'
}";

    [JsonIgnore]
    public bool IsChanged { get; set; }

    public override string ToString()
    {
      // Human-readable Plugin name
      return "Easy Symbols";
    }

    public override void HandleAction(IActionContext actionContext)
    {

      string patreon = "https://www.patreon.com/experimental_learning";
      string coffee = "https://buymeacoffee.com/experilearning";
      string github = "https://github.com/bjsi/SuperMemoAssistant.Plugins.HtmlTables";
      string feedback = "https://feedback.experimental-learning.com/";
      string youtube = "https://www.youtube.com/channel/UCIaS9XDdQkvIjASBfgim1Uw";
      string twitter = "https://twitter.com/experilearning";
      string blog = "https://www.experimental-learning.com/";

      string action = actionContext.Action as string;
      if (action == "patreon")
        openLinkDefaultBrowser(patreon);
      else if (action == "github")
        openLinkDefaultBrowser(github);
      else if (action == "coffee")
        openLinkDefaultBrowser(coffee);
      else if (action == "feedback")
        openLinkDefaultBrowser(feedback);
      else if (action == "youtube")
        openLinkDefaultBrowser(youtube);
      else if (action == "twitter")
        openLinkDefaultBrowser(twitter);
      else if (action == "blog")
        openLinkDefaultBrowser(blog);
      else
        base.HandleAction(actionContext);
    }

    // Hack
    private DateTime LastLinkOpen { get; set; } = DateTime.MinValue;

    private void openLinkDefaultBrowser(string url)
    {
      var diffInSeconds = (DateTime.Now - LastLinkOpen).TotalSeconds;
      if (diffInSeconds > 1)
      {
        LastLinkOpen = DateTime.Now;
        Process.Start(url);
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
