namespace DevotedAssessment.Console
{
    using System;

    internal class Program
    {

        private static void Main(string[] args)
        {
            var db = new InMemDatabase();

            var commands = new Dictionary<string, Action<string[]>>() {

                { "SET", (dbArgs) => {
                    db.Set(dbArgs[0], dbArgs[1]);
                } },
                { "GET", (dbArgs) => {
                    Console.WriteLine(db.Get(dbArgs[0]));
                } },
                { "DELETE", (dbArgs) => {
                    db.Delete(dbArgs[0]);
                } },
                { "COUNT", (dbArgs) => {
                    Console.WriteLine(db.Count(dbArgs[0]));
                } },
                { "BEGIN", (dbArgs) => {
                    db.BeginTransaction();
                } },
                { "COMMIT", (dbArgs) => {
                    db.CommitAllTransactions();
                } },
                { "ROLLBACK", (dbArgs) => {
                    db.RollbackTransaction();
                } }
            };

            Console.WriteLine("In memory database ready...");

            while (true)
            {
                string? cmd = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrEmpty(cmd)) break;
                if (cmd == "END") break;

                var cmdArgs = cmd.Split(' ');

                if (commands.ContainsKey(cmdArgs[0].ToUpperInvariant())) {
                    commands[cmdArgs[0]](cmdArgs.Skip(1).ToArray());
                }
            }
        }
    }


}

