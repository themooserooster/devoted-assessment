using System;

namespace DevotedAssessment.Console {

    public class InMemDatabase {

        internal class TransactionEntry {
            public required string Key { get; set; }
            public string? OldValue { get; set; }
            public string? NewValue { get; set; }
        }

        internal class Transaction : List<TransactionEntry> {}

        Dictionary<string, string> db = new Dictionary<string, string>();
        Dictionary<string, int> histogram = new Dictionary<string, int>();
        List<Transaction> transactions = new List<Transaction>();

        public void Set(string key, string value) {
            if (db.ContainsKey(key)) DecrementHistogram(db[key]);
            IncrementHistogram(value);

            if (!db.ContainsKey(key)) LogTransaction(key, null, value);
            else LogTransaction(key, db[key], value);

            db[key] = value;
        }

        public string Get(string key) {
            if (!db.ContainsKey(key)) return "NULL";
            
            return db[key];
        }

        public void Delete(string key) {
            if (!db.ContainsKey(key)) return;

            var oldValue = db[key];
            DecrementHistogram(oldValue);
            LogTransaction(key, oldValue, null);
            
            db.Remove(key);
        }

        public int Count(string val) {
            if (!histogram.ContainsKey(val)) return 0;

            return histogram[val];
        }

        public void BeginTransaction() {
            transactions.Add(new Transaction());
        }

        public void CommitAllTransactions() {
            transactions.Clear();
        }

        public void RollbackTransaction() {
            if (transactions.Count == 0) return;

            var trx = transactions.Last();

            for (var i = trx.Count-1; i >= 0; i--) {
                var entry = trx[i];
                
                IncrementHistogram(entry.OldValue);
                DecrementHistogram(entry.NewValue);

                if (entry.OldValue == null) db.Remove(entry.Key);
                else db[entry.Key] = entry.OldValue;

            }

            transactions.RemoveAt(transactions.Count - 1);
        }

        protected void LogTransaction(string key, string? oldValue, string? newValue) {
            if (transactions.Count == 0) return;

            transactions.Last().Add(new TransactionEntry() {
                Key = key,
                OldValue = oldValue,
                NewValue = newValue
            });
        }

        protected void IncrementHistogram(string? val) {
            if (val == null) return;
            
            if (!histogram.ContainsKey(val)) histogram[val] = 0;
            histogram[val] += 1;
        }

        protected void DecrementHistogram(string? val) {
            if (val == null) return;

            if (!histogram.ContainsKey(val)) return;
            histogram[val] -= 1;
            if (histogram[val] == 0) histogram.Remove(val);
        }
    }

}