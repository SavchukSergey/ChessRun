using System.Reflection;
using ChessRun.Engine;
using ChessRun.Engine.Debugger;

[assembly: AssemblyTitle("ChessRun.Engine.Debugger")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]

[assembly: System.Diagnostics.DebuggerVisualizer(typeof(ChessBoardDebugger), typeof(ChessBoardVisualizerObjectSource),
    Target = typeof(ChessBoard), Description = "ChessBoard Visualizer")]
