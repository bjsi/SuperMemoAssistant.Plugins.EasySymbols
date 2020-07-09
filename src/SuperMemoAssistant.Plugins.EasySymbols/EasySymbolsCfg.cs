using Forge.Forms.Annotations;
using Newtonsoft.Json;
using SuperMemoAssistant.Services.UI.Configuration;
using SuperMemoAssistant.Sys.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    [Field(Name = "Name-Symbol Pairs (JSON)")]
    [MultiLine]
    public string NameSymbolMap { get; set; } = @"
{
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
      return "EasySymbols";
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
