## Easy Symbols Plugin for SuperMemoAssistant

### Features

- Easily insert symbols like Greek letters into SuperMemo HTML components.
- Easily add / remove available symbols.

### Installation

#### Manual Installation

##### Pre-built binaries (RECOMMENDED)
###### Guide

1. Check the [releases](https://github.com/bjsi/SuperMemoAssistant.Plugins.EasySymbols/releases) for this repository.
2. Download the latest available version.
3. Navigate to the development plugin folder (`C:\Users\<YOUR USERNAME>\SuperMemoAssistant\Plugins\Development`) and extract the zip folder into the directory.
  > Note: If you are upgrading from an older version, you should delete the older version first.

##### Building from source
###### Prerequisites

- Install Visual Studio 2019 or higher.
- Select the following  VS components during the install:
  + .NET desktop development
  + .NET Core cross-platform development

###### Guide

1. Clone the project using git.

  `git clone https://github.com/bjsi/SuperMemoAssistant.Plugins.EasySymbols`

2. Open the cloned project folder.

3. Double click on the solution (EasySymbols.sln) to open the project in Visual Studio 2019.

4. Right click on the solution file in the **Solution Explorer**:

![Image of Solution Explorer](https://github.com/bjsi/docs/blob/master/SMA/plugins/images/solution-explorer.png)

5. Select **Build Solution**:

![Image of Build Solution Option](https://github.com/bjsi/docs/blob/master/SMA/plugins/images/build-solution.jpg)

6. Check that the build succeeded by confirming that a folder matching the name of the github repo exists in the following directory:

`C:\Users\<YOUR USERNAME>\SuperMemoAssistant\Plugins\Development`

7. Close Visual Studio and run SuperMemoAssistant.

### Manual

#### Usage

The **default hotkey** is `Ctrl+Alt+Shift+S`.

Either:
- Highlight a piece of text that matches a symbol's name and press the hotkey to convert it to its corresponding symbol.
- Without any highlighted text, press the hotkey and type the name of the symbol.

#### Configuration

##### Settings

> You can access the settings of any SuperMemoAssistant plugin by pressing Ctrl+Alt+Shift+O and clicking the gear icon.
- You can define your own name: symbol mapping in the config.

### Contributing Guide

#### Issues and Suggestions

See the [contribution guide](https://github.com/bjsi/docs/blob/master/SMA/plugins/CONTRIBUTING.md) for information on how to report issues or make suggestions.

#### Code Contributions

Pull requests are welcome!

1. Firstly, go through the manual installation guide above.
2. You will also require [Git Hooks for VS](https://marketplace.visualstudio.com/items?itemName=AlexisIncogito.VisualStudio-Git-Hooks) which is used to enforce a consistent code style.
> Note: you do not need to build the entire SuperMemoAssistant project to make changes to or debug a plugin.
3. See the code section of the [contribution guide](https://github.com/bjsi/docs/blob/master/SMA/plugins/CONTRIBUTING.md) for pull request instructions.
4. If you need help, don't hesitate to get in touch with me (Jamesb) on the SMA discord channel.
