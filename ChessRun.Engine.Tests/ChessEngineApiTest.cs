using System;
using System.IO;
using NUnit.Framework;

namespace ChessRun.Engine.Tests {
    [TestOf(typeof(ChessEngineApi))]
    public class ChessEngineApiTest : BaseTestFixture {

        [Test]
        public void DivideSuiteTest() {
            var resource = GetType().Assembly.GetManifestResourceStream("ChessRun.Engine.Tests.Resources.perftsuite.epd");
            if (resource == null) throw new InvalidOperationException("Test resource is not found");
            var engine = new ChessEngineApi();
            using (var reader = new StreamReader(resource)) {
                var lineIndex = 0;
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine() ?? "";
                    lineIndex++;
                    line = line.Trim();
                    if (line.StartsWith("#") || line == string.Empty) continue;
                    var args = line.Split(';');
                    engine.SetBoard(args[0]);
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine(args[0]);
                    for (var i = 1; i < args.Length; i++) {
                        var expectedArgs = args[i].Trim().Split(' ');
                        int depth = int.Parse(expectedArgs[0].TrimStart('D'));
                        ulong expected = ulong.Parse(expectedArgs[1]);
                        if (depth == 4) {
                            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - -");
                            var actual = engine.Divide(depth);
                            Assert.AreEqual(expected, actual, "Checking line " + lineIndex + ", depth " + depth);
                        }
                    }
                }
            }
        }
    }
}
