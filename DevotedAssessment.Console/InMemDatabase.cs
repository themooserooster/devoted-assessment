using System;

namespace DevotedAssessment.Console {

    public class InMemDatabase {

        Dictionary<string, string> db = new Dictionary<string, string>();
        Dictionary<string, int> histogram = new Dictionary<string, int>();

        public void Set(string key, string value) {
            if (db.ContainsKey(key)) {
                var oldVal = db[key];
                histogram[oldVal] -= 1;
                if (histogram[oldVal] == 0) histogram.Remove(oldVal);
            } 
            
            if (!histogram.ContainsKey(value)) histogram[value] = 0;
            histogram[value] += 1;
            
            db[key] = value;
        }

        public string Get(string key) {
            if (!db.ContainsKey(key)) return "NULL";
            
            return db[key];
        }

        public void Delete(string key) {
            if (!db.ContainsKey(key)) return;

            var val = db[key];
            histogram[val] -= 1;
            if (histogram[val] == 0) histogram.Remove(val);

            db.Remove(key);
        }

        public int Count(string val) {
            if (!histogram.ContainsKey(val)) return 0;

            return histogram[val];
        }

        public void BeginTransaction() {
            throw new NotImplementedException();
        }

        public void Commit() {
            throw new NotImplementedException();
        }

        public void Rollback() {
            throw new NotImplementedException();
        }

    }

}