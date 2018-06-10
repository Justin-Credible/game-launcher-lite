Game Launcher Lite
=============

:warning: Work in progress :warning:

This is a simple frontend designed for an low resolution (15 kHz CRT) arcade monitor that allows the user to browse a selection of games and start one. It was designed to be a much simpler alternative to fully-featured frontends (like [Hyperspin](http://www.hyperspin-fe.com/)) for system that have a handful of games rather than thousands.

I refer to it as _Lite_ because I previously made [another game launcher](https://github.com/Justin-Credible/Game-Launcher) which uses a high resolution GUI, while this one is simply a text menu with an arcade-style font.

Though it was designed for launching games, it can actually launch any type of executable.

It is written in C# using WinForms and is designed for very low resolution monitors (such as 15 kHz CRT arcade monitor).

## User Experience

The user is launched into a full screen application and can use the up and down arrow keys to move through a list of games. Pressing space (or other configured key) will cause the selected game to be launched.

The title and footer labels can have customized text.

When idle, the frontend can be configured to launch a specific game after a configurable number of seconds.

## How to Build

You'll also want to make sure you have the `Press Start K` font installed before running (see `resources/PRSTARTK.TTF`).

Open the solution file `src/GameLauncherLite.sln` with Visual Studio 2017 community edition and press F6 to build or F5 to run.

## Configuration

Several parameters such as resolution, emulator path, keybindings, and game list can be configured by editing the self-documented `App.config` file.

## License

Released under an MIT license; see [LICENSE](https://github.com/Justin-Credible/game-launcher-lite/blob/master/LICENSE) for more information.
